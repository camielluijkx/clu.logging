﻿using System.Web;
using System.Web.Http;

namespace clu.logging.webapi.net461
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
