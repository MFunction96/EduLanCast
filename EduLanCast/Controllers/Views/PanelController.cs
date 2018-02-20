using EduLanCast.Controllers.Capturer;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace EduLanCast.Controllers.Views
{
    public class PanelController
    {
        private DesktopDuplication Duplication { get; }

        public int Fps { get; set; }

        public IList<string> Adapters { get; }

        public IList<string> Outputs { get; }

        public PanelController()
        {
            Duplication = new DesktopDuplication();
            Adapters = new List<string>();
            Outputs = new List<string>();
            RefrushAdapters();
            RefrushOutputs();
        }

        private void RefrushAdapters()
        {
            Adapters.Clear();
            foreach (var adapter1 in Duplication.Adapters1)
            {
                Adapters.Add(adapter1.Description1.Description);
            }
        }

        public async Task QueryOutputsAsync(string adapter)
        {
            var q = Duplication.Adapters1.First(tmp => tmp.Description1.Description == adapter);
            Duplication.QueryOutputs(q);
            await RefrushOutputs();
        }

        private Task RefrushOutputs()
        {
            return Task.Run(() =>
            {
                Outputs.Clear();
                if (Duplication.Outputs is null) return;
                foreach (var output1 in Duplication.Outputs)
                {
                    Outputs.Add(output1.Description.DeviceName);
                }
            });
        }

        public Task<string> SelectOutput(string output)
        {
            return Task.Run(() =>
            {
                if (Duplication.Outputs is null)
                {
                    return "null";
                }
                var output1 = Duplication.Outputs.First(tmp => tmp.Description.DeviceName == output);
                var screen = Duplication.SelectOutput(output1);
                return $"{screen.Width}x{screen.Height}";
            });
        }
    }
}
