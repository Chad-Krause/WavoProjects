using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.DatabaseModels.QueryModels
{
    public class TimesheetTeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ClockedIn { get; set; }
    }
}
