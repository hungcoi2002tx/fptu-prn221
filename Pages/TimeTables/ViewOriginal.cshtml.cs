using Assignment.Models;
using Assignment.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using static NuGet.Packaging.PackagingConstants;

namespace Assignment.Pages.TimeTables
{
    public class ViewOriginalModel : PageModel
    {
        private readonly TimeTableContext _db;
        private readonly Logger _log;
        private readonly Validate _valid;
        private readonly IMapper _mapper;

        public ViewOriginalModel(TimeTableContext db, Logger log, Validate valid, IMapper mapper)
        {
            _db = db;
            _log = log;
            _valid = valid;
            _mapper = mapper;
        }
        [BindProperty(SupportsGet = true)]
        public List<Timetable> Datas { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<string> Classes { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<string> Teachers { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<string> Rooms { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<string> Slots { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<string> Subjects { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ClassSelected { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SubjectSelected { get; set; } = "All";
        [BindProperty(SupportsGet = true)]
        public string TeacherSelected { get; set; } = "All";
        [BindProperty(SupportsGet = true)]
        public string RoomSelected { get; set; } = "All";
        [BindProperty(SupportsGet = true)]
        public string SlotSelected { get; set; } = "All";

        [BindProperty(SupportsGet = true)]
        public int Total { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }

        [BindProperty(SupportsGet = true)]
        public int TotalPage { get; set; }
        public async Task OnGetAsync(string ClassSelected, string SubjectSelected, string TeacherSelected, string RoomSelected, string SlotSelected)
        {
            await GetBaseDataAsync();
            if(ClassSelected != null)
            {
                Datas = Datas.Where(x => x.ClassCode.Contains(ClassSelected)).ToList();
            }
            if(SubjectSelected != "All" && SubjectSelected != null)
            {
                Datas = Datas.Where(x => x.SubjectCode == SubjectSelected).ToList();
            }
            if (TeacherSelected != "All" && TeacherSelected != null)
            {
                Datas = Datas.Where(x => x.TeacherCode == TeacherSelected).ToList();
            } 
            if (RoomSelected != "All" && RoomSelected != null)
            {
                Datas = Datas.Where(x => x.RoomCode == RoomSelected).ToList();
            }
            if (SlotSelected != "All" && SlotSelected != null)
            {
                Datas = Datas.Where(x => x.SlotCode == SlotSelected).ToList();
            }
            Paging();
        }

        private async Task GetBaseDataAsync()
        {
            try
            {
                Datas = await _db.Timetables.ToListAsync();
                Classes = await _db.Classes.Where(x => x.Status == true).Select(x => x.Code).ToListAsync();
                Teachers = await _db.Teachers.Where(x => x.Status == true).Select(x => x.Code).ToListAsync();
                Rooms = await _db.Rooms.Where(x => x.Status == true).Select(x => x.Code).ToListAsync();
                Slots = await _db.Slots.Select(x => x.Code).ToListAsync();
                Subjects = await _db.Subjects.Where(x => x.Status == true).Select(x => x.Code).ToListAsync();
            }
            catch(Exception ex)
            {
                _log.LogError($"ViewOriginal - GetBaseDataAsync - {ex.Message}");
            }
        }

        public void Paging()
        {
            if (PageIndex < 1) PageIndex = 1;
            if (Datas?.Any() == true)
            {
                Total = Datas.Count();
                PageSize = 10;
                TotalPage = (int)Math.Ceiling((double)Total / PageSize);

                if (TotalPage > 0)
                {
                    if (PageIndex > TotalPage) PageIndex = TotalPage;
                    Datas = Datas.Skip((PageIndex - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                }
            }
            else
            {
                Total = 0;
            }

        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var entity = _db.Timetables.Where(x => x.Id == id).FirstOrDefault();
                    _db.Remove(entity);
                    _db.SaveChanges();
                }             
            }catch(Exception ex)
            {
                _log.LogError($"OnPostAsync - ViewOriginal - {ex.Message}");
            }
            return RedirectToAction("/TimeTables/ViewOriginal");
        }
    }
}
