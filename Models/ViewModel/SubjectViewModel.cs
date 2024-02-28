using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Assignment.Models.ViewModel
{
    public class SubjectViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Tên môn học")]
        public string Name { get; set; }
        [Display(Name = "Code môn học")]
        public string Code { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreateTime { get; set; }
    }
}
