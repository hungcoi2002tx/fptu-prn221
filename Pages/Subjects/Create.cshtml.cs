using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment.Models;
using System.Net.WebSockets;

namespace Assignment.Pages.Subjects
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
        public Subject Subject { get; set; }
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Subjects == null || Subject == null)
                {
                    return Page();
                }
            if (_context.Subjects.FirstOrDefault(x => x.Code == Subject.Code) != null)
            {
                ModelState.AddModelError("", "Code trùng");
                return Page();
            }
            Subject.CreateTime = DateTime.Now;
            _context.Subjects.Add(Subject);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
