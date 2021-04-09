using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.Models
{
    public class UpdateProjectAndSortOrdersModel
    {
        public int Id { get; set; }
        public int NewPriorityId { get; set; }

        public List<ProjectSortOrder> SortOrders { get; set; }
    }
}
