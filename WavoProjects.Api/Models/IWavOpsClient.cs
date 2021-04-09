using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.Models.QueryModels;

namespace WavoProjects.Api.Models
{
    public interface IWavOpsClient
    {
        public Task UpdateProjectBoard(List<PriorityView> projects);
    }
}
