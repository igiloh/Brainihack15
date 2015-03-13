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
            this.picboxAttention = new System.Windows.Forms.PictureBox();
            this.picboxMeditation = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picboxAttention)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxMeditation)).BeginInit();
            this.SuspendLayout();
            // 
            // mainWebBrowser
            // 
            this.mainWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.mainWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.mainWebBrowser.Name = "mainWebBrowser";
            this.mainWebBrowser.Size = new System.Drawing.Size(921, 477);
            this.mainWebBrowser.TabIndex = 0;
            this.mainWebBrowser.Url = new System.Uri("http://facebook.com", System.UriKind.Absolute);
            this.mainWebBrowser.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.mainWebBrowser_PreviewKeyDown);
            // 
            // picboxAttention
            // 
            this.picboxAttention.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picboxAttention.BackColor = System.Drawing.Color.Lime;
            this.picboxAttention.Location = new System.Drawing.Point(826, 364);
            this.picboxAttention.Name = "picboxAttention";
            this.picboxAttention.Size = new System.Drawing.Size(14, 100);
            this.picboxAttention.TabIndex = 1;
            this.picboxAttention.TabStop = false;
            // 
            // picboxMeditation
            // 
            this.picboxMeditation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picboxMeditation.BackColor = System.Drawing.Color.Green;
            this.picboxMeditation.Location = new System.Drawing.Point(850, 364);
            this.picboxMeditation.Name = "picboxMeditation";
            this.picboxMeditation.Size = new System.Drawing.Size(14, 100);
            this.picboxMeditation.TabIndex = 1;
            this.picboxMeditation.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(823, 464);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Att";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(847, 464);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Med";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(881, 463);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 4;
            // 
            // WebBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 477);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picboxMeditation);
            this.Controls.Add(this.picboxAttention);
            this.Controls.Add(this.mainWebBrowser);
            this.Name = "WebBrowserForm";
            this.Text = "BrainwaveBrowser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebBrowserForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picboxAttention)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxMeditation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser mainWebBrowser;
        private System.Windows.Forms.PictureBox picboxAttention;
        private System.Windows.Forms.PictureBox picboxMeditation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
    }
}

