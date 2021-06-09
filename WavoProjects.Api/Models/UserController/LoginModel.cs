using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.Models.UserController
{
    public class LoginModel
    {
        public string Email {
            get { return Email.Trim().ToLower(); }
            set { Email = value; }
        }
        public string Password { get; set; }

    }
}
