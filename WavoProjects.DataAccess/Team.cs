using System;
using System.Collections.Generic;
using System.Text;

namespace WavoProjects.DataAccess
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual List<Project> Projects { get; set; }

    }
}
