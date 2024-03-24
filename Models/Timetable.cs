using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Timetable
    {
        public string Id { get; set; } = null!;
        public string? ClassCode { get; set; }
        public string? TeacherCode { get; set; }
        public string? SubjectCode { get; set; }
        public string? RoomCode { get; set; }
        public string? SlotCode { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual Class? ClassCodeNavigation { get; set; }
        public virtual Room? RoomCodeNavigation { get; set; }
        public virtual Slot? SlotCodeNavigation { get; set; }
        public virtual Subject? SubjectCodeNavigation { get; set; }
        public virtual Teacher? TeacherCodeNavigation { get; set; }
    }
}
