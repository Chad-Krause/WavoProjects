using Microsoft.EntityFrameworkCore;
using System;
using WavoProjects.Api.DatabaseModels.QueryModels;

namespace WavoProjects.Api.DatabaseModels
{
    public class WavoContext : DbContext
    {
        public WavoContext(DbContextOptions<WavoContext> options) : base(options)
        {

        }
        
        public virtual DbSet<HubClient> HubClients { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Snapshot> Snapshots { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<Timesheet> Timesheets { get; set; }

        internal virtual DbSet<PriorityView> _PriorityView { get; set; }
        internal virtual DbSet<TeamMemberExtendedInformation> _TeamMemberExtendedInformation { get; set; }
        internal virtual DbSet<TimesheetTeamMember> _TimesheetTeamUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WavoContext).Assembly);
        }
    }
}
