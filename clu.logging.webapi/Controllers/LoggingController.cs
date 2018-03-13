using clu.logging.log4net;

using Swashbuckle.Swagger.Annotations;

using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace clu.logging.webapi.controllers
{
    [AllowAnonymous]
    [RoutePrefix("Logging")]
    public class LoggingController : ApiController
    {
        [HttpPost]
        [Route("Debug")]
        [SwaggerResponse(HttpStatusCode.OK, "Debug message was logged successfully.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> PostDebugMessageAsync([FromBody] dynamic body)
        {
            try
            {
                string message = body.message;

                await Log4netLogger.Instance.LogDebugAsync(message);

                return Ok();
            }
            catch (Exception ex)
            {
                await Log4netLogger.Instance.LogErrorAsync("An error occurred trying to log debug message", ex);
                return InternalServerError(ex);
            }
        }
    }
}
