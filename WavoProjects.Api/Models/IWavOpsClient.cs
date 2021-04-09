using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.Models
{
    public interface IWavOpsClient
    {
        public Task UpdateProjectBoard(List<Priority> projects);
    }
}
