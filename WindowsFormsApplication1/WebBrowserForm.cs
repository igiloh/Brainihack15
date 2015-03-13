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
//using System.Threading;

namespace BrainwaveScroller
{
    public partial class WebBrowserForm : Form
    {
        const long KEY_PRESS_IGNORE_TIME = 200; //milli sec

        System.Threading.Thread workerThread = null;
        int m_nScrollIntervalMilliSec = 500;
        bool bFormClosing = false;
        bool bScrollingEnabled = false;
        Stopwatch m_KeyPressStopwatch = new Stopwatch();
        MindWave m_mindWaves;

        public WebBrowserForm()
        {
            InitializeComponent();

            m_KeyPressStopwatch.Start();
            StartWorkerThread();

            m_mindWaves = new MindWave();
            m_mindWaves.OnAttentionNewValueEvent += OnNewAttenValue;
            m_mindWaves.OnMeditationNewValueEvent += OnNewMeditationValue;
            
            m_mindWaves.Connect();

        }

        public void OnNewAttenValue(double dNewAttenVal)
        {
            SetPicBoxHeight(picboxAttention, (int)dNewAttenVal);
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
            if (false == bScrollingEnabled)
                return;

            Invoke(new ScrollDownDelegate(
            delegate
            {
                if (false == mainWebBrowser.Focused)
                    mainWebBrowser.Focus();
                System.Windows.Forms.SendKeys.Send("{PGDN}");
            })
            );
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
            bFormClosing = true;
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
