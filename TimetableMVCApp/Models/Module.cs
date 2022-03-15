using System;
using System.Collections.Generic;

namespace TimetableMVCApp.Models
{
    public partial class Module
    {
        public Module()
        {
            Days = new HashSet<Day>();
        }

        public int ModuleId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual ICollection<Day> Days { get; set; }
    }
}
