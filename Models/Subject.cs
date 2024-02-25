using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Timetables = new HashSet<Timetable>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string Code { get; set; } = null!;
        public DateTime? CreateTime { get; set; }

        public virtual ICollection<Timetable> Timetables { get; set; }
    }
}
