using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment.Models;

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
        public Subject Subject { get; set; } = new Subject()
        {
            Id = Guid.NewGuid().ToString("N"),
            CreateTime = DateTime.Now,
        };
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Subjects == null || Subject == null)
            {
                return Page();
            }
            Subject.CreateTime = DateTime.Now;
            _context.Subjects.Add(Subject);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
