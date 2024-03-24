using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment.Models;
using Assignment.Models.EditModel;
using AutoMapper;

namespace Assignment.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly Assignment.Models.TimeTableContext _context;
        private readonly IMapper _mapper;

        public CreateModel(Assignment.Models.TimeTableContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.EditModel.RoomEditModel EditModel { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Rooms == null || EditModel == null)
            {
                return Page();
            }
            if (_context.Rooms.FirstOrDefault(x => x.Code == EditModel.Code) != null)
            {
                ModelState.AddModelError("", "Code trùng");
                return Page();
            }
            EditModel.CreateTime = DateTime.Now;
            var classModel = _mapper.Map<Room>(EditModel);
            _context.Rooms.Add(classModel);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
