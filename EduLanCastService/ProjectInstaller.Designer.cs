namespace EduLanCastService
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EduLanCastServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EduLanCastServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // EduLanCastServiceProcessInstaller
            // 
            this.EduLanCastServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.EduLanCastServiceProcessInstaller.Password = null;
            this.EduLanCastServiceProcessInstaller.Username = null;
            // 
            // EduLanCastServiceInstaller
            // 
            this.EduLanCastServiceInstaller.DisplayName = "EduLanCastService";
            this.EduLanCastServiceInstaller.ServiceName = "EduLanCastService";
            this.EduLanCastServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EduLanCastServiceProcessInstaller,
            this.EduLanCastServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EduLanCastServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EduLanCastServiceInstaller;
    }
}