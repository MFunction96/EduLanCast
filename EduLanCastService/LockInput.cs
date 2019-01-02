using EduLanCastCore.Controllers.Utils;

namespace EduLanCastService
{
    public class SystemCtrl
    {
        public static void BlockInput(bool flag)
        {
            SystemUtil.BlockInput(flag);
        }
    }
}
