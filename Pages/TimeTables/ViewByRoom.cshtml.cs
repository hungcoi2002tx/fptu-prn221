using Assignment.Models;
using Assignment.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Pages.TimeTables
{
    public class ViewByRoomModel : PageModel
    {
        
        private readonly TimeTableContext _db;
        private readonly Logger _log;
        private readonly Validate _valid;
        private readonly IMapper _mapper;
        public async Task OnGetAsync()
        {

        }
    }
}
