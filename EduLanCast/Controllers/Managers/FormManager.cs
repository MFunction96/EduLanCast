using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduLanCast.Controllers.Managers
{
    public class FormManager : Manager<Form>
    {
        protected override Task StartCore(Form obj)
        {
            return Task.Run(() => { obj.Show(); });
        }

        protected override Task TerminateCore(Form obj)
        {
            return Task.Run(() => { obj.Close(); });
        }
    }
}
