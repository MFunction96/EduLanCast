using EduLanCastCore.Controllers.Drawcontrol;
using EduLanCastCore.Controllers.Drawcontrol.DrawingFunc;
using EduLanCastOld.Controllers.Views;
using EduLanCastOld.Data;
using System;
using System.Threading;
using System.Windows.Forms;

namespace EduLanCastCoreTests.Controllers.Drawing
{
    public partial class DrawingForm : Form
    {
        /// <summary>
        /// DrawingControl 板书窗口控制器
        /// </summary>
        private DrawingController Controller;
        public DrawingForm()
        {
            InitializeComponent();
            StaticData.FormMgr.ManageObject[nameof(ParentForm)] = this;
            StaticData.ThreadMgr.ManageObject["RenderScreen"] = new Thread(RenderScreen);
            Canvainfo.height = Height;
            Canvainfo.width = Width;
            Canvainfo.handPtr = Handle;
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
