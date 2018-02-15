using EduLanCast.Controllers.Views;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace EduLanCast.Views
{
    public partial class PanelForm : Form
    {
        private IList<int> Fps { get; }
        private IList<Thread> Threads { get; }
        private IList<bool> Switchs { get; }
        private Dictionary<string,int> Map { get; }
        private int Interval { get; set; }
        private int Count { get; set; }
        private PanelController Controller { get; }

        public PanelForm()
        {
            InitializeComponent();
            Fps = new List<int> { 1, 2, 5, 10, 20, 50, 100 };
            Threads = new List<Thread>();
            Switchs = new List<bool> {true};
            Map = new Dictionary<string, int>();
            Controller = new PanelController();
            Count = 0;
            Map["CaptureScreen"] = Count++;
            var thread = new Thread(ShowScreen) {Name = "CaptureScreen"};
            Threads.Add(thread);
            CbFps.DataSource = Fps;
            Interval = (int)CbFps.SelectedItem;
            CbAdapters.DataSource = Controller.Adapters;
        }

        private void BtnDemo_Click(object sender, EventArgs e)
        {
            Threads[Map["CaptureScreen"]].Start();
        }

        private void ShowScreen()
        {
            while (Switchs[Map["CaptureScreen"]])
            {
                Thread.Sleep(Interval);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Switchs[Map["CaptureScreen"]] = false;
        }

        private void CbAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CbFps_SelectedIndexChanged(object sender, EventArgs e)
        {
            Interval = (int)CbFps.SelectedItem;
            Controller.Duplication.Fps = (int) CbFps.SelectedItem;
            Controller.Recorder.Fps = (int) CbFps.SelectedItem;
        }

        private void PanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
