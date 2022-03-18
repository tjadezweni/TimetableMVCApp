using System;
using System.Collections.Generic;

namespace TimetableMVCApp.Models
{
    public partial class Day
    {
        public Day()
        {
            Modules = new HashSet<Module>();
        }

        public int DayId { get; set; }
        public string Name { get; set; } = null!;
        public int TimeId { get; set; }

        public virtual Time? Time { get; set; } = null!;

        public virtual ICollection<Module> Modules { get; set; }
    }
}
