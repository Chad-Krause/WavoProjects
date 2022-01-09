using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.DatabaseModels.QueryModels
{
    public class TeamMemberExtendedInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool TrackTime { get; set; }
        public double HoursAdjustment { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public double? Hours { get; set; }
        public int DistinctDays { get; set; }

    }
}
