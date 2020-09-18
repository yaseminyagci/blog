using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;

namespace Application.Middlewares
{
    public class LoggingMiddleware
    {
        readonly RequestDelegate _next;
        private readonly long _start;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _start = Stopwatch.GetTimestamp();
        }


        public async Task InvokeAsync(HttpContext httpContext, UserService userService)
        {

            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var user = await userService.GetUserByNameAsync(httpContext.User.Identity.Name).ConfigureAwait(false);

            // Push the user name into the log context so that it is included in all log entries
            if (user != null)
            {
                LogContext.PushProperty("UserId", user.Id);
                LogContext.PushProperty("UserName", user.UserName);
            }


            // Getting the request body is a little tricky because it's a stream
            // So, we need to read the stream and then rewind it back to the beginning
            await RequestLog(httpContext.Request).ConfigureAwait(false);


            // The response body is also a stream so we need to:
            // - hold a reference to the original response body stream
            // - re-point the response body to a new memory stream
            // - read the response body after the request is handled into our memory stream
            // - copy the response in the memory stream out to the original response stream
            await ResponseLog(httpContext).ConfigureAwait(false);
        }


        private async Task RequestLog(HttpRequest request)
        {
            request.EnableBuffering();
            Stream body = request.Body;
            byte[] buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
            var requestBody = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;
            Log.ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestBody", requestBody)
                .Information("Request information {RequestMethod} {RequestPath}", request.Method, request.Path);
        }
        private async Task ResponseLog(HttpContext httpContext)
        {
            HttpResponse response = httpContext.Response;
            await using var responseBodyMemoryStream = new MemoryStream();
            var originalResponseBodyReference = response.Body;
            response.Body = responseBodyMemoryStream;

            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                Guid errorId = Guid.NewGuid();
                Log.ForContext("Type", "Error")
                    .ForContext("Exception", exception, destructureObjects: true)
                    .Error(exception, exception.Message + ". {@errorId}", errorId);

                var result = JsonConvert.SerializeObject(new { error = "Sorry, an unexpected error has occurred", errorId });
                response.ContentType = "application/json";
                response.StatusCode = 500;
                await response.WriteAsync(result).ConfigureAwait(false);
            }

            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync().ConfigureAwait(false);
            response.Body.Seek(0, SeekOrigin.Begin);

            Log.ForContext("ResponseHeaders", response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()))
                .ForContext("ResponseBody", responseBody)
                .Information("Response information {RequestMethod} {RequestPath} {statusCode} {ElapsedTime} ms", httpContext.Request.Method, httpContext.Request.Path, response.StatusCode, GetElapsedMilliseconds(_start, Stopwatch.GetTimestamp()));

            await responseBodyMemoryStream.CopyToAsync(originalResponseBodyReference).ConfigureAwait(false);
        }
        private static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000L / (double)Stopwatch.Frequency;
        }


    }
}
