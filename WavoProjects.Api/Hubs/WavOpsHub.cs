using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.Models;

namespace WavoProjects.Api.Hubs
{
    public class WavOpsHub: Hub<IWavOpsHubClient>
    {
        private readonly WavoContext m_db;
        private readonly ILogger<WavOpsHub> m_logger;
        public static string kProjectPageGroup = "PROJECT_PAGE";
        public static string kProjectDragGroup = "PROJECT_DRAG";
        public static string kTimesheetGroup = "TIMESHEETS";

        public WavOpsHub(WavoContext db, ILogger<WavOpsHub> logger)
        {
            m_db = db;
            m_logger = logger;
        }

        #region Management
        public override async Task<Task> OnConnectedAsync()
        {
            try
            {
                await m_db.HubClients.AddAsync(new HubClient
                {
                    Id = Context.ConnectionId,
                    ConnectedOn = DateTime.Now,
                });
                await m_db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                m_logger.LogError(e, $"Error saving client {Context.ConnectionId} to DB!");
            }
            return base.OnConnectedAsync();
        }

        public override async Task<Task> OnDisconnectedAsync(Exception exception)
        {
            try
            {
                HubClient client = await m_db.HubClients.SingleAsync(i => i.Id == Context.ConnectionId);
                client.DisconnectedOn = DateTime.Now;
                await m_db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                m_logger.LogError(e, $"Error saving client {Context.ConnectionId} to DB!");
            }

            return base.OnDisconnectedAsync(exception);
        }
        #endregion

        #region Project Board
        public async Task SubscribeToProjectPage()
        {
            m_logger.LogInformation($"Adding client {Context.ConnectionId} to project board group");
            await Groups.AddToGroupAsync(Context.ConnectionId, kProjectPageGroup);
            await Clients.Caller.UpdateProjectBoard(await m_db.GetProjectsByPriorityAsync());
        }

        public async Task UnsubscribeToProjectPage()
        {
            m_logger.LogInformation($"Removing client {Context.ConnectionId} from project board group");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, kProjectPageGroup);
        }
        #endregion

        #region Project Drags
        public async Task SubscribeToProjectDrags()
        {
            m_logger.LogInformation($"Adding client {Context.ConnectionId} to project drag group");
            await Groups.AddToGroupAsync(Context.ConnectionId, kProjectDragGroup);
        }

        public async Task UnsubscribeToProjectDrags()
        {
            m_logger.LogInformation($"Removing client {Context.ConnectionId} from project drag group");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, kProjectDragGroup);
        }

        public async Task DragProject(ProjectDrag drag)
        {
            drag.ClientId = Context.ConnectionId;
            await Clients.OthersInGroup(kProjectDragGroup).ProjectDrag(drag);
        }
        #endregion

        #region Timesheet
        public async Task SubscribeToTimesheetUpdates()
        {
            m_logger.LogInformation($"Adding client {Context.ConnectionId} to timesheet group");
            await Groups.AddToGroupAsync(Context.ConnectionId, kTimesheetGroup);
            await Clients.Caller.UpdateTimesheets(await m_db.GetTimesheetTeamMembers());
        }

        public async Task UnsubscribeFromTimesheetUpdates()
        {
            m_logger.LogInformation($"Removing client {Context.ConnectionId} from timesheet group");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, kTimesheetGroup);
        }
        #endregion
    }
}
