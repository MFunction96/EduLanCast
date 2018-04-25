using EduLanCast.Controllers.Views;
using EduLanCast.Data;
using EduLanCast.Properties;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace EduLanCast.Views
{
    /// <inheritdoc />
    /// <summary>
    /// 面板窗体。
    /// </summary>
    public partial class PanelForm : Form
    {
        /// <summary>
        /// 刷新率列表。
        /// </summary>
        private IList<int> Fps { get; }
        /// <summary>
        /// PanelForm控制器。
        /// </summary>
        private PanelController Controller { get; }
        /// <inheritdoc />
        /// <summary>
        /// 面板窗体构造函数。
        /// </summary>
        public PanelForm()
        {
            InitializeComponent();
            StaticData.FormMgr.ManageObject[nameof(ParentForm)] = this;
            Fps = new List<int> { 1, 2, 5, 10, 20, 50, 100 };
            Controller = new PanelController();
            StaticData.ThreadMgr.ManageObject["ShowDuplication"] = new Thread(ShowDuplication);
            CbFps.DataSource = Fps;
            CbAdapters.DataSource = Controller.Adapters;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void BtnDemo_Click(object sender, EventArgs e)
        {
            Controller.InitDuplicate(CbAdapters.SelectedItem.ToString(), CbOutputs.SelectedItem.ToString());
            StaticData.ThreadMgr.ManageObject["Duplication"].Start();
            StaticData.ThreadMgr.ManageObject["ShowDuplication"].Start();
        }
        /// <summary>
        /// 显示屏幕复制。
        /// </summary>
        private void ShowDuplication()
        {
            PbScreen.Image = Controller.Screen;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStop_Click(object sender, EventArgs e)
        {
            StaticData.ThreadMgr.ManageObject["ShowDuplication"].Interrupt();
            StaticData.ThreadMgr.ManageObject["Duplication"].Interrupt();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CbAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Controller.QueryOutputsAsync(CbAdapters.SelectedItem.ToString());
            CbOutputs.DataSource = null;
            CbOutputs.DataSource = Controller.Outputs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbFps_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controller.Fps = (int)CbFps.SelectedItem;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PanelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StaticData.FormMgr.ManageObject.Remove(nameof(ParentForm));
            if (StaticData.FormMgr.ManageObject.Count == 0)
            {
                await StaticData.ThreadMgr.Terminate();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CbOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblDpi.Text = CbOutputs.DataSource is null
                ? Resources.NullTip
                : await Controller.SelectOutput(CbOutputs.SelectedItem.ToString());
        }
    }
}
