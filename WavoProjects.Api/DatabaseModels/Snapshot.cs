using System;
using System.Collections.Generic;
using System.Text;

namespace WavoProjects.Api.Models
{
    public class Snapshot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset SnapshotTakenOn { get; set; }
    }
}
