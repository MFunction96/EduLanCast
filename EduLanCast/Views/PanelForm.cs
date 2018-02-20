using EduLanCast.Controllers.Threads;
using EduLanCast.Controllers.Views;
using EduLanCast.Properties;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace EduLanCast.Views
{
    public partial class PanelForm : Form
    {
        private IList<int> Fps { get; }
        private int Interval { get; set; }
        private PanelController Controller { get; }

        public PanelForm()
        {
            InitializeComponent();
            Fps = new List<int> { 1, 2, 5, 10, 20, 50, 100 };
            Controller = new PanelController();
            ThreadManager.Threads["CaptureScreen"] = new Thread(ShowScreen) {Name = "CaptureScreen"};
            CbFps.DataSource = Fps;
            Interval = (int)CbFps.SelectedItem;
            CbAdapters.DataSource = Controller.Adapters;
        }

        private void BtnDemo_Click(object sender, EventArgs e)
        {
            ThreadManager.Threads["CaptureScreen"].Start();
        }

        private void ShowScreen()
        {
            Controller.Duplicate(CbOutputs.SelectedItem.ToString());
            Thread.Sleep(Interval);
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            ThreadManager.Threads["CaptureScreen"].Interrupt();
        }

        private async void CbAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Controller.QueryOutputsAsync(CbAdapters.SelectedItem.ToString());
            CbOutputs.DataSource = null;
            CbOutputs.DataSource = Controller.Outputs;
        }

        private void CbFps_SelectedIndexChanged(object sender, EventArgs e)
        {
            Interval = (int)CbFps.SelectedItem;
            Controller.Fps = (int)CbFps.SelectedItem;
        }

        private void PanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ThreadManager.Terminate();
        }

        private async void CbOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblDpi.Text = CbOutputs.DataSource is null
                ? Resources.NullTip
                : await Controller.SelectOutput(CbOutputs.SelectedItem.ToString());
        }
    }
}
