using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WavoProjects.Api.DatabaseModels;

namespace WavoProjects.Api.DatabaseModels
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TeamId { get; set; }
        public int? PriorityId { get; set; }
        public int? SortOrder { get; set; }
        public DateTime? StartedOn { get; set; }
        public int? ProjectOwnerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Team Team { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual TeamMember ProjectOwner { get; set; }
    }

    public partial class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entity)
        {
            entity.Property(i => i.Id).ValueGeneratedOnAdd();
            entity.Property(i => i.Name).HasMaxLength(500);
            entity.Property(i => i.Description).HasMaxLength(2000);
            entity.ToTable("Project");


            entity.HasData(
                new Project { Id = 1, Name = "T-Shirt Cannon Robot", Description = "T-Shirt Cannon Robot", TeamId = 3, PriorityId = 1, SortOrder = 1, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now },
                new Project { Id = 2, Name = "Sponsor Thank Yous", Description = "Sponsor Thank Yous", TeamId = 1, PriorityId = 1, SortOrder = 2, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now },
                new Project { Id = 3, Name = "Chairman's Presentation", Description = "Chairman's Presentation", TeamId = 2, PriorityId = 1, SortOrder = 3, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now },
                new Project { Id = 4, Name = "Organize Electrical Toolbox", Description = "Organize Electrical Toolbox", TeamId = 4, PriorityId = 1, SortOrder = 4, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now },
                new Project { Id = 5, Name = "Make New Router Vacuum Table", Description = "Make New Router Vacuum Table", TeamId = 6, PriorityId = 1, SortOrder = 5, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now },
                new Project { Id = 6, Name = "Develop Path Planning", Description = "Develop Path Planning", TeamId = 4, PriorityId = 1, SortOrder = 6, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now }
            );
        }
    }
}
