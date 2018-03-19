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
        /// <summary>
        /// 
        /// </summary>
        private IList<int> Fps { get; }
        /// <summary>
        /// 
        /// </summary>
        private PanelController Controller { get; }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public PanelForm()
        {
            InitializeComponent();
            Fps = new List<int> { 1, 2, 5, 10, 20, 50, 100 };
            Controller = new PanelController();
            ThreadManager.Threads["ShowDuplication"] = new Thread(ShowDuplication);
            CbFps.DataSource = Fps;
            CbAdapters.DataSource = Controller.Adapters;
        }

        private void BtnDemo_Click(object sender, EventArgs e)
        {
            Controller.InitDuplicate(CbAdapters.SelectedItem.ToString(), CbOutputs.SelectedItem.ToString());
            ThreadManager.Threads["Duplication"].Start();
            ThreadManager.Threads["ShowDuplication"].Start();
        }

        private void ShowDuplication()
        {
            PbScreen.Image = Controller.Screen;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            ThreadManager.Threads["ShowDuplication"].Interrupt();
            ThreadManager.Threads["Duplication"].Interrupt();
        }

        private async void CbAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Controller.QueryOutputsAsync(CbAdapters.SelectedItem.ToString());
            CbOutputs.DataSource = null;
            CbOutputs.DataSource = Controller.Outputs;
        }

        private void CbFps_SelectedIndexChanged(object sender, EventArgs e)
        {
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
