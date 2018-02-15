using EduLanCast.Controllers.Capturer;
using EduLanCast.Controllers.Record;
using System.Collections.Generic;
using System.Linq;

namespace EduLanCast.Controllers.Views
{
    public class PanelController
    {
        public ScreenRecorder Recorder { get; }

        public DesktopDuplication Duplication { get; set; }

        public IList<string> Adapters { get; }

        public IList<string> Outputs { get; }
        
        public PanelController()
        {
            Duplication = new DesktopDuplication();
            Recorder = new ScreenRecorder();
            Adapters = new List<string>();
            Outputs = new List<string>();
            RefrushAdapters();
            RefrushOutputs();
        }

        private void RefrushAdapters()
        {
            foreach (var adapter1 in Duplication.Adapters1)
            {
                Adapters.Add(adapter1.Description.AdapterDescription);
            }
        }

        private void RefrushOutputs()
        {
            if (Duplication.Outputs1 is null) return;
            foreach (var output1 in Duplication.Outputs1)
            {
                Outputs.Add(output1.Description.DeviceName);
            }
        }

        public void QueryOutputs(string adapter)
        {
            var q = Duplication.Adapters1.First(tmp => tmp.Description.AdapterDescription == adapter);
            Duplication.QueryOutputs(q);
            RefrushOutputs();
        }
    }
}
