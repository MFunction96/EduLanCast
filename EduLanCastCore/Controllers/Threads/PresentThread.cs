using EduLanCastCore.Services;
using System;
using EduLanCastCore.Models.Configs;
using EduLanCastCore.Services.Structures;

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
            NativeMethods.BlockInput(true);
            NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS |
                                                  EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                                  EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            base.Start();
        }

        public override void Operation()
        {
            throw new NotImplementedException();
        }

        public new void Interrupt()
        {
            base.Interrupt();
            NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            NativeMethods.BlockInput(false);
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
