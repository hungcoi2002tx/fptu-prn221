using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.EditModel
{
    public class ClassEditModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Tên không được trống")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Tên lớp học")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Code không được trống")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Code lớp học")]
        public string Code { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
