using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.Models.Extensions
{
    public static class StoredProcedureExtensions
    {
        /**
         * Gets the projects for the main view by priority. Does not get completed items
         */
        public static async Task<List<Priority>> GetProjectsByPriorityAsync(this WavoContext context)
        {
            return await context.Priorities.Include(i => i.Projects)
                                                .ThenInclude(j => j.Team)
                                            .Where(i => i.Id != 5)
                                            .ToListAsync();
        }
    }
}
