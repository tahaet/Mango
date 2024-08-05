namespace Mango.Web.Models
{
    public class RequestDto
    {
        public string ApiType { get; set; } = "GET";
        public string Url { get; set; }
        public string Data { get; set; }
        public string AccesssToken { get; set; }
    }
}
