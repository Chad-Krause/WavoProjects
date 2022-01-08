using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.Models;

namespace WavoProjects.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReferenceController : ControllerBase
    {
        private readonly ILogger<ReferenceController> m_logger;
        private WavoContext m_db;
        public ReferenceController(ILogger<ReferenceController> logger, WavoContext context)
        {
            m_logger = logger;
            m_db = context;
        }

        [HttpGet("GetTeams")]
        public async Task<List<Team>> GetTeams()
        {
            m_logger.LogInformation($"GetTeams");
            return await m_db.Teams.Where(i => i.DeletedOn == null).ToListAsync();
        }

        [HttpGet("GetTeamMembersNameId")]
        public async Task<List<NameId>> GetTeamMembers()
        {
            m_logger.LogInformation($"GetTeamMembersNameId");
            return await m_db.TeamMembers.Where(i => i.DeletedOn == null)
                                        .OrderBy(i => i.Name)
                                        .Select(i => new NameId { Id = i.Id.Value, Name = i.Name })
                                        .ToListAsync();
        }
    }
}
