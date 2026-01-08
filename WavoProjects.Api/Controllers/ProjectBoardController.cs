using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WavoProjects.Api.Hubs;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.Models;
using WavoProjects.Api.Models.Extensions;

namespace WavoProjects.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectBoardController : ControllerBase
    {
        private readonly ILogger<ProjectBoardController> m_logger;
        private WavoContext m_db;
        private readonly IHubContext<WavOpsHub, IWavOpsHubClient> m_projectHub;

        public ProjectBoardController(ILogger<ProjectBoardController> logger, WavoContext context, IHubContext<WavOpsHub, IWavOpsHubClient> hub)
        {
            m_logger = logger;
            m_db = context;
            m_projectHub = hub;
        }

        [HttpGet("test")]
        public string Test()
        {
            m_logger.LogInformation($"Test Api reached at {DateTime.Now}");
            return "test";
        }

        [HttpPost("UpdateProjectPriorityAndSortOrders")]
        public async Task<bool> UpdateProjectPriorityAndSortOrders([FromBody] UpdateProjectAndSortOrdersModel model)
        {
            m_logger.LogInformation($"UpdateProjectPriorityAndSortOrders projectId: {model.Id}");
            Project dbProject = await m_db.Projects.SingleAsync(i => i.Id == model.Id);
            dbProject.PriorityId = model.NewPriorityId;
            dbProject.SortOrder = model.SortOrders.Single(i => dbProject.Id == i.ProjectId).SortOrder;
            dbProject.UpdatedOn = DateTime.Now;

            if(dbProject.StartedOn == null)
            {
                dbProject.StartedOn = DateTime.Now;
            }

            if(model.NewPriorityId != 5)
            {
                List<Project> projects = await m_db.Projects.Where(i => i.PriorityId == model.NewPriorityId).ToListAsync();
                foreach(Project p in projects)
                {
                    p.SortOrder = model.SortOrders.Single(i => i.ProjectId == p.Id).SortOrder;
                }
            }
            

            await m_db.SaveChangesAsync();

            await SendProjectUpdate();
            
            return true;
        }

        [HttpPost("CreateOrUpdateProject")]
        public async Task CreateOrUpdateProject([FromBody] Project project)
        {
            bool isNewProject = !project.Id.HasValue || project.Id == 0;
            string action = isNewProject ? "Create" : "Update";
            m_logger.LogInformation($"CreateOrUpdateProject name: {project.Name}, action: {action}");

            if(isNewProject)
            {
                m_db.Projects.Add(new Project
                {
                    Name = project.Name,
                    Description = project.Description,
                    TeamId = project.TeamId,
                    PriorityId = 1,
                    SortOrder = 999999,
                    ProjectOwnerId = project.ProjectOwnerId,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                });
            } else
            {
                Project edit = await m_db.Projects.SingleAsync(i => i.Id == project.Id);
                edit.Name = project.Name;
                edit.Description = project.Description;
                edit.ProjectOwnerId = project.ProjectOwnerId;
                edit.TeamId = project.TeamId;
            }
            

            await m_db.SaveChangesAsync();
            await SendProjectUpdate();
        }

        private async Task SendProjectUpdate()
        {
            m_logger.LogInformation($"Sending SignalR Project Updates");
            await m_projectHub.Clients.Groups(WavOpsHub.kProjectPageGroup).UpdateProjectBoard(await m_db.GetProjectsByPriorityAsync());
        }
    }
}
