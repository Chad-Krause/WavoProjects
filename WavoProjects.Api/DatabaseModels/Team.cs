using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WavoProjects.Api.DatabaseModels
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual List<Project> Projects { get; set; }

    }

    public partial class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> entity)
        {
            entity.Property(i => i.Id).ValueGeneratedOnAdd();
            entity.Property(i => i.Name).HasMaxLength(500);
            entity.Property(i => i.Color).HasColumnType("char(7)");
            entity.ToTable("Team");


            entity.HasData(
                new Team { Id = 1, Name = "General", Color = "#ef0000" },
                new Team { Id = 2, Name = "Business", Color = "#00ff2d" },
                new Team { Id = 3, Name = "CAD/Design", Color = "#4700d8" },
                new Team { Id = 4, Name = "Programming", Color = "#ffab00" },
                new Team { Id = 5, Name = "Media", Color = "#0dcaf0" },
                new Team { Id = 6, Name = "Build", Color = "#039aff" }
            );
        }
    }
}
