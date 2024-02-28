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
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Assignment.Pages.Subjects
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

        public IList<SubjectViewModel> ViewModels { get;set; }

        public async Task OnGetAsync()
        {
            try
            {
                if (ViewModels?.Any() != true)
                {
                    var subjects = await _context.Subjects.ToListAsync();
                    ViewModels = _mapper.Map<List<SubjectViewModel>>(subjects);
                }
            }
            catch (Exception ex) { 
                Console.Write("Error in OnGetAsync() - Index - Subjects :" + ex.ToString());   
            }
        }
    }
}
