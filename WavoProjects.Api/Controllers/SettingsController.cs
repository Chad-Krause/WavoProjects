using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.DatabaseModels.QueryModels;
using WavoProjects.Api.Hubs;
using WavoProjects.Api.Models;
using static BCrypt.Net.BCrypt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WavoProjects.Api.Controllers
{
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        private readonly ILogger<SettingsController> m_logger;
        private WavoContext m_db;
        private readonly IHubContext<WavOpsHub, IWavOpsHubClient> m_hub;

        public SettingsController(ILogger<SettingsController> logger, WavoContext db, IHubContext<WavOpsHub, IWavOpsHubClient> hub)
        {
            m_logger = logger;
            m_db = db;
            m_hub = hub;
        }

        [HttpGet("GetProjects")]
        public async Task<List<Project>> GetProjects()
        {
            m_logger.LogInformation($"GetProjects");

            return await m_db.Projects.Include(i => i.ProjectOwner)
                                    .Include(i => i.Team)
                                    .Include(i => i.Priority)
                                    .Where(i => i.PriorityId != 5)
                                    .OrderBy(i => i.Name)
                                    .ToListAsync();
        }

        [HttpDelete("DeleteProject")]
        public async Task<bool> DeleteProject(int Id)
        {
            m_logger.LogInformation($"DeleteProject - Id: {Id}");

            Project delete = await m_db.Projects.SingleAsync(i => i.Id == Id);
            m_db.Remove(delete);
            await m_db.SaveChangesAsync();

            await SendProjectUpdate();

            return true;
        }

        [HttpPost("CreateOrUpdateTeam")]
        public async Task<bool> CreateOrUpdateTeam([FromBody] Team team)
        {
            string action = team.Id.HasValue ? "Edit" : "Create";
            m_logger.LogInformation($"CreateOrUpdateTeam - Action: {action}");
            
            if(!team.Id.HasValue) // Create
            {
                Team newTeam = new Team
                {
                    Name = team.Name,
                    Color = team.Color,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };
                await m_db.Teams.AddAsync(newTeam);
            } else // Edit
            {
                Team edit = await m_db.Teams.SingleAsync(i => i.Id == team.Id);
                edit.Color = team.Color;
                edit.Name = team.Name;
                edit.UpdatedOn = DateTime.Now;
            }

            await m_db.SaveChangesAsync();

            await SendProjectUpdate();

            return true;
        }

        [HttpDelete("DeleteTeam")]
        public async Task<bool> DeleteTeam(int Id)
        {
            m_logger.LogInformation($"DeleteTeam - Id: {Id}");

            Team delete = await m_db.Teams.SingleAsync(i => i.Id == Id);
            delete.DeletedOn = DateTime.Now;
            await m_db.SaveChangesAsync();

            return true;
        }

        [HttpGet("GetTeamMembersWithTimesheetInformation")]
        public async Task<List<TeamMemberExtendedInformation>> GetTeamMembersWithTimesheetInformation()
        {
            m_logger.LogInformation($"GetTeamMembersWithTimesheetInformation");
            return await m_db.GetTeamMembersWithTimesheetInfo();
        }

        [HttpPost("CreateOrUpdateTeamMember")]
        public async Task<bool> CreateOrUpdateTeamMember([FromBody] TeamMember tm)
        {
            string action = tm.Id.HasValue ? "Edit" : "Create";
            m_logger.LogInformation($"CreateOrUpdateTeamMember - Action: {action}");

            if (!tm.Id.HasValue) // Create
            {
                TeamMember newTeamMember = new TeamMember
                {
                    Name = tm.Name,
                    TrackTime = tm.TrackTime,
                    HoursAdjustment = tm.HoursAdjustment,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

                if(tm.Pin.Length > 0)
                {
                    newTeamMember.Pin = HashPassword(tm.Pin);
                }

                await m_db.TeamMembers.AddAsync(newTeamMember);
            }
            else // Edit
            {
                TeamMember edit = await m_db.TeamMembers.SingleAsync(i => i.Id == tm.Id);
                edit.Name = tm.Name;
                edit.TrackTime = tm.TrackTime;
                edit.HoursAdjustment = tm.HoursAdjustment;
                
                if(tm.Pin.Length > 0)
                {
                    edit.Pin = HashPassword(tm.Pin);
                } 

                edit.UpdatedOn = DateTime.Now;
            }

            await m_db.SaveChangesAsync();

            await SendTimesheetUpdate();

            return true;
        }

        [HttpDelete("DeleteTeamMember")]
        public async Task<bool> DeleteTeamMember(int Id)
        {
            m_logger.LogInformation($"DeleteTeamMember - Id: {Id}");

            TeamMember delete = await m_db.TeamMembers.SingleAsync(i => i.Id == Id);
            delete.DeletedOn = DateTime.Now;
            await m_db.SaveChangesAsync();

            await SendTimesheetUpdate();

            return true;
        }

        [HttpPost("ChangePin")]
        public async Task<bool> ChangePin([FromBody] ChangePinModel pin)
        {
            m_logger.LogInformation($"ChangePin - Id: {pin.Id}");
            TeamMember tm = await m_db.TeamMembers.SingleAsync(i => i.Id == pin.Id);

            if(pin.Pin.Length > 0)
            {
                tm.Pin = HashPassword(pin.Pin);
            }

            await m_db.SaveChangesAsync();

            return true;
        }

        [HttpGet("GetTimesheets")]
        public async Task<List<Timesheet>> GetTimesheets(int teamMemberId)
        {
            m_logger.LogInformation($"GetTimesheets - Id: {teamMemberId}");
            return await m_db.Timesheets.Where(i => i.TeamMemberId == teamMemberId && i.DeletedOn == null).ToListAsync();
        }

        [HttpPost("CreateOrUpdateTimesheet")]
        public async Task<bool> CreateOrUpdateTimesheet([FromBody] Timesheet ts)
        {
            string action = ts.Id.HasValue ? "Edit" : "Create";
            m_logger.LogInformation($"CreateOrUpdateTtimesheet - Action: {action}");

            if (!ts.Id.HasValue) // Create
            {
                Timesheet newTimesheet = new Timesheet
                {
                    TeamMemberId = ts.TeamMemberId,
                    ClockIn = ts.ClockIn,
                    ClockOut = ts.ClockOut,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

                await m_db.Timesheets.AddAsync(newTimesheet);
            }
            else // Edit
            {
                Timesheet edit = await m_db.Timesheets.SingleAsync(i => i.Id == ts.Id);
                edit.ClockIn = ts.ClockIn;
                edit.ClockOut = ts.ClockOut;
                edit.AutoClockOut = false;
                edit.UpdatedOn = DateTime.Now;
            }

            await m_db.SaveChangesAsync();

            await SendTimesheetUpdate(); // This could affect whether the user is clocked in or not

            return true;
        }

        [HttpDelete("DeleteTimesheet")]
        public async Task<bool> DeleteTimesheet(int Id)
        {
            m_logger.LogInformation($"DeleteTimesheet - Id: {Id}");

            Timesheet delete = await m_db.Timesheets.SingleAsync(i => i.Id == Id);
            delete.DeletedOn = DateTime.Now;
            await m_db.SaveChangesAsync();

            return true;
        }


        private async Task SendTimesheetUpdate()
        {
            m_logger.LogInformation($"Sending SignalR Timesheet Updates");
            await m_hub.Clients.Groups(WavOpsHub.kTimesheetGroup).UpdateTimesheets(await m_db.GetTimesheetTeamMembers());
        }

        private async Task SendProjectUpdate()
        {
            m_logger.LogInformation($"Sending SignalR Project Updates");
            await m_hub.Clients.Groups(WavOpsHub.kProjectPageGroup).UpdateProjectBoard(await m_db.GetProjectsByPriorityAsync());
        }

    }
}
