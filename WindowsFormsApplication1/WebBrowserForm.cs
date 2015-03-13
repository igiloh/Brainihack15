using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Threading;

namespace BrainwaveScroller
{
    public partial class WebBrowserForm : Form
    {
        System.Threading.Thread workerThread = null;

        public WebBrowserForm()
        {
            InitializeComponent();

        }

        // ~WebForm()
        //{
        //    workerThread.Abort();
        //}

        public delegate void ScrollDownDelegate();
        public void ScrollDown()
        {
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
            //System.Threading.Thread.Sleep(7000);
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                ScrollDown();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //mainWebBrowser.AutoScrollOffset = new Point(0, 1000);
            //ScrollDown();

            if (null == workerThread)
            {
                workerThread = new System.Threading.Thread(TempScrollerThread);
                workerThread.Start();
            }
        }
    }
}
