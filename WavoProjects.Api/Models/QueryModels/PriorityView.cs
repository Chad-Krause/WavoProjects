using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.Models.QueryModels
{
    public class PriorityView
    {
        public PriorityView()
        {

        }

        public PriorityView(Priority p)
        {
            Id = p.Id;
            Name = p.Name;
            Projects = p.Projects.Select(i => new PriorityProject(i)).ToList();
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PriorityProject> Projects { get; set; }
    }
    
    public class PriorityProject
    {
        public PriorityProject()
        {

        }

        public PriorityProject(Project p)
        {
            Id = p.Id;
            Name = p.Name;
            Description = p.Description;
            TeamId = p.TeamId;
            PriorityId = p.PriorityId;
            StartedOn = p.StartedOn;
            CreatedOn = p.CreatedOn;
            UpdatedOn = p.UpdatedOn;

            if(p.Team != null)
            {
                Team = new PriorityTeam(p.Team);
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TeamId { get; set; }
        public int? PriorityId { get; set; }
        public DateTimeOffset? StartedOn { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }

        public virtual PriorityTeam Team { get; set; }
    }

    public class PriorityTeam
    {
        public PriorityTeam()
        {

        }

        public PriorityTeam(Team t)
        {
            Id = t.Id;
            Name = t.Name;
            Color = t.Color;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
