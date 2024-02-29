using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Pages.Classes
{
    public class DeleteModel : PageModel
    {
        private readonly Assignment.Models.TimeTableContext _context;
        public DeleteModel(Assignment.Models.TimeTableContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }
            var classModel = await _context.Classes.FindAsync(id);

            if (classModel != null)
            {
                Class = classModel;
                _context.Classes.Remove(Class);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
