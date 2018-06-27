using System;
using System.Threading;
using System.Windows.Forms;
using EduLanCastCore.Controllers.Drawcontrol.DrawingFunc;
using EduLanCastCore.Models.Drawmodel;
using EduLanCastOld.Controllers.Views;
using EduLanCastOld.Data;

namespace EduLanCastOld.Views
{
    public partial class DrawingForm : Form
    {
        /// <summary>
        /// DrawingControl 板书窗口控制器
        /// </summary>
        //private DrawingController Controller;
        public DrawingForm()
        {
            InitializeComponent();
            StaticData.FormMgr.ManageObject[nameof(ParentForm)] = this;
            StaticData.ThreadMgr.ManageObject["RenderScreen"] = new Thread(RenderScreen);
            Canvainfo.Height = Height;
            Canvainfo.Width = Width;
            Canvainfo.HandPtr = Handle;
            //默认为Chalk Line:2
            Tooltype.Type = 1;
            Tooltype.Line = 2;
        }

        private void RenderScreen()
        {
            
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
