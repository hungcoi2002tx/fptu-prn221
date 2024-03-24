using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Assignment.Models.ViewModel
{
    public class TeacherViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Tên giáo viên học")]
        public string Name { get; set; }
        [Display(Name = "Code giáo viên học")]
        public string Code { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "Trạng thái")]
        public string StatusVm { get => Status == true ? "Active" : "DeActive"; set { } }
        public bool Status { get; set; }
    }
}
