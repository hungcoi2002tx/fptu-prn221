using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;
using AutoMapper;
using Assignment.Models.ViewModel;

namespace Assignment.Pages.Teachers
{
    public class IndexModel : PageModel
    {
        private readonly Assignment.Models.TimeTableContext _context;
        private readonly IMapper _mapper;

        public IndexModel(Assignment.Models.TimeTableContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<TeacherViewModel> ViewModels { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                if (ViewModels?.Any() != true)
                {
                    var teachers = await _context.Teachers.ToListAsync();
                    ViewModels = _mapper.Map<List<TeacherViewModel>>(teachers);
                }
            }
            catch (Exception ex)
            {
                Console.Write("Error in OnGetAsync() - Index - Ckasses :" + ex.ToString());
            }
        }
    }
}
