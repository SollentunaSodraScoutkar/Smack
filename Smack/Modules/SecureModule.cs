using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;

namespace Smack.Modules
{
    public class SecureModule : NancyModule
    {
        public SecureModule()
        {
            Initialize();
        }

        protected SecureModule(string path) : base(path)
        {
            Initialize();
        }

        private void Initialize()
        {
            //this.RequiresHttps();
            this.RequiresAuthentication();
        }
    }
}