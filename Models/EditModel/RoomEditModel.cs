using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Assignment.Models.EditModel
{
    public class RoomEditModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Code không được trống")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Chiều dài không chính xác")]
        [Display(Name = "Code phòng học")]
        public string Code { get; set; }

        public bool Status { get; set; } = true;

        public DateTime CreateTime { get; set; }
    }
}
