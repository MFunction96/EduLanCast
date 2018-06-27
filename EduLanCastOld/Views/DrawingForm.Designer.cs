using System.Windows.Forms;
using EduLanCastCore.Controllers.Drawcontrol;
using EduLanCastCore.Controllers.Drawcontrol.DrawingFunc;

namespace EduLanCastOld.Views
{
    partial class DrawingForm
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
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Pointtrace.Pressdown();
                Pointtrace.AddPoint((float)(2 * e.X - this.Width) / this.Width, (float)(this.Height - 2 * e.Y) / this.Height);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Pointtrace.Flag == 1)
            {
                if (Tooltype.IsChalkorEra())
                {
                    Pointtrace.AddPoint((float)(2 * e.X - this.Width) / this.Width, (float)(this.Height - 2 * e.Y) / this.Height);
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Pointtrace.AddPoint((float)(2 * e.X - this.Width) / this.Width, (float)(this.Height - 2 * e.Y) / this.Height);
                Pointtrace.Pressup();
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            switch (e.KeyChar)
            {
                case 'c':
                    Tooltype.Type = 1;
                    Tooltype.Line = 2;
                    break;
                case 'e':
                    Tooltype.Type = 2;
                    Tooltype.Line = 7;
                    break;
                case 's':
                    if (!Tooltype.IsonlyClickTool())
                    {
                        Tooltype.Lasttype = Tooltype.Type;
                    }
                    Tooltype.Type = 3;
                    break;
                case 'y':
                    Tooltype.Type = 4;
                    break;
                case 'f':
                    Tooltype.Type = 5;
                    break;
                case '1':
                    Tooltype.Line = 1;
                    break;
                case '2':
                    Tooltype.Line = 2;
                    break;
                case '3':
                    Tooltype.Line = 3;
                    break;
                case '4':
                    Tooltype.Line = 4;
                    break;
                case '5':
                    Tooltype.Line = 5;
                    break;
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DrawingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DrawingForm";
            this.Load += new System.EventHandler(this.DrawingForm_Load);
            this.ResumeLayout(false);

        }


        #endregion
    }
}