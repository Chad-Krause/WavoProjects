using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace WavoProjects.Api.DatabaseModels
{
    public class AutoSignOutTime
    {
        public int DayOfWeek { get; set; }
        public DateTime SignOutTime { get; set; }
    }

    public partial class AutoSignOutTimeConfiguration : IEntityTypeConfiguration<AutoSignOutTime>
    {
        public void Configure(EntityTypeBuilder<AutoSignOutTime> entity)
        {
            entity.ToTable("AutoSignOutTime");
            entity.HasKey(i => i.DayOfWeek);
            entity.Property(i => i.DayOfWeek).ValueGeneratedNever();

            DateTime weekend = DateTime.Today.AddHours(16);
            DateTime weekday = DateTime.Today.AddHours(21);
            entity.HasData(
                new AutoSignOutTime { DayOfWeek = 0, SignOutTime = weekend },
                new AutoSignOutTime { DayOfWeek = 1, SignOutTime = weekday },
                new AutoSignOutTime { DayOfWeek = 2, SignOutTime = weekday },
                new AutoSignOutTime { DayOfWeek = 3, SignOutTime = weekday },
                new AutoSignOutTime { DayOfWeek = 4, SignOutTime = weekday },
                new AutoSignOutTime { DayOfWeek = 5, SignOutTime = weekday },
                new AutoSignOutTime { DayOfWeek = 6, SignOutTime = weekend }
            );
        }
    }
}