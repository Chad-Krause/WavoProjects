using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels;

namespace WavoProjects.Api.Models.UserController
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}
