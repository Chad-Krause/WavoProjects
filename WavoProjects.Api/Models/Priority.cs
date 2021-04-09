using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WavoProjects.Api.Models
{
    public class Priority
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Project> Projects { get; set; }

    }

    public partial class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> entity)
        {
            entity.HasData(
                new Priority { Id = 1, Name = "Unassigned" },
                new Priority { Id = 2, Name = "High" },
                new Priority { Id = 3, Name = "Medium" },
                new Priority { Id = 4, Name = "Low" },
                new Priority { Id = 5, Name = "Completed" }
            );
        }
    }
}
