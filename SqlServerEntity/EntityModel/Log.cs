namespace SqlServerEntity.EntityModel
{
    public class Log
    {
        public int LogId { get; set; }
        public string ThreadId { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string EventLevel { get; set; }
        public string UserName { get; set; }
        public string MachineName { get; set; }
        public int? PortfolioId { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorSource { get; set; }
        public string StackTrace { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string InnerErrorMessage { get; set; }
    }
}
