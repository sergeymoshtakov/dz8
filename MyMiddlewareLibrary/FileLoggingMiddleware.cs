using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace MyMiddlewareLibrary
{
    public class FileLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public FileLoggingMiddleware(RequestDelegate next, string logFilePath)
        {
            _next = next;
            _logFilePath = logFilePath;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestPath = context.Request.Path;
            var logMessage = $"Request Path: {requestPath}, Time: {DateTime.Now}";

            await WriteToLogFile(logMessage);

            await _next(context);
        }

        private async Task WriteToLogFile(string logMessage)
        {
            await File.AppendAllTextAsync(_logFilePath, logMessage + Environment.NewLine);
        }
    }
}
