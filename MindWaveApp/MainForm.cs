using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using NeuroSky.ThinkGear;

namespace MindWaveApp
{
    public delegate void ConnectDelegate(String str);

    public partial class MindWaveForm : Form
    {
        // Delegate
        delegate void SetTextCallback(string text);
        
        // Const Members
        private const String k_RawStr = "Raw";               // DO NOT CHANGE IT!!!! it is key to the Data from headset
        private const String k_PoorSignalStr = "PoorSignal"; // DO NOT CHANGE IT!!!!
        private const String k_AttentionStr = "Attention";   // DO NOT CHANGE IT!!!!
        private const String k_MeditationStr = "Meditation"; // DO NOT CHANGE IT!!!!
        private const String k_DataMsg = "{0}\tValue: {1}.";
        private const String k_DefultPortCom = "COM40";
        private const double k_BllonImgW = 90;
        private const double k_BllonImgH = 100;

   
        // Data Members
        private readonly List<TextBox> r_ResultTextBoxs = new List<TextBox>();
        private Connector m_Connector = new Connector();
        private String m_LastUsedPortName = k_DefultPortCom;
        private Thread m_WriteThread = null;
        private int m_ImageLocationY; // Save the location 
        private bool m_TheBallonChanged = false;
        private Bitmap m_BallonImg = new Bitmap("HotAirBallon.png");

        private double m_PriviousAttention = 0;
        private double m_CurrnetAttention = 0;
        private double m_PriviousMedition = 0;
        private double m_CurrnetMedition = 0;
         
        // C'tor
        public MindWaveForm()
        {
            InitializeComponent();
            initConnector();
            Application.ApplicationExit += new EventHandler(OnProcessExit);
            m_ImageLocationY = Leeway_panel.Size.Height - Leeway_panel.Size.Width;
        }

        // Methodes
        private void OnProcessExit(object sender, EventArgs e)
        {
            m_Connector.Close();
            Console.WriteLine("The App exit and closed the the connector!");
        }

        private void initConnector()
        {
            m_Connector.DeviceConnected += new EventHandler(OnDeviceConnected);
            m_Connector.DeviceFound += new EventHandler(OnDeviceFound);
            m_Connector.DeviceNotFound += new EventHandler(OnDeviceNotFound);
            m_Connector.DeviceConnectFail += new EventHandler(OnDeviceNotFound);
            m_Connector.DeviceDisconnected += new EventHandler(OnDeviceDisconnected);
            m_Connector.DeviceValidating += new EventHandler(OnDeviceValidating);  
        }

        private void OnDeviceValidating(object sender, EventArgs e)
        {
            this.m_WriteThread =
                new Thread(new ThreadStart(this.validatingActionThreadSafe));

            this.m_WriteThread.Start();
        }

        private void validatingActionThreadSafe()
        {
            writeLogs("Validating...");
        }

        private void OnDeviceDisconnected(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs deviceEventArgs = (Connector.DeviceEventArgs)e;

            writeLogs("Disconnected from device: " + deviceEventArgs.Device.PortName);
            deviceEventArgs.Device.DataReceived -= OnDataReceived;
            swapEnableBitweenConnectButton();
            initParams();
        }

        private void initParams()
        {
            m_CurrnetMedition = m_CurrnetAttention = 0;
            writeMessage("");
        }

        private void OnDeviceNotFound(object sender, EventArgs e)
        {
            swichConnectButtonMode();
            writeLogs("No device founds :(");
        }

        private void swichConnectButtonMode()
        {
            if (isGUIThreadIsInvoke())
            {
                Action a = new Action(swichConnectButtonMode);
                this.Invoke(a, new object[] { });
            }
            else
            {
                Connect_button.Enabled = !Connect_button.Enabled;
            }
        }

        private void OnDeviceFound(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs deviceEventArgs = (Connector.DeviceEventArgs)e;
            writeLogs("The device " + deviceEventArgs.Device.PortName + " found");
        }

        private void OnDeviceConnected(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs deviceEventArgs = (Connector.DeviceEventArgs)e;

            m_LastUsedPortName = deviceEventArgs.Device.PortName;
            writeLogs("New Headset Created: " + m_LastUsedPortName);
            deviceEventArgs.Device.DataReceived += new EventHandler(OnDataReceived);

            writeMessage("Get concentrated to lift the ballon.");
            swapEnableBitweenConnectButton();
        }

        private void writeMessage(String msg)
        {
            if (isGUIThreadIsInvoke())
            {
                SetTextCallback d = new SetTextCallback(writeMessage);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                Message_label.Text = msg;
            }
        }

        private bool isGUIThreadIsInvoke()
        {
            return this.Logs_listBox.InvokeRequired || this.Message_label.InvokeRequired;
        }
        private void swapEnableBitweenConnectButton()
        {
            if (isGUIThreadIsInvoke())
            {
                Action a = new Action(swapEnableBitweenConnectButton);
                this.Invoke(a, new object[] { });
            }
            else
            {
                Connect_button.Enabled = Connect_button.Enabled == Disconnect_button.Enabled ? false : Disconnect_button.Enabled;
                Disconnect_button.Enabled = !Disconnect_button.Enabled;
            }
        }

