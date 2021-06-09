using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels;

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
            return await m_db.Teams.ToListAsync();
        }
    }
}
