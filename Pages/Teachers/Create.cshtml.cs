using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment.Models;
using AutoMapper;
using Assignment.Models.EditModel;

namespace Assignment.Pages.Teachers
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
        public TeacherEditModel EditModel { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Teachers == null || EditModel == null)
            {
                return Page();
            }
            if (_context.Teachers.FirstOrDefault(x => x.Code == EditModel.Code) != null)
            {
                ModelState.AddModelError("", "Code trùng");
                return Page();
            }
            EditModel.CreateTime = DateTime.Now;
            var teacherModel = _mapper.Map<Teacher>(EditModel);
            _context.Teachers.Add(teacherModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
