using Assignment.Models;
using Assignment.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Pages.TimeTables
{
    public class ViewByClassModel : PageModel
    {
        private readonly TimeTableContext _db;
        private readonly Logger _log;
        private readonly Validate _valid;
        private readonly IMapper _mapper;

        [BindProperty]
        public List<Models.Class> Classes { get; set; }

        [BindProperty]
        public string ClassCodeSelected { get; set; }

        [BindProperty]
        public Timetable[,] Data { get; set; } = new Timetable[4, 7];

        private List<Timetable> _tables { get;set; }

        public ViewByClassModel(TimeTableContext db, Logger log, Validate valid, IMapper mapper)
        {
            _db = db;
            _log = log;
            _valid = valid;
            _mapper = mapper;
        }

        public async Task OnGetAsync(string? ClassCodeSelected)
        {
            try
            {
                await GetDataAsync();                
                List<Timetable> timeTables = ClassCodeSelected == null ? await _db.Timetables.Where(x => x.ClassCode == Classes.FirstOrDefault().Code).ToListAsync()
                                                    : await _db.Timetables.Where(x => x.ClassCode == ClassCodeSelected).ToListAsync();
                foreach (var item in timeTables)
                {
                    if(item.SlotCode[0] == 'A')
                    {
                        int daySlot1 = Int32.Parse(item.SlotCode[1].ToString());
                        int daySlot2 = Int32.Parse(item.SlotCode[2].ToString());
                        Data[0, daySlot1 - 2] = item;
                        Data[1, daySlot2 - 2] = item;
                    }else if(item.SlotCode[0] == 'P')
                    {
                        int daySlot1 = Int32.Parse(item.SlotCode[1].ToString());
                        int daySlot2 = Int32.Parse(item.SlotCode[2].ToString());
                        Data[2, daySlot1 - 2] = item;
                        Data[3, daySlot2 - 2] = item;
                    }
                    
                }
            }
            catch(Exception ex)
            {
                _log.LogError("ViewByClass - OnGetAsync - " + ex.Message);
            }
        }

        private async Task GetDataAsync()
        {
            try
            {
                Classes = await _db.Classes.Where(x => x.Status == true).ToListAsync();

            }
            catch (Exception ex)
            {
                _log.LogError("ViewByClass - GetDataAsync - " + ex.Message);
            }
        }
    }
}
