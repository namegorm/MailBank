using System.Net;

namespace Core.Application.Models
{
    public class CoreResultModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        protected CoreResultModel() { }

        public static CoreResultModel Create(HttpStatusCode statusCode = HttpStatusCode.OK, string message = default, object data = default)
        {
            return new CoreResultModel
            {
                StatusCode = (int)statusCode,
                Message = message,
                Data = data
            };
        }
    }
}
