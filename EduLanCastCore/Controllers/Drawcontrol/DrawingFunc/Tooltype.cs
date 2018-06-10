namespace EduLanCastCore.Controllers.Drawcontrol.DrawingFunc
{
    /// <summary>
    /// 画图工具类型及其属性
    /// </summary>
    class Tooltype
    {
        /// <summary>
        /// Type:
        /// 1--画笔
        /// 2--橡皮擦
        /// 3--页面清除
        /// 4--画圆
        /// 5--画矩形
        ///
        /// </summary>
        public static int Type { get; set; }
        public static int Lasttype { get; set; }
        public static int Line { get; set; }

        public static string Getfx(int t) {
            switch (t) {
                case 1:return "chalk.fx";
                case 2:return "eraser.fx";
            }
            return null;
        }
        public static bool IsChalkorEra()
        {
            return Type == 1 || Type == 2;
        }
        public static bool IsShape()
        {
            return Type == 4 || Type == 5;
        }
        public static bool IsonlyClickTool() {
            return Type == 3;
        }
    }
}