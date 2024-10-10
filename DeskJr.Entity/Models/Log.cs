namespace DeskJr.Entity.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string? Level { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string? Exception { get; set; }
        public string? LogMessage { get; set; }
        public string? RequestMethod { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }
        public string? RequestUrl { get; set; }
        public int? StatusCode { get; set; }
        public string? Ip { get; set; }
    }

}
