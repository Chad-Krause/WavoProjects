using System;
using System.Collections.Generic;
using System.Text;

namespace WavoProjects.Api.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TeamId { get; set; }
        public int? PriorityId { get; set; }
        public DateTimeOffset? StartedOn { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }

        public virtual Team Team { get; set; }
        public virtual Priority Priority { get; set; }
    }
}
