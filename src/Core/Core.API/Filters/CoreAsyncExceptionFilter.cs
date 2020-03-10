using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Core.Application.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.API.Filters
{
    public class CoreAsyncExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var coreModelResult = CoreResultModel.Create(HttpStatusCode.OK, context.Exception.Message);
            await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(coreModelResult));
        }
    }
}
