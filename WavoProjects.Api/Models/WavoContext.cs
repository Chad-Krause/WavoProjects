using Microsoft.EntityFrameworkCore;
using System;
using WavoProjects.Api.Models.QueryModels;

namespace WavoProjects.Api.Models
{
    public class WavoContext : DbContext
    {
        public WavoContext(DbContextOptions<WavoContext> options) : base(options)
        {

        }
        
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Snapshot> Snapshots { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        internal virtual DbSet<PriorityView> _PriorityView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WavoContext).Assembly);
        }
    }
}
