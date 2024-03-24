using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment.Models;
using System.Net.WebSockets;
using AutoMapper;
using Assignment.Models.EditModel;

namespace Assignment.Pages.Subjects
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
        public SubjectEditModel EditModel { get; set; }
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Subjects == null || EditModel == null)
            {
                return Page();
            }
            if (_context.Subjects.FirstOrDefault(x => x.Code == EditModel.Code) != null)
            {
                ModelState.AddModelError("", "Code trùng");
                return Page();
            }
            EditModel.CreateTime = DateTime.Now;
            var subjectModel = _mapper.Map<Subject>(EditModel);
            _context.Subjects.Add(subjectModel);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
