using clu.logging.library.api.net461;

using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace clu.logging.webapi.controllers.net461
{
    [AllowAnonymous]
    [RoutePrefix("")]
    [ExcludeFromCodeCoverage]
    public class RootController : RootApiController
    {
        public RootController()
            : base(Assembly.GetExecutingAssembly())
        {

        }

        /// <summary>
        /// Returns API version.
        /// </summary>
        /// <returns>API version.</returns>
        [Route("")]
        [HttpGet]
        public override HttpResponseMessage Get()
        {
            return base.Get();
        }
    }
}
