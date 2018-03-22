using clu.logging.library.extensions.net461;
using clu.logging.log4net.net461;

using Swashbuckle.Swagger.Annotations;

using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace clu.logging.webapi.controllers.net461
{
    [AllowAnonymous]
    [RoutePrefix("Logging")]
    public class LoggingController : ApiController
    {
        private enum LogLevel
        {
            Debug,
            Error,
            Fatal,
            Info,
            Warn
        }

        private class LogRequest
        {
            public LogLevel Level { get; }

            public string Message { get; }

            public LogRequest(LogLevel level, dynamic body)
            {
                Level = level;

                if (body == null)
                {
                    throw new ArgumentNullException(nameof(body));
                }

                if (body.message == null)
                {
                    throw new ArgumentNullException(nameof(body.message));
                }

                Message = body.message;
            }
        }

        private class LogResponse
        {
            public bool Success { get; }

            public string Message { get; }

            public LogResponse(bool success, string message)
            {
                Success = success;
                Message = message;
            }
        }

        private async Task<LogResponse> LogMessageAsync(LogRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                switch (request.Level)
                {
                    case LogLevel.Debug:
                    {
                        await Log4netLogger.Instance.LogDebugAsync(request.Message);
                        break;
                    }
                    case LogLevel.Error:
                    {
                        await Log4netLogger.Instance.LogErrorAsync(request.Message);
                        break;
                    }
                    case LogLevel.Fatal:
                    {
                        await Log4netLogger.Instance.LogFatalAsync(request.Message);
                        break;
                    }
                    case LogLevel.Info:
                    {
                        await Log4netLogger.Instance.LogInformationAsync(request.Message);
                        break;
                    }
                    case LogLevel.Warn:
                    {
                        await Log4netLogger.Instance.LogWarningAsync(request.Message);
                        break;
                    }

                    default: 
                    {
                        return new LogResponse(false, $"Log level {request.Level} is not supported!");
                    }
                }

                return new LogResponse(true, "Message was logged succesfully!");
            }
            catch (Exception ex)
            {
                return new LogResponse(false, $"An error occurred trying to log debug message: {ex.ToExceptionMessage()}");
            }
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="body">Body with message.</param>
        /// <returns>API response.</returns>
        [HttpPost]
        [Route("Debug")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(OkResult))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> LogDebugMessageAsync([FromBody] dynamic body)
        {
            var response = await LogMessageAsync(new LogRequest(LogLevel.Debug, body));
            if (response.Success)
            {
                return Ok();
            }
            return Content(HttpStatusCode.InternalServerError, response.Message);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="body">Body with message.</param>
        /// <returns>API response.</returns>
        [HttpPost]
        [Route("Error")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(OkResult))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> LogErrorMessageAsync([FromBody] dynamic body)
        {
            var response = await LogMessageAsync(new LogRequest(LogLevel.Error, body));
            if (response.Success)
            {
                return Ok();
            }
            return Content(HttpStatusCode.InternalServerError, response.Message);
        }

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="body">Body with message.</param>
        /// <returns>API response.</returns>
        [HttpPost]
        [Route("Fatal")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(OkResult))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> LogMessageAsync([FromBody] dynamic body)
        {
            var response = await LogMessageAsync(new LogRequest(LogLevel.Fatal, body));
            if (response.Success)
            {
                return Ok();
            }
            return Content(HttpStatusCode.InternalServerError, response.Message);
        }

        /// <summary>
        /// Logs an info message.
        /// </summary>
        /// <param name="body">Body with message.</param>
        /// <returns>API response.</returns>
        [HttpPost]
        [Route("Info")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(OkResult))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> LogInfoMessageAsync([FromBody] dynamic body)
        {
            var response = await LogMessageAsync(new LogRequest(LogLevel.Info, body));
            if (response.Success)
            {
                return Ok();
            }
            return Content(HttpStatusCode.InternalServerError, response.Message);
        }

        /// <summary>
        /// Logs a warn message.
        /// </summary>
        /// <param name="body">Body with message.</param>
        /// <returns>API response.</returns>
        [HttpPost]
        [Route("Warn")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(OkResult))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> LogWarnMessageAsync([FromBody] dynamic body)
        {
            var response = await LogMessageAsync(new LogRequest(LogLevel.Warn, body));
            if (response.Success)
            {
                return Ok();
            }
            return Content(HttpStatusCode.InternalServerError, response.Message);
        }
    }
}
