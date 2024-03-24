using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Assignment.Models.EditModel
{
    public class TeacherEditModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Tên không được trống")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Tên giáo viên")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Code không được trống")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Code giáo viên")]
        public string Code { get; set; }

        public bool Status { get; set; } = true;

        public DateTime CreateTime { get; set; }
    }
}
