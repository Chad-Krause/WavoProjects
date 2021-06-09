using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.Models.UserController
{
    public class CheckEmailResponse
    {
        public bool Exists { get; set; }
        public CheckEmailResponse()
        {

        }

        public CheckEmailResponse(bool exists)
        {
            Exists = exists;
        }
    }
}
