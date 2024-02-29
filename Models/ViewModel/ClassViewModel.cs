using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Assignment.Models.ViewModel
{
    public class ClassViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Tên lớp học")]
        public string Name { get; set; }
        [Display(Name = "Code lớp học")]
        public string Code { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreateTime { get; set; }
    }
}
