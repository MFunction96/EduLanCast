using EduLanCastCore.Models.Configs;
using EduLanCastCore.Services;
using EduLanCastCore.Services.Enums;
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
            if (!Config.AllowInput) NativeMethods.BlockInput(true);
            NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS |
                                                  EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                                  EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            base.Start();
        }

        public override void Operation()
        {
            while (true)
            {
                //Thread.Sleep(10000);
            }
        }

        public new void Interrupt()
        {
            base.Interrupt();
            NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            if (!Config.AllowInput) NativeMethods.BlockInput(false);
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
