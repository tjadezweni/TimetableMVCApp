using System;
using System.Collections.Generic;

namespace TimetableMVCApp.Models
{
    public partial class Time
    {
        public Time()
        {
            Days = new HashSet<Day>();
        }

        public int TimeId { get; set; }
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual ICollection<Day> Days { get; set; }
    }
}
