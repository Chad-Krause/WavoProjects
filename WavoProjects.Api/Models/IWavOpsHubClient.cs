using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels.QueryModels;

namespace WavoProjects.Api.Models
{
    public interface IWavOpsHubClient
    {
        public Task UpdateProjectBoard(List<PriorityView> projects);
        public Task ProjectDrag(ProjectDrag drag);
        public Task UpdateTimesheets(List<TimesheetTeamMember> teamMembers);
        public Task Refresh();
    }
}
