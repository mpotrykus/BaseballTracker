using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
[assembly: OwinStartup(typeof(MK.BaseballTracker.API.Startup))]

namespace MK.BaseballTracker.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }

        private void ConfigureAuth(IAppBuilder app)
        {
            //throw new NotImplementedException();
        }
    }
}