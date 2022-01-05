using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WavoProjects.Api.DatabaseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WavoProjects.Api.Controllers
{
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        private readonly ILogger<SettingsController> m_logger;
        private WavoContext m_db;

        public SettingsController(ILogger<SettingsController> logger, WavoContext db)
        {
            m_logger = logger;
            m_db = db;
        }

        [HttpGet("GetProjects")]
        public async Task<List<Project>> GetProjectsAsync(int id)
        {
            return await m_db.Projects.Include(i => i.ProjectOwner)
                                    .Include(i => i.Team)
                                    .Include(i => i.Priority)
                                    .Where(i => i.PriorityId != 5)
                                    .OrderBy(i => i.Name)
                                    .ToListAsync();
        }

        
    }
}
