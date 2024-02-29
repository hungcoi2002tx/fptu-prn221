using Assignment.Models;
using Assignment.Models.EditModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Pages.Classes
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

        [BindProperty]
        public ClassEditModel EditModel { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Classes == null || EditModel == null)
            {
                return Page();
            }
            if (_context.Classes.FirstOrDefault(x => x.Code == EditModel.Code) != null)
            {
                ModelState.AddModelError("", "Code trùng");
                return Page();
            }
            EditModel.CreateTime = DateTime.Now;
            var classModel = _mapper.Map<Class>(EditModel);
            _context.Classes.Add(classModel);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
