using Assignment.Models;
using Assignment.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Assignment.Pages.TimeTables
{
    public class ViewBySlotModel : PageModel
    {
        private readonly TimeTableContext _db;
        private readonly Logger _log;
        private readonly Validate _valid;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet =true)]
        public List<string> Weekdays { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ItemSelected { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<Models.Room> Rooms { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public Timetable[,] Data { get; set; }

        private List<Timetable> _tables { get; set; }
        [BindProperty(SupportsGet = true)]
        public int NumberOfRoom { get; set; }

        private int DateSelected { get; set; }

        public ViewBySlotModel(TimeTableContext db, Logger log, Validate valid, IMapper mapper)
        {
            _db = db;
            _log = log;
            _valid = valid;
            _mapper = mapper;
        }

        public async Task OnGetAsync(string? ItemSelected)
        {
            try
            {
                await GetDataAsync();
                Timetable[,] Data = new Timetable[NumberOfRoom, 4];
                DateSelected = GetDateSelected(ItemSelected);
                _tables =await _db.Timetables.Where(x => x.SlotCode.Contains(DateSelected.ToString())).ToListAsync();
                foreach (var item in Rooms)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var table = _tables.FirstOrDefault(x => x.RoomCode == item.Code);
                        if(table != null)
                        {
                            string slot = table.SlotCode;
                            if (slot.Substring(0, 1) == "A" && i <= 1)
                            {
                                string day = DateSelected.ToString();
                                int indexOf = slot.IndexOf(day);
                                int indexOfTimetable = Rooms.IndexOf(item);

                                if (indexOf - 1 == i)
                                {
                                    Data[indexOfTimetable, i] = table;
                                }
                            }
                            else if (slot.Substring(0, 1) == "P" && i > 1)
                            {
                                string day = DateSelected.ToString();
                                int indexOf = slot.IndexOf(day);
                                int indexOfTimetable = Rooms.IndexOf(item);

                                if (indexOf + 1 == i)
                                {
                                    Data[indexOfTimetable, i] = table;
                                }
                            }
                        }
                        
                    }                
                }
                ViewData["Data"] = Data;
            }
            catch (Exception ex)
            {
                _log.LogError("ViewBySlot - OnGetAsync - " + ex.Message);
            }
        }


        public int GetDateSelected(string? ItemSelected)
        {
            try
            {
                if (ItemSelected != null)
                {
                    switch (ItemSelected)
                    {
                        case "Monday":
                            return 2;
                            break;
                        case "Tuesday":
                            return 3;
                            break;
                        case "Wednesday":
                            return 4;
                            break;
                        case "Thursday":
                            return 5;
                            break;
                        case "Friday":
                            return 6;
                            break;
                        case "Saturday":
                            return 7;
                            break;
                        case "Sunday":
                            return 8;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError("ViewBySlot - OnGetAsync - " + ex.Message);
            }
            return 2;
        }

        private async Task GetDataAsync()
        {
            try
            {
                Rooms = await _db.Rooms.OrderBy(x => x.Code).ToListAsync();
                NumberOfRoom = Rooms.Count();
                Weekdays = new List<string>{
                                        "Monday",    
                                        "Tuesday",   
                                        "Wednesday", 
                                        "Thursday",  
                                        "Friday",    
                                        "Saturday",  
                                        "Sunday"     
                                    };
            }
            catch (Exception ex)
            {
                _log.LogError("ViewBySlot - GetDataAsync - " + ex.Message);
            }
        }
    }
}
