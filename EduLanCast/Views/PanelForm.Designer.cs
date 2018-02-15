namespace EduLanCast.Views
{
    partial class PanelForm
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
            this.BtnDemo = new System.Windows.Forms.Button();
            this.PbScreen = new System.Windows.Forms.PictureBox();
            this.BtnStop = new System.Windows.Forms.Button();
            this.CbAdapters = new System.Windows.Forms.ComboBox();
            this.CbFps = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.PbScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnDemo
            // 
            this.BtnDemo.Location = new System.Drawing.Point(924, 115);
            this.BtnDemo.Name = "BtnDemo";
            this.BtnDemo.Size = new System.Drawing.Size(75, 23);
            this.BtnDemo.TabIndex = 0;
            this.BtnDemo.Text = "捕获";
            this.BtnDemo.UseVisualStyleBackColor = true;
            this.BtnDemo.Click += new System.EventHandler(this.BtnDemo_Click);
            // 
            // PbScreen
            // 
            this.PbScreen.Location = new System.Drawing.Point(55, 93);
            this.PbScreen.Name = "PbScreen";
            this.PbScreen.Size = new System.Drawing.Size(813, 456);
            this.PbScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbScreen.TabIndex = 1;
            this.PbScreen.TabStop = false;
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(924, 157);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(75, 23);
            this.BtnStop.TabIndex = 2;
            this.BtnStop.Text = "暂停";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // CbAdapters
            // 
            this.CbAdapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbAdapters.FormattingEnabled = true;
            this.CbAdapters.Location = new System.Drawing.Point(55, 53);
            this.CbAdapters.Name = "CbAdapters";
            this.CbAdapters.Size = new System.Drawing.Size(813, 23);
            this.CbAdapters.TabIndex = 3;
            this.CbAdapters.SelectedIndexChanged += new System.EventHandler(this.CbAdapters_SelectedIndexChanged);
            // 
            // CbFps
            // 
            this.CbFps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbFps.FormattingEnabled = true;
            this.CbFps.Location = new System.Drawing.Point(899, 53);
            this.CbFps.Name = "CbFps";
            this.CbFps.Size = new System.Drawing.Size(121, 23);
            this.CbFps.TabIndex = 4;
            this.CbFps.SelectedIndexChanged += new System.EventHandler(this.CbFps_SelectedIndexChanged);
            // 
            // PanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 608);
            this.Controls.Add(this.CbFps);
            this.Controls.Add(this.CbAdapters);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.PbScreen);
            this.Controls.Add(this.BtnDemo);
            this.Name = "PanelForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PanelForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PbScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnDemo;
        private System.Windows.Forms.PictureBox PbScreen;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.ComboBox CbAdapters;
        private System.Windows.Forms.ComboBox CbFps;
    }
}

