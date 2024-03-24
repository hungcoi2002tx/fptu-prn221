using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Class
    {
        public Class()
        {
            Timetables = new HashSet<Timetable>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string Code { get; set; } = null!;
        public DateTime? CreateTime { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Timetable> Timetables { get; set; }
    }
}
