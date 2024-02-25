using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Room
    {
        public Room()
        {
            Timetables = new HashSet<Timetable>();
        }

        public string Id { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime? CreateTime { get; set; }

        public virtual ICollection<Timetable> Timetables { get; set; }
    }
}
