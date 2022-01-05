using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.DatabaseModels.QueryModels
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
            Projects = p.Projects.Select(i => new PriorityProject(i)).OrderBy(i => i.SortOrder).ToList();
            
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
            SortOrder = p.SortOrder;
            StartedOn = p.StartedOn;
            ProjectOwnerId = p.ProjectOwnerId;
            ProjectOwner = p.ProjectOwner;
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
        public int? SortOrder { get; set; }
        public int? ProjectOwnerId { get; set; }
        public TeamMember ProjectOwner { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

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

    public partial class PriorityViewConfiguration : IEntityTypeConfiguration<PriorityView>
    {
        public void Configure(EntityTypeBuilder<PriorityView> entity)
        { 
            entity.ToTable(nameof(PriorityView), t => t.ExcludeFromMigrations());
        }
    }

    public partial class PriorityProjectConfiguration : IEntityTypeConfiguration<PriorityProject>
    {
        public void Configure(EntityTypeBuilder<PriorityProject> entity)
        {
            entity.ToTable(nameof(PriorityProject), t => t.ExcludeFromMigrations());
        }
    }

    public partial class PriorityTeamConfiguration : IEntityTypeConfiguration<PriorityTeam>
    {
        public void Configure(EntityTypeBuilder<PriorityTeam> entity)
        {
            entity.ToTable(nameof(PriorityTeam), t => t.ExcludeFromMigrations());
        }
    }
}
