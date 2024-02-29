using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Pages.Classes
{
    public class DetailsModel : PageModel
    {
        private readonly Assignment.Models.TimeTableContext _context;

        public DetailsModel(Assignment.Models.TimeTableContext context)
        {
            _context = context;
        }

        public Class Class { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var classModel = await _context.Classes.FirstOrDefaultAsync(m => m.Id == id);
            if (classModel == null)
            {
                return NotFound();
            }
            else
            {
                Class = classModel;
            }
            return Page();
        }
    }
}
