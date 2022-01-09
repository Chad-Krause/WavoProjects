using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.Hubs;
using WavoProjects.Api.Models;

namespace WavoProjects.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimesheetController : ControllerBase
    {
        private readonly ILogger<SettingsController> m_logger;
        private WavoContext m_db;
        private readonly IHubContext<WavOpsHub, IWavOpsHubClient> m_hub;

        public TimesheetController(ILogger<SettingsController> logger, WavoContext db, IHubContext<WavOpsHub, IWavOpsHubClient> hub)
        {
            m_logger = logger;
            m_db = db;
            m_hub = hub;
        }

        [HttpPost("ClockIn")]
        public async Task ClockIn([FromBody]int teamMemberId)
        {
            m_logger.LogInformation($"ClockIn - teamMemberId: {teamMemberId}");

            int clockedInRecords = await m_db.Timesheets.CountAsync(i => i.ClockOut == null && i.TeamMemberId == teamMemberId);

            if(clockedInRecords > 0)
            {
                throw new Exception($"Team Member already clocked in!");
            }

            await m_db.Timesheets.AddAsync(new Timesheet
            {
                TeamMemberId = teamMemberId,
                ClockIn = DateTime.Now,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            });

            await m_db.SaveChangesAsync();

            await SendTimesheetUpdate();
        }

        [HttpPost("ClockOut")]
        public async Task ClockOut([FromBody]int teamMemberId)
        {
            m_logger.LogInformation($"ClockOut - teamMemberId: {teamMemberId}");

            var record = await m_db.Timesheets.SingleOrDefaultAsync(i => i.TeamMemberId == teamMemberId && i.ClockOut == null);

            if(record.Id == null)
            {
                throw new Exception("Team Member not clocked in!");
            }

            record.ClockOut = DateTime.Now;
            record.UpdatedOn = DateTime.Now;

            await m_db.SaveChangesAsync();

            await SendTimesheetUpdate();
        }

        private async Task SendTimesheetUpdate()
        {
            m_logger.LogInformation($"Sending SignalR Timesheet Updates");
            await m_hub.Clients.Groups(WavOpsHub.kTimesheetGroup).UpdateTimesheets(await m_db.GetTimesheetTeamMembers());
        }
    }
}
