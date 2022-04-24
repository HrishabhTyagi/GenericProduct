using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RequestModel
{
    public class UserSessionTrackingRequest
    {
        public int UserId { get; set; }
        public string AuthenticationToken { get; set; }
        public DateTime TokenTime { get; set; } = DateTime.Now;
        public string Machine { get; set; } = System.Environment.MachineName;
        public string DeviceType { get; set; }
        public string Browser { get; set; }
        public string OperatingSystem { get; set; }
        public string IpAddress { get; set; }
        public string BrowserVersion { get; set; }
    }
}
