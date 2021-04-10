using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.Models
{
    public class ProjectDrag
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string ClientId { get; set; }
        public int ProjectId { get; set; }
    }
}
