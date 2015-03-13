namespace BrainwaveScroller
{
    partial class WebBrowserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainWebBrowser = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainWebBrowser
            // 
            this.mainWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.mainWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.mainWebBrowser.Name = "mainWebBrowser";
            this.mainWebBrowser.Size = new System.Drawing.Size(778, 423);
            this.mainWebBrowser.TabIndex = 0;
            this.mainWebBrowser.Url = new System.Uri("http://facebook.com", System.UriKind.Absolute);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(659, 378);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WebForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 423);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mainWebBrowser);
            this.Name = "WebForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser mainWebBrowser;
        private System.Windows.Forms.Button button1;
    }
}

