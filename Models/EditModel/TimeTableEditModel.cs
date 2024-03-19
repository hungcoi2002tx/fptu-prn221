using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Assignment.Models.EditModel
{
    public class TimeTableEditModel
    {
        public string Id { get; set; }

        [Display(Name = "Lớp học")]
        [Required(ErrorMessage = "Trường lớp học không được trống")]
        public string? ClassCode { get; set; }

        [Display(Name = "Giáo viên")]
        [Required(ErrorMessage = "Trường giáo viên không được trống")]
        public string? TeacherCode { get; set; }

        [Display(Name = "Môn học")]
        [Required(ErrorMessage = "Trường môn học không được trống")]
        public string? SubjectCode { get; set; }

        [Display(Name = "Phòng học")]
        [Required(ErrorMessage = "Phòng học không được trống")]
        public string? RoomCode { get; set; }

        [Display(Name = "Thời gian")]
        [Required(ErrorMessage = "Thời gian không được trống")]
        public string? SlotCode { get; set; }


        public DateTime? CreateTime { get; set; }
    }
}
