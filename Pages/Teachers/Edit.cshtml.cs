﻿using System;
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

namespace Assignment.Pages.Teachers
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
        public TeacherEditModel EntityEditModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacherModel = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);
            ViewData["model"] = teacherModel;
            if (teacherModel == null)
            {
                return NotFound();
            }
            EntityEditModel = _mapper.Map<TeacherEditModel>(teacherModel);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var teacherModel = _mapper.Map<Teacher>(EntityEditModel);

            _context.Attach(teacherModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(EntityEditModel.Id))
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

        private bool TeacherExists(string id)
        {
          return (_context.Teachers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
