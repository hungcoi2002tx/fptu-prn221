using Assignment.Models.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Pages.Classes
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

        public IList<ClassViewModel> ViewModels { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                if (ViewModels?.Any() != true)
                {
                    var classes = await _context.Classes.ToListAsync();
                    ViewModels = _mapper.Map<List<ClassViewModel>>(classes);
                }
            }
            catch (Exception ex)
            {
                Console.Write("Error in OnGetAsync() - Index - Ckasses :" + ex.ToString());
            }
        }
    }
}