        private void writeAttention(String msg)
        {
            if (isGUIThreadIsInvoke())
            {
                SetTextCallback d = new SetTextCallback(writeAttention);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                Attention_label.Text = msg;
            }
        }

        private void writeMedition(String msg)
        {
            if (isGUIThreadIsInvoke())
            {
                SetTextCallback d = new SetTextCallback(writeMedition);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                Medition_label.Text = msg;
            }
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
                    writeLogs(String.Format(k_DataMsg, " - " + k_PoorSignalStr, tgParser.ParsedData[i][k_PoorSignalStr]));
                }

                if (tgParser.ParsedData[i].ContainsKey(k_AttentionStr))
                {
                    writeLogs(String.Format(k_DataMsg, " @ " + k_AttentionStr, tgParser.ParsedData[i][k_AttentionStr]));
                    
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = tgParser.ParsedData[i][k_AttentionStr];

                    writeAttention(((int)m_CurrnetAttention).ToString());
                }

                if (tgParser.ParsedData[i].ContainsKey(k_MeditationStr))
                {
                    writeLogs(String.Format(k_DataMsg, " $ " + k_MeditationStr, tgParser.ParsedData[i][k_MeditationStr]));

                    m_PriviousMedition = m_CurrnetMedition;
                    m_CurrnetMedition = tgParser.ParsedData[i][k_MeditationStr];

                    writeMedition(((int)m_CurrnetMedition).ToString());
                }
            }
        }

        private int newValueForImageLocationY()
        {
            double diffPanelHighAndImgHigh = Leeway_panel.Size.Height - Leeway_panel.Size.Width;
            double resY = diffPanelHighAndImgHigh - ((m_CurrnetAttention / 100) * diffPanelHighAndImgHigh);
            
            return (int)resY;
        }

        private void writeLogs(String msg)
        {

            if (isGUIThreadIsInvoke())
            {
                SetTextCallback d = new SetTextCallback(writeLogs);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                String dataTime = DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToString("h:mm:ss tt") + "  ||\t";
                String logMessage = dataTime + msg;
                Logs_listBox.Items.Add(logMessage);
                int visibleItems = Logs_listBox.ClientSize.Height / Logs_listBox.ItemHeight;
                Logs_listBox.TopIndex = Math.Max(Logs_listBox.Items.Count - visibleItems + 1, 0);
            }   
        }

        private void Connect_button_Click(object sender, EventArgs e)
        {
            m_Connector.ConnectScan(m_LastUsedPortName);
            Connect_button.Enabled = false;
        }

        private void Disconnect_button_Click(object sender, EventArgs e)
        {
            m_Connector.Disconnect();
        }

        private void Leeway_panel_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImage(m_BallonImg,
            //    10, m_ImageLocationY,
            //    Leeway_panel.Size.Width - 20, Leeway_panel.Size.Width);
        }

        private int clickCountForTesting = 0; // Data member that related to the method: testForAttetion

        private void testForAttention(object sender, EventArgs e)
        {
            // This Method simulate data that recived from headset
            int x = clickCountForTesting % 10;
            clickCountForTesting++;

            switch (x)
            {
                case 0:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 20;
                    break;
                case 1:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 60;
                    break;
                case 2:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 90;
                    break;
                case 3:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 93;
                    break;
                case 4:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 100;
                    break;
                case 5:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 30;
                    break;
                case 6:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 56;
                    break;
                case 7:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 79;
                    break;
                case 9:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 34;
                    break;
                case 10:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 15;
                    break;
                default:
                    m_PriviousAttention = m_CurrnetAttention;
                    m_CurrnetAttention = 83;
                    break;
            }
        }

        private void Moving_timer_Tick(object sender, EventArgs e)
        {
            int Y = newValueForImageLocationY();

            m_TheBallonChanged = false;

            if (m_ImageLocationY > Y) // Up - The user become more Attention
            {
                m_TheBallonChanged = true;
                m_ImageLocationY -= 10;

                if (m_ImageLocationY < Y)
                {
                    m_ImageLocationY = Y;
                }
            }
            else if (m_ImageLocationY < Y) // Down - The user become less Attention
            {
                m_TheBallonChanged = true;
                m_ImageLocationY += 10;

                if (m_ImageLocationY > Y)
                {
                    m_ImageLocationY = Y;
                }
            }

            // Invalidate();
            if (m_TheBallonChanged)
            {
                this.Invalidate();
              //  Leeway_panel.Invalidate();                
                
            }
        }

        private void MindWaveForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(m_BallonImg,
                Logs_listBox.Right + 10, m_ImageLocationY,
                Leeway_panel.Size.Width - 20, Leeway_panel.Size.Width);
            //e.Graphics.DrawImage(m_BallonImg,
            //    Logs_listBox.Right + 10, m_ImageLocationY,
            //    74, 94);
        }
    }
}