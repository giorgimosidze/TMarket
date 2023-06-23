using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TMarket.WEB.RequestModels;

namespace TMarket.WEB.Helpers.CustomMiddlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;


        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger<RequestResponseLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                var request = httpContext.Request;
                if (request.Path.StartsWithSegments(new PathString("/api")))
                {
                    var stopWatch = Stopwatch.StartNew();
                    var requestTime = DateTime.UtcNow;
                    var requestBodyContent = await ReadRequestBody(request);
                    var originalBodyStream = httpContext.Response.Body;
                    var ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
                    var localAddress = httpContext.Connection.LocalIpAddress.ToString();
                    var localPort = httpContext.Connection.LocalPort.ToString();
                    var remotePort = httpContext.Connection.RemotePort.ToString();
                    using (var responseBody = new MemoryStream())
                    {
                        var response = httpContext.Response;
                        response.Body = responseBody;
                        await _next(httpContext);
                        stopWatch.Stop();

                        string responseBodyContent = null;
                        responseBodyContent = await ReadResponseBody(response);
                        await responseBody.CopyToAsync(originalBodyStream);

                        SafeLog(requestTime,
                            stopWatch.ElapsedMilliseconds,
                            response.StatusCode,
                            request.Method,
                            request.Path,
                            request.QueryString.ToString(),
                            ipAddress,
                            localAddress,
                            localPort,
                            remotePort,
                            requestBodyContent,
                            responseBodyContent);
                    }
                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (Exception)
            {
                await _next(httpContext);
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private void SafeLog(DateTime requestTime,
                    long responseMillis,
                    int statusCode,
                    string method,
                    string path,
                    string queryString,
                    string ipAddress,
                    string localAddress,
                    string localPort,
                    string remotePort,
                    string requestBody,
                    string responseBody)
        {
            // TODO: როცა ლოგირებას დაამატებ, მერე ამოაკომენტარე!
            // if (path.ToLower().StartsWith("/api/login"))
            // {
            //     requestBody = "(Request logging disabled for /api/login)";
            //     responseBody = "(Response logging disabled for /api/login)";
            // }

            if (requestBody.Length > 200)
            {
                requestBody = $"(Truncated to 200 chars) {requestBody.Substring(0, 200)}";
            }

            if (responseBody.Length > 200)
            {
                responseBody = $"(Truncated to 200 chars) {responseBody.Substring(0, 200)}";
            }

            if (queryString.Length > 200)
            {
                queryString = $"(Truncated to 200 chars) {queryString.Substring(0, 200)}";
            }

            _logger.LogInformation(new ApiLogItem
            {
                RequestTime = requestTime,
                ResponseMillis = responseMillis,
                StatusCode = statusCode,
                Method = method,
                Path = path,
                IpAddress = ipAddress,
                LocalAdress = localAddress,
                LocalPort = localPort,
                RemotePort = remotePort,
                QueryString = queryString,
                RequestBody = requestBody,
                ResponseBody = responseBody
            }.ToString());
        }
    }
}
