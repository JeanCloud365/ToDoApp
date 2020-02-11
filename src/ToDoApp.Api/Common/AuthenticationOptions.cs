using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Api.Common
{
    public class AuthenticationOptions
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
    }
}
