using EduLanCast.Controllers.Capturer;
using EduLanCast.Controllers.Record;
using System.Collections.Generic;

namespace EduLanCast.Controllers.Views
{
    public class PanelController
    {
        public ScreenRecorder Recorder { get; }

        public DesktopDuplication Duplication { get; set; }

        public IList<string> Adapters { get; }
        
        public PanelController()
        {
            Duplication = new DesktopDuplication();
            Recorder = new ScreenRecorder();
            Adapters = new List<string>();
            foreach (var adapter1 in Duplication.Adapters1)
            {
                Adapters.Add(adapter1.Description.AdapterDescription);
            }
        }
    }
}
