using EduLanCast.Controllers.Views;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using EduLanCast.Properties;

namespace EduLanCast.Views
{
    public partial class PanelForm : Form
    {
        private IList<int> Fps { get; }
        private IList<Thread> Threads { get; }
        private IList<bool> Switchs { get; }
        private Dictionary<string, int> ThreadIndex { get; }
        private int Interval { get; set; }
        private int Count { get; }
        private PanelController Controller { get; }

        public PanelForm()
        {
            InitializeComponent();
            Fps = new List<int> { 1, 2, 5, 10, 20, 50, 100 };
            Threads = new List<Thread>();
            Switchs = new List<bool> { true };
            ThreadIndex = new Dictionary<string, int>();
            Controller = new PanelController();
            Count = 0;
            ThreadIndex["CaptureScreen"] = Count++;
            var thread = new Thread(ShowScreen) { Name = "CaptureScreen" };
            Threads.Add(thread);
            CbFps.DataSource = Fps;
            Interval = (int)CbFps.SelectedItem;
            CbAdapters.DataSource = Controller.Adapters;
        }

        private void BtnDemo_Click(object sender, EventArgs e)
        {
            Threads[ThreadIndex["CaptureScreen"]].Start();
        }

        private void ShowScreen()
        {
            while (Switchs[ThreadIndex["CaptureScreen"]])
            {
                Thread.Sleep(Interval);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Switchs[ThreadIndex["CaptureScreen"]] = false;
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
            for (var i = 0; i < Switchs.Count; i++)
            {
                Switchs[i] = false;
            }
        }

        private async void CbOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblDpi.Text = CbOutputs.DataSource is null
                ? Resources.NullTip
                : await Controller.SelectOutput(CbOutputs.SelectedItem.ToString());
        }
    }
}
