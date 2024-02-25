using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Slot
    {
        public Slot()
        {
            Timetables = new HashSet<Timetable>();
        }

        public string Id { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime? CreateTime { get; set; }

        public virtual ICollection<Timetable> Timetables { get; set; }
    }
}
