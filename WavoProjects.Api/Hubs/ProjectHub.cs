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
    public class ProjectHub: Hub<IWavOpsClient>
    {
        private readonly WavoContext m_db;
        private readonly ILogger<ProjectHub> m_logger;
        public static string kProjectPageGroup = "PROJECT_PAGE";
        public static string kProjectDragGroup = "PROJECT_DRAG";

        public ProjectHub(WavoContext db, ILogger<ProjectHub> logger)
        {
            m_db = db;
            m_logger = logger;
        }

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
    }
}
