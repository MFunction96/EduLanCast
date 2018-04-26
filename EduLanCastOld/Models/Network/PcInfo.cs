using System.Net;

namespace EduLanCastOld.Models.Network
{
    public class PcInfo
    {
        public string PcName { get; set; }
        public IPAddress LocalAddress { get; set; }
        public IPAddress MaskAddress { get; set; }
        public IPAddress DnsAddress1 { get; set; }
        public IPAddress DnsAddress2 { get; set; }

        public PcInfo()
        {
            //Todo

        }
    }
}
