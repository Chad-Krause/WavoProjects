using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace WavoProjects.Api.DatabaseModels
{
    public class Snapshot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime SnapshotTakenOn { get; set; }
    }

    public partial class SnapshotConfiguration : IEntityTypeConfiguration<Snapshot>
    {
        public void Configure(EntityTypeBuilder<Snapshot> entity)
        {
            entity.Property(i => i.Id).ValueGeneratedOnAdd();
            entity.Property(i => i.Name).HasMaxLength(500);
            entity.ToTable("Snapshot");
        }
    }
}
