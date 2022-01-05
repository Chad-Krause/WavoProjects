using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels.QueryModels;

namespace WavoProjects.Api.DatabaseModels
{
    public static class StoredProcedureExtensions
    {
        /**
         * Gets the projects for the main view by priority. Does not get completed items
         */
        public static async Task<List<PriorityView>> GetProjectsByPriorityAsync(this WavoContext context)
        {
            var priorities = await context.Priorities.Include(i => i.Projects.Where(i => i.PriorityId != 5))
                                                        .ThenInclude(j => j.Team)
                                                    .Include(i => i.Projects.Where(i => i.PriorityId != 5))
                                                        .ThenInclude(j => j.ProjectOwner)
                                                    .Select(i => new PriorityView(i))
                                                    .ToListAsync();

            priorities.Single(i => i.Id == 5).Projects.Clear(); // remove elements from last list

            return priorities;
        }
    }
}
