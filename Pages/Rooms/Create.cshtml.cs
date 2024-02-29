using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment.Models;

namespace Assignment.Pages.Rooms
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
        public Room Room { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Rooms == null || Room == null)
            {
                return Page();
            }
          if(_context.Rooms.FirstOrDefault(x => x.Code == Room.Code) != null)
            {
                ModelState.AddModelError("", "Code trùng");
                return Page();
            }
            Room.CreateTime = DateTime.Now;
            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
