using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;
using Assignment.Models.EditModel;
using AutoMapper;

namespace Assignment.Pages.Subjects
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
        public SubjectEditModel SubjectEditModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject =  await _context.Subjects.FirstOrDefaultAsync(m => m.Id == id);
            ViewData["model"] = subject;
            if (subject == null)
            {
                return NotFound();
            }
            SubjectEditModel = _mapper.Map<SubjectEditModel>(subject);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var subject = _mapper.Map<Subject>(SubjectEditModel);

            _context.Attach(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(SubjectEditModel.Id))
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

        private bool SubjectExists(string id)
        {
          return (_context.Subjects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
