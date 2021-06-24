using Microsoft.EntityFrameworkCore;
using System;
using WavoProjects.Api.Models.QueryModels;

namespace WavoProjects.Api.DatabaseModels
{
    public class WavoContext : DbContext
    {
        public WavoContext(DbContextOptions<WavoContext> options) : base(options)
        {

        }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Snapshot> Snapshots { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Tidbit> Tidbits { get; set; }
        public virtual DbSet<TidbitType> TidbitTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Validator> Validators { get; set; }


        internal virtual DbSet<PriorityView> _PriorityView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WavoContext).Assembly);
        }
    }
}
