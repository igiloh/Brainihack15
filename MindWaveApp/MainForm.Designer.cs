namespace MindWaveApp
{
    partial class MindWaveForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MindWaveForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Connect_button = new System.Windows.Forms.Button();
            this.Disconnect_button = new System.Windows.Forms.Button();
            this.Logs_listBox = new System.Windows.Forms.ListBox();
            this.Leeway_panel = new System.Windows.Forms.Panel();
            this.Message_label = new System.Windows.Forms.Label();
            this.Moving_timer = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.Attention_label = new System.Windows.Forms.Label();
            this.Medition_label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 46);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mind Wave App";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Logs:";
            // 
            // Connect_button
            // 
            this.Connect_button.Location = new System.Drawing.Point(718, 745);
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.Size = new System.Drawing.Size(141, 59);
            this.Connect_button.TabIndex = 5;
            this.Connect_button.Text = "Connect";
            this.Connect_button.UseVisualStyleBackColor = true;
            this.Connect_button.Click += new System.EventHandler(this.Connect_button_Click);
            // 
            // Disconnect_button
            // 
            this.Disconnect_button.Enabled = false;
            this.Disconnect_button.Location = new System.Drawing.Point(566, 744);
            this.Disconnect_button.Name = "Disconnect_button";
            this.Disconnect_button.Size = new System.Drawing.Size(141, 59);
            this.Disconnect_button.TabIndex = 9;
            this.Disconnect_button.Text = "Disconnect";
            this.Disconnect_button.UseVisualStyleBackColor = true;
            this.Disconnect_button.Click += new System.EventHandler(this.Disconnect_button_Click);
            // 
            // Logs_listBox
            // 
            this.Logs_listBox.FormattingEnabled = true;
            this.Logs_listBox.ItemHeight = 25;
            this.Logs_listBox.Location = new System.Drawing.Point(12, 184);
            this.Logs_listBox.Name = "Logs_listBox";
            this.Logs_listBox.Size = new System.Drawing.Size(694, 554);
            this.Logs_listBox.TabIndex = 10;
            this.Logs_listBox.Click += new System.EventHandler(this.testForAttention);
            // 
            // Leeway_panel
            // 
            this.Leeway_panel.BackColor = System.Drawing.Color.Transparent;
            this.Leeway_panel.ForeColor = System.Drawing.Color.Black;
            this.Leeway_panel.Location = new System.Drawing.Point(718, 17);
            this.Leeway_panel.Name = "Leeway_panel";
            this.Leeway_panel.Size = new System.Drawing.Size(141, 720);
            this.Leeway_panel.TabIndex = 11;
            this.Leeway_panel.Visible = false;
            this.Leeway_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.Leeway_panel_Paint);
            // 
            // Message_label
            // 
            this.Message_label.AutoSize = true;
            this.Message_label.BackColor = System.Drawing.Color.Transparent;
            this.Message_label.ForeColor = System.Drawing.Color.Red;
            this.Message_label.Location = new System.Drawing.Point(16, 69);
            this.Message_label.Name = "Message_label";
            this.Message_label.Size = new System.Drawing.Size(0, 25);
            this.Message_label.TabIndex = 12;
            // 
            // Moving_timer
            // 
            this.Moving_timer.Enabled = true;
            this.Moving_timer.Tick += new System.EventHandler(this.Moving_timer_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 764);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Attention:";
            // 
            // Attention_label
            // 
            this.Attention_label.AutoSize = true;
            this.Attention_label.Location = new System.Drawing.Point(125, 764);
            this.Attention_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Attention_label.Name = "Attention_label";
            this.Attention_label.Size = new System.Drawing.Size(24, 25);
            this.Attention_label.TabIndex = 14;
            this.Attention_label.Text = "0";
            // 
            // Medition_label
            // 
            this.Medition_label.AutoSize = true;
            this.Medition_label.Location = new System.Drawing.Point(346, 764);
            this.Medition_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Medition_label.Name = "Medition_label";
            this.Medition_label.Size = new System.Drawing.Size(24, 25);
            this.Medition_label.TabIndex = 16;
            this.Medition_label.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 764);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 25);
            this.label5.TabIndex = 15;
            this.label5.Text = "Medition:";
            // 
            // MindWaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(903, 734);
            this.Controls.Add(this.Medition_label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Attention_label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Message_label);
            this.Controls.Add(this.Leeway_panel);
            this.Controls.Add(this.Logs_listBox);
            this.Controls.Add(this.Disconnect_button);
            this.Controls.Add(this.Connect_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "MindWaveForm";
            this.RightToLeftLayout = true;
            this.Text = "Hello Word Mind Wave";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MindWaveForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Connect_button;
        private System.Windows.Forms.Button Disconnect_button;
        private System.Windows.Forms.ListBox Logs_listBox;
        private System.Windows.Forms.Panel Leeway_panel;
        private System.Windows.Forms.Label Message_label;
        private System.Windows.Forms.Timer Moving_timer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Attention_label;
        private System.Windows.Forms.Label Medition_label;
        private System.Windows.Forms.Label label5;
    }
}

