using EduLanCastCore.Controllers.Utils;
using EduLanCastCore.Models.Configs;
using System;

namespace EduLanCastCore.Controllers.Threads
{
    public class PresentThread : ServiceThread
    {
        protected AppConfig Config;

        public PresentThread(ref AppConfig config)
        {
            Config = config;
        }

        public new void Start()
        {
            if (!Config.AllowInput) SystemUtil.BlockInput(true);
            SystemUtil.KeepScreenOn(true);
            base.Start();
        }

        public override void Operation()
        {
            while (true)
            {
                //Thread.Sleep(10000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public new void Interrupt()
        {
            base.Interrupt();
            SystemUtil.KeepScreenOn(false);
            if (!Config.AllowInput) SystemUtil.BlockInput(false);
        }

        public new void Dispose()
        {
            try
            {
                Interrupt();
            }
            catch (Exception )
            {
                // ignore
            }
            base.Dispose();
        }
    }
}
