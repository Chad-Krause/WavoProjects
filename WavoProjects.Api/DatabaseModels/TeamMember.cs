using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WavoProjects.Api.Models;

namespace WavoProjects.Api.DatabaseModels
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool TrackTime { get; set; }
    }

    public partial class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> entity)
        {
            entity.Property(i => i.Id).ValueGeneratedOnAdd();
            entity.Property(i => i.Name).HasMaxLength(255);
            entity.ToTable("TeamMember");
        }
    }
}
