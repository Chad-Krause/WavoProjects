using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.Models.QueryModels;

namespace WavoProjects.Api.Models
{
    public static class StoredProcedureExtensions
    {
        /**
         * Gets the projects for the main view by priority. Does not get completed items
         */
        public static async Task<List<PriorityView>> GetProjectsByPriorityAsync(this WavoContext context)
        {
            return await context.Priorities.Include(i => i.Projects)
                                                .ThenInclude(j => j.Team)
                                            .Where(i => i.Id != 5)
                                            .Select(i => new PriorityView(i))
                                            .ToListAsync();
        }
    }
}
