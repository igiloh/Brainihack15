using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeuroSky.ThinkGear;

namespace BrainwaveScroller
{
    public delegate void ChangedEEGValEventHandler(double newVal);
    public delegate void ChangedStatusEventHandler(string newVal);
    public delegate void ChangedBooleanEventHandler(bool newVal);    

    class MindWave
    {
        // Const Members
        public const String k_RawStr = "Raw";               // DO NOT CHANGE IT!!!! it is key to the Data from headset
        public const String k_PoorSignalStr = "PoorSignal"; // DO NOT CHANGE IT!!!!
        public const String k_AttentionStr = "Attention";   // DO NOT CHANGE IT!!!!
        public const String k_MeditationStr = "Meditation"; // DO NOT CHANGE IT!!!!
        public const String k_DefultPortCom = "COM7";
        public const int k_PoorSignalVal = 200; 
        // Event
        public event ChangedEEGValEventHandler OnAttentionNewValueEvent;
        public event ChangedEEGValEventHandler OnMeditationNewValueEvent;
        public event ChangedStatusEventHandler OnNewStatus;
        public event ChangedBooleanEventHandler OnPoorSiganl; // Provide 'true' if the signal is poor and 'false' if not

        // Data Members
        private Connector m_Connector = new Connector();
        private bool m_IsMindWaveConnected = false;
        private String m_LastPortUsed = k_DefultPortCom;

        private bool m_PoorSignal = true;
        private double m_PreviousPoorSignal = k_PoorSignalVal;
        private double m_CurrentPoorSignal = k_PoorSignalVal;
        private double m_PriviousAttention = 0;
        private double m_CurrnetAttention = 0;
        private double m_PriviousMedition = 0;
        private double m_CurrnetMedition = 0;

        // Ctor
        public MindWave()
        {
            initConnector();
        }

        // Properties
        public bool IsMindWaveConnected {
            get { return m_IsMindWaveConnected; }
        }

        // Methodes
        public void Connect()
        {
            this.m_Connector.ConnectScan(m_LastPortUsed);
        }

        private void initConnector()
        {
            this.m_Connector.DeviceConnected += new EventHandler(OnDeviceConnected);
            this.m_Connector.DeviceFound += new EventHandler(OnDeviceFound);
            this.m_Connector.DeviceNotFound += new EventHandler(OnDeviceNotFound);
            this.m_Connector.DeviceConnectFail += new EventHandler(OnDeviceNotFound);
            this.m_Connector.DeviceDisconnected += new EventHandler(OnDeviceDisconnected);
            this.m_Connector.DeviceValidating += new EventHandler(OnDeviceValidating);  
        }

        private void OnDeviceValidating(object sender, EventArgs e)
        {
            if (null != OnNewStatus)
                OnNewStatus("Validating");
        }

        private void OnDeviceDisconnected(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs deviceEventArgs = (Connector.DeviceEventArgs)e;

            if (null != OnNewStatus) 
                OnNewStatus("Disconnected from device: " + deviceEventArgs.Device.PortName);
            deviceEventArgs.Device.DataReceived -= OnDataReceived;
            initParams();
        }

        private void initParams()
        {
            m_PoorSignal = true;
            m_PreviousPoorSignal = k_PoorSignalVal;
            m_CurrentPoorSignal = k_PoorSignalVal;
            m_PriviousAttention = 0;
            m_CurrnetAttention = 0;
            m_PriviousMedition = 0;
            m_CurrnetMedition = 0;
        }

        private void OnDeviceNotFound(object sender, EventArgs e)
        {
            if (null != OnNewStatus) 
                OnNewStatus("No device founds :(");
        }

        private void OnDeviceFound(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs deviceEventArgs = (Connector.DeviceEventArgs)e;
            if (null != OnNewStatus) 
                OnNewStatus("The device " + deviceEventArgs.Device.PortName + " found");
        }

        private void OnDeviceConnected(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs deviceEventArgs = (Connector.DeviceEventArgs)e;

            this.m_LastPortUsed = deviceEventArgs.Device.PortName;
            if (null != OnNewStatus) 
                OnNewStatus("New Headset Created: " + this.m_LastPortUsed);
            deviceEventArgs.Device.DataReceived += new EventHandler(OnDataReceived);
        }

        private bool updatePoorSignalValue(double i_Previous, double i_Currnet)
        {
            // UP poorsignal value 
            // return true - siganl changed
            // return false - signal doesn't change
            m_PreviousPoorSignal = i_Previous;
            m_CurrentPoorSignal = i_Currnet;

            bool change = false;

            if (m_CurrentPoorSignal != m_PreviousPoorSignal) // the signal changed and now is poor
            {
                change = true;
            }

            return change;
        }

        private bool itIsPoorSignal()
        {
            return m_CurrentPoorSignal != 0;
        }

        private void OnDataReceived(object sender, EventArgs e)
        {
            Device device = (Device)sender;
            Device.DataEventArgs dataEventArgs = (Device.DataEventArgs)e;

            TGParser tgParser = new TGParser();
            tgParser.Read(dataEventArgs.DataRowArray);

            for (int i = 0; i < tgParser.ParsedData.Length; i++)
            {
                if (tgParser.ParsedData[i].ContainsKey(k_RawStr))
                {
                    // writeLogs(String.Format(k_DataMsg, k_RawStr, tgParser.ParsedData[i][k_RawStr]));
                }

                if (tgParser.ParsedData[i].ContainsKey(k_PoorSignalStr))
                {

                    if (this.updatePoorSignalValue(m_CurrentPoorSignal, tgParser.ParsedData[i][k_PoorSignalStr]))
                    {
                        if (itIsPoorSignal())
                        {
                            if (null != OnPoorSiganl)
                                OnPoorSiganl(true);                                   
                        }
                        else
                        {
                            if (null != OnPoorSiganl)
                                OnPoorSiganl(false);
                        }
                    }

                    //writeLogs(String.Format(k_DataMsg, " - " + k_PoorSignalStr, tgParser.ParsedData[i][k_PoorSignalStr]));
                }

                if (tgParser.ParsedData[i].ContainsKey(k_AttentionStr))
                {
                    // writeLogs(String.Format(k_DataMsg, " @ " + k_AttentionStr, tgParser.ParsedData[i][k_AttentionStr]));

                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = tgParser.ParsedData[i][k_AttentionStr];

                    OnAttentionNewValueEvent(m_CurrnetAttention);
                }

                if (tgParser.ParsedData[i].ContainsKey(k_MeditationStr))
                {
                    // writeLogs(String.Format(k_DataMsg, " $ " + k_MeditationStr, tgParser.ParsedData[i][k_MeditationStr]));

                    m_PriviousMedition = m_CurrnetMedition;
                    m_CurrnetMedition = tgParser.ParsedData[i][k_MeditationStr];

                    OnMeditationNewValueEvent(m_CurrnetMedition);
                }
            }
        }

        public void CloseMindWave()
        {
            m_Connector.Close();
        }


    }
}
