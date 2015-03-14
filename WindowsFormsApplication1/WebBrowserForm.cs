using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

//using System.Threading;

namespace BrainwaveScroller
{
    enum BciHeadsetType
    {
        NEUROSKY = 0,
        NEUROSTEER
    }

    public partial class WebBrowserForm : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        const BciHeadsetType currHeadsetType = BciHeadsetType.NEUROSKY;

        const long KEY_PRESS_IGNORE_TIME = 200; //milli sec
        const uint MOUSEEVENTF_WHEEL = 0x0800;

        System.Threading.Thread workerThread = null;
        int m_nScrollIntervalMilliSec = 130;
        volatile bool bFormClosing = false;
        bool bScrollingEnabled = false;
        Stopwatch m_KeyPressStopwatch = new Stopwatch();
        MindWave m_mindWaves;
        CsvToEEG m_csvReader;
        Timer m_tNeuroSteerReadTimer;

        double m_dFilteredAtten = 0;
        double m_dPrevAtten = 0; 

        public WebBrowserForm()
        {
            InitializeComponent();

            m_KeyPressStopwatch.Start();
            StartWorkerThread();

            switch (currHeadsetType)
            {
                case BciHeadsetType.NEUROSKY:
                    InitNeuroSky();
                    break;
                case BciHeadsetType.NEUROSTEER:
                    InitNeroSteer();
                    break;
                default:
                    break;
            }

        }

        void InitNeuroSky()
        {
            m_mindWaves = new MindWave();
            m_mindWaves.OnAttentionNewValueEvent += OnNewAttenValue;
            m_mindWaves.OnMeditationNewValueEvent += OnNewMeditationValue;
            m_mindWaves.OnNewStatus += UpdateStatusString;
            m_mindWaves.OnPoorSiganl += OnPoorSignal;

            m_mindWaves.Connect();
        }

        void InitNeroSteer()
        {
            m_csvReader = new CsvToEEG();
            m_tNeuroSteerReadTimer.Interval = 1000;
            m_tNeuroSteerReadTimer.Tick += m_tNeuroSteerReadTimer_Tick;
            m_tNeuroSteerReadTimer.Start();
        }

        void m_tNeuroSteerReadTimer_Tick(object sender, EventArgs e)
        {
            Dictionary<String, double> data;
            if (m_csvReader.readData(@"C:\temp\test.csv"))
            {
                data = m_csvReader.GetEEG;


                try
                {
                    double e2 = data["e2"];
                    double dAttention = ((e2 + 6) / 12) * 100;
                    OnNewAttenValue(dAttention);
                }
                catch (Exception)
                {
                }
            }

        }

        public void OnNewAttenValue(double dNewAttenVal)
        {
            SetPicBoxHeight(picboxAttention, (int)dNewAttenVal);

            SetScrollInterval((int)CalcIntervalFromAtten(dNewAttenVal));
        }

        private double CalcIntervalFromAtten(double atten)
        {
            const double dFactor = 0.7;
            const bool bUseDeltas = true;

            double dNewTimeInterval;

            if (bUseDeltas)
            {
                //m_dFilteredAtten = dFactor * m_dFilteredAtten + (1.0d - dFactor) * atten;
                //double delta = atten - m_dFilteredAtten;

                m_dFilteredAtten = dFactor * (m_dFilteredAtten + atten - m_dPrevAtten);
                m_dPrevAtten = atten;

                //double delta = m_dFilteredAtten;
                //Console.WriteLine(delta.ToString());

                if (m_dFilteredAtten > 8)
                    return 3000;
                else if (m_dFilteredAtten < -7)
                    return 250;
                else
                    return m_nScrollIntervalMilliSec;
            }
            else
                dNewTimeInterval = (atten / 100) * 1000 + 100;

            return dNewTimeInterval;
        }

       public delegate void  UpdateStatusDelegate(string strStatus);
       void UpdateStatusString(string strStatus)
       {
           if (bFormClosing)
               return;

           Invoke (new UpdateStatusDelegate(
               delegate
               {
                   lblStatus.Text = strStatus;
               }
               ) ,strStatus );
       }

        public void OnNewMeditationValue(double dNewMedVal)
        {
            SetPicBoxHeight(picboxMeditation, (int)dNewMedVal);
        }

        private void SetScrollInterval(int nScrollInterval)
        {
            m_nScrollIntervalMilliSec = nScrollInterval;
        }

        private void EnableScrolling(bool bEnable)
        {
            bScrollingEnabled = bEnable;
        }

        private void ToggleScrollingEnabled()
        {
            bScrollingEnabled = !bScrollingEnabled;
        }

        public delegate void ScrollDownDelegate();
        public void ScrollDown()
        {
            if (false == bScrollingEnabled || true == bFormClosing)
                return;

            Invoke(new ScrollDownDelegate(
            delegate
            {
                if (false == mainWebBrowser.Focused)
                    mainWebBrowser.Focus();
                //System.Windows.Forms.SendKeys.Send("{PGDN}");
                //mainWebBrowser.AutoScrollOffset = new Point(mainWebBrowser.AutoScrollOffset.X, mainWebBrowser.AutoScrollOffset.Y + 10);
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, unchecked((uint) - 120), UIntPtr.Zero);
            })
            );
        }

        public delegate void OnPoorSignalDelegate(bool bPoorSig);
        public void OnPoorSignal(bool bPoorSig) 
        {
            Invoke(new OnPoorSignalDelegate
                (
                delegate
                {
                    if (bPoorSig)
                    {
                        lblStatus.Text = "Poor Signal";
                        lblStatus.ForeColor = Color.Red;
                        EnableScrolling(false);
                    }
                    else
                    {
                        lblStatus.Text = "BCI ready";
                        lblStatus.ForeColor = Color.Green;
                        EnableScrolling(true);
                    }
                }
                ), bPoorSig);
        }

        private void TempScrollerThread()
        {
            while (false == bFormClosing)
            {
                System.Threading.Thread.Sleep(m_nScrollIntervalMilliSec);
                ScrollDown();
            }
        }

        private void StartWorkerThread()
        {

            if (null == workerThread)
            {
                workerThread = new System.Threading.Thread(TempScrollerThread);
                workerThread.Start();
            }
        }

        private void WebBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currHeadsetType == BciHeadsetType.NEUROSKY)
                m_mindWaves.CloseMindWave();

            bFormClosing = true;
            //workerThread. 
        }

        private void mainWebBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (m_KeyPressStopwatch.ElapsedMilliseconds < KEY_PRESS_IGNORE_TIME)
                    return;

                ToggleScrollingEnabled();
                m_KeyPressStopwatch.Restart();
                
            }
        }

        private delegate void SetPicBoxHeightDelegatePictureBox (PictureBox picBox , int nHeight);
        private void SetPicBoxHeight(PictureBox picBox , int nHeight)
        {
            Invoke (new SetPicBoxHeightDelegatePictureBox(
            delegate
            {
                if (nHeight < 0 || nHeight > 100)
                    return;

                picBox.Location = new Point(picBox.Location.X, picBox.Location.Y - nHeight + picBox.Height);
                picBox.Height = nHeight;
            }
            ) ,picBox , nHeight );
        }

    }
}
