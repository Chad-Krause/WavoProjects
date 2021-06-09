
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.DatabaseModels
{
    public class Tidbit
    {
        public int UserId { get; set; }
        public int TidbitTypeId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public User User { get; set; }
        public TidbitType TidbitType { get; set; }

    }
}
