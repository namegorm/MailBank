using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Core.Application.Models;

using Microsoft.AspNetCore.Http;

namespace Core.API.Middlewares
{
    public class CoreResultMiddleware
    {
        private readonly RequestDelegate _next;

        public CoreResultMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stream originalResponseBody = context.Response.Body;
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    context.Response.Body = memoryStream;

                    await _next(context);
                    memoryStream.Position = 0;

                    if (context.Response.StatusCode != (int)HttpStatusCode.OK)
                    {
                        CoreResultModel coreResultModel = null;
                        switch (context.Response.StatusCode)
                        {
                            case (int)HttpStatusCode.Unauthorized:
                                coreResultModel = CoreResultModel.Create((HttpStatusCode)context.Response.StatusCode, "Not authorized user.");
                                break;
                            default:
                                coreResultModel = CoreResultModel.Create((HttpStatusCode)context.Response.StatusCode, "An error occurred during execution.");
                                break;
                        }

                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.Response.Body.SetLength(0);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(coreResultModel));
                    }

                    memoryStream.Position = 0;
                    await context.Response.Body.CopyToAsync(originalResponseBody);
                    context.Response.Body = originalResponseBody;
                }

            }
            finally
            {
                context.Response.Body = originalResponseBody;
            }
        }
    }
}
