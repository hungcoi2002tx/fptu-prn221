using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment.Models;

namespace Assignment.Pages.Teachers
{
    public class CreateModel : PageModel
    {
        private readonly Assignment.Models.TimeTableContext _context;

        public CreateModel(Assignment.Models.TimeTableContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Teacher Teacher { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Teachers == null || Teacher == null)
            {
                return Page();
            }
            if (_context.Teachers.FirstOrDefault(x => x.Code == Teacher.Code) != null)
            {
                ModelState.AddModelError("", "Code trùng");
                return Page();
            }
            Teacher.CreateTime = DateTime.Now;
            _context.Teachers.Add(Teacher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
