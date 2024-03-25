using Assignment.Models.EditModel;
using Assignment.Models;
using Assignment.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Assignment.Pages.TimeTables
{
    public class DetailsModel : PageModel
    {
        private readonly TimeTableContext _db;
        private readonly Logger _log;
        private readonly Validate _valid;
        private readonly IMapper _mapper;

        public DetailsModel(TimeTableContext db, Logger log, Validate valid, IMapper mapper)
        {
            _db = db;
            _log = log;
            _valid = valid;
            _mapper = mapper;
        }

        [BindProperty]
        public List<Models.Class> Classes { get; set; }
        [BindProperty]
        public List<Models.Room> Rooms { get; set; }
        [BindProperty]
        public List<Teacher> Teachers { get; set; }
        [BindProperty]
        public List<Slot> Slots { get; set; }
        [BindProperty]
        public List<Subject> Subjects { get; set; }
        [BindProperty]
        public TimeTableEditModel EditModel { get; set; }
        public async Task OnGetAsync(string id)
        {
            var model = await _db.Timetables.FirstOrDefaultAsync(x => x.Id == id);
            EditModel = _mapper.Map<TimeTableEditModel>(model);
            await GetBaseDataAsync();
        }

        private async Task GetBaseDataAsync()
        {
            try
            {
                Classes = await _db.Classes.Where(x => x.Status == true).ToListAsync();
                Rooms = await _db.Rooms.Where(x => x.Status == true).ToListAsync();
                Slots = await _db.Slots.ToListAsync();
                Subjects = await _db.Subjects.Where(x => x.Status == true).ToListAsync();
                Teachers = await _db.Teachers.Where(x => x.Status == true).ToListAsync();
            }
            catch (Exception ex)
            {
                _log.LogError($"GetBaseData - Create - Timetables - {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostAsync(TimeTableEditModel EditModel)
        {
            try
            {
                Timetable timetable = _mapper.Map<Timetable>(EditModel);
                var error = await _valid.ValidationTimeTableAsync(timetable);
                if (error == String.Empty)
                {                 
                    _db.Timetables.Update(timetable);
                    _db.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("", error);
                    await GetBaseDataAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"OnPostAsync - Create - Timetables - {ex.Message}");
            }
            return RedirectToPage("./ViewOriginal");
        }
    }
}
