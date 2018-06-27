
using EduLanCastCore.Controllers.Drawcontrol.DrawingFunc;
using EduLanCastCore.Controllers.Managers;
using SharpDX.Mathematics.Interop;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EduLanCastCore.Models.Drawmodel;

namespace EduLanCastCore.Controllers.Drawcontrol.DrawingThrd
{
    public class DrawingStart:ThreadManager
    {
        public DrawingStart(Form form)
        {
            Canvainfo.Height = form.Height;
            Canvainfo.Width = form.Width;
            Canvainfo.HandPtr = form.Handle;
            //默认为Chalk Line:2
            Tooltype.Type = 1;
            Tooltype.Line = 2;
            Thread t = new Thread(threadmethod);
            StartCore(t);
            form.Show();
            Application.Run(form);
        }

        sealed override
        protected Task StartCore(Thread thread)
        {
            return Task.Run(()=> { thread.Start(); });
        }
        override
            protected Task TerminateCore(Thread thread)
        {
            return Task.Run(() => { thread.Interrupt(); });
        }
        private void threadmethod()
        {
            Initdetail initdetail = new Initdetail(Canvainfo.HandPtr,Canvainfo.Height,Canvainfo.Width);
            Chalk chalk = new Chalk(initdetail, new RawColor4(0f,0f, 0f,0.5f));
            Eraser eraser = new Eraser(initdetail,new RawColor4(0f,0f,0f,0.5f));
            chalk.Cleancanvas();
            while (true) {
                if (Pointtrace.Flag == 1)
                {
                    switch (Tooltype.Type)
                    {
                        case 1: chalk.Render(Pointtrace.GetAllPoint(), Pointtrace.GetPointlist()); break;
                        case 2: eraser.Render(Pointtrace.GetAllPoint(), Pointtrace.GetPointlist()); break;
                        case 3: chalk.Cleancanvas(); Tooltype.Type = Tooltype.Lasttype; break;

                    }
                }
                else {
                    switch (Tooltype.Type) {
                        case 3:
                            chalk.Cleancanvas();
                            Tooltype.Type = Tooltype.Lasttype;break;
                    }
                }
                Thread.Sleep(1);
            }

        }
    }
}
