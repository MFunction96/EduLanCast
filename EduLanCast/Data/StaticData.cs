using EduLanCast.Controllers.Managers;

namespace EduLanCast.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class StaticData
    {
        /// <summary>
        /// 
        /// </summary>
        public static ThreadManager ThreadMgr { get; }
        /// <summary>
        /// 
        /// </summary>
        public static FormManager FormMgr { get; }
        /// <summary>
        /// 
        /// </summary>
        static StaticData()
        {
            ThreadMgr = new ThreadManager();
            FormMgr = new FormManager();
        }
    }
}
