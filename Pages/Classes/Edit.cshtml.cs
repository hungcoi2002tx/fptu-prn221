using Assignment.Models;
using Assignment.Models.EditModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Pages.Classes
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
        public ClassEditModel EntityEditModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var classModel = await _context.Classes.FirstOrDefaultAsync(m => m.Id == id);
            ViewData["model"] = classModel;
            if (classModel == null)
            {
                return NotFound();
            }
            EntityEditModel = _mapper.Map<ClassEditModel>(classModel);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var classModel = _mapper.Map<Class>(EntityEditModel);

            _context.Attach(classModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassCheckCode(EntityEditModel.Id))
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

        private bool ClassCheckCode(string id)
        {
            return (_context.Classes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
