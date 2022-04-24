namespace SqlServerEntity.EntityModel
{
    public partial class UserSessionTracking
    {
        public int UserSessionTrackingId { get; set; }
        public int UserId { get; set; }
        public string AuthenticationToken { get; set; }
        public DateTime TokenTime { get; set; }
        public string Machine { get; set; }
        public string DeviceType { get; set; }
        public string Browser { get; set; }
        public string OperatingSystem { get; set; }
        public string BrowserVersion { get; set; }
        public string IpAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
