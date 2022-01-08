using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.DatabaseModels
{
    public class Timesheet
    {
        public int? Id { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

    public partial class TimesheetConfiguration : IEntityTypeConfiguration<Timesheet>
    {
        public void Configure(EntityTypeBuilder<Timesheet> entity)
        {
            entity.Property(i => i.Id).ValueGeneratedOnAdd();
            entity.ToTable("Timesheet");
        }
    }
}
