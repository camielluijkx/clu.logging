using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace clu.logging.library.Api
{
    public abstract class RootApiController : ApiController
    {
        private readonly Assembly exposingAssembly;

        protected RootApiController()
        {
            exposingAssembly = Assembly.GetCallingAssembly();
        }

        protected RootApiController(Assembly exposingAssembly)
        {
            this.exposingAssembly = exposingAssembly;
        }

        public virtual HttpResponseMessage Get()
        {
            string applicationName = exposingAssembly.GetName().Name;
            string environmentName = Environment.MachineName;
            string buildnumber = exposingAssembly.GetName().Version.ToString();

            string version = $"{applicationName} {environmentName} - Build {buildnumber}";

            return Request.CreateResponse(HttpStatusCode.OK, version);
        }
    }
}
