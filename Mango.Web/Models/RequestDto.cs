using static Mango.Web.Utility.SD;

namespace Mango.Web.Models
{
    public class RequestDto
    {
        public  ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public string Data { get; set; }
        public string AccesssToken { get; set; }
    }
}
