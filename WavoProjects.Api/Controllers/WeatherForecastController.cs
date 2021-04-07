using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.Models;

namespace WavoProjects.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private WavoContext m_db;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WavoContext context)
        {
            _logger = logger;
            m_db = context;
        }

        [HttpGet]
        public async Task<List<Team>> Get()
        {
            return await m_db.Teams.ToListAsync();
        }
    }
}
