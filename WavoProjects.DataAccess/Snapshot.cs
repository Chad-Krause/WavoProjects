using System;
using System.Collections.Generic;
using System.Text;

namespace WavoProjects.DataAccess
{
    public class Snapshot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset SnapshotTakenOn { get; set; }
    }
}
