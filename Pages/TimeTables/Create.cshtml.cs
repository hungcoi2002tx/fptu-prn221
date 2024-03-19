using Assignment.Models;
using Assignment.Models.EditModel;
using Assignment.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace Assignment.Pages.TimeTables
{
    public class CreateModel : PageModel
    {
        private readonly TimeTableContext _db;
        private readonly Logger _log;
        private readonly Validate _valid;
        private readonly IMapper _mapper;

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
        public CreateModel(TimeTableContext db, Logger log, Validate valid, IMapper mapper)
        {
            _db = db;
            _log = log;
            _valid = valid;
            _mapper = mapper;
        }
        public async Task OnGetAsync()
        {            
            try
            {
                EditModel = new TimeTableEditModel();
                await GetBaseDataAsync();
            }
            catch(Exception ex)
            {
                _log.LogError($"GetBaseData - Create - Timetables - {ex.Message}");
            }
        }

        private async Task GetBaseDataAsync()
        {
            try
            {
                Classes = await _db.Classes.ToListAsync();
                Rooms = await _db.Rooms.ToListAsync();
                Slots = await _db.Slots.ToListAsync();
                Subjects = await _db.Subjects.ToListAsync();
                Teachers = await _db.Teachers.ToListAsync();
            }catch (Exception ex)
            {
                _log.LogError($"GetBaseData - Create - Timetables - {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostAsync(TimeTableEditModel EditModel){
            try
            {
                Timetable timetable = _mapper.Map<Timetable>(EditModel);
                var error =await _valid.ValidationTimeTableAsync(timetable);
                if (error == String.Empty)
                {
                    timetable.Id = Guid.NewGuid().ToString("N");
                    timetable.CreateTime = DateTime.Now;
                    _db.AddAsync(timetable);
                    _db.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("", error);
                    await GetBaseDataAsync();
                    return Page();
                }
            }catch(Exception ex)
            {
                _log.LogError($"OnPostAsync - Create - Timetables - {ex.Message}");
            }
            return RedirectToPage("./Index");
        }
    }
}
