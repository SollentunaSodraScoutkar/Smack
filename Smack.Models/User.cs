using System.Collections.Generic;
using Nancy.Security;

namespace Smack.Models
{
    public class User : IUserIdentity
    {
        public string VarFirstName { get; set; }
        public string VarSurName { get; set; }
        public string VarUserName { get; set; }
        public string VarEmail { get; set; }
        public string VarPassword { get; set; }

        public string UserName => VarUserName;

        public IEnumerable<string> Claims => new string[] {};
    }
}
