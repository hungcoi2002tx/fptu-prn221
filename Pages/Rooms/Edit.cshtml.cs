using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;
using AutoMapper;
using Assignment.Models.EditModel;

namespace Assignment.Pages.Rooms
{
    public class EditModel : PageModel
    {
        private readonly Assignment.Models.TimeTableContext _context;
        private readonly IMapper _mapper;

        public EditModel(Assignment.Models.TimeTableContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [BindProperty]
        public RoomEditModel EntityEditModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var roomModel = await _context.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            ViewData["model"] = roomModel;
            if (roomModel == null)
            {
                return NotFound();
            }
            EntityEditModel = _mapper.Map<RoomEditModel>(roomModel);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var roomEditModel = _mapper.Map<Room>(EntityEditModel);

            _context.Attach(roomEditModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(EntityEditModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool RoomExists(string id)
        {
          return (_context.Rooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
