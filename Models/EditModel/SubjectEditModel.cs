using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.EditModel
{
    public class SubjectEditModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Phải có tên môn học")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Tên môn học")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phải có code môn học")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Code môn học")]
        public string Code { get; set; }

        public bool Status { get; set; } = true;

        public DateTime CreateTime { get; set; }
    }
}
