using Assignment.Models;
using Assignment.Models.EditModel;
using Assignment.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Pages.TimeTables
{
    public class CreateModel : PageModel
    {
        private readonly TimeTableContext _db;
        private readonly Logger _log;

        public List<Models.Class> Classes;
        public List<Models.Room> Rooms;
        public List<Teacher> Teachers;
        public List<Slot> Slots;
        public List<Subject> Subjects;
        public TimeTableEditModel EditModel { get; set; }
        public CreateModel(TimeTableContext db, Logger log)
        {
            _db = db;
            _log = log;
        }
        public void OnGet()
        {
            GetBaseData();
        }

        private void GetBaseData()
        {
            try
            {
                Classes = _db.Classes.ToList();
                Rooms = _db.Rooms.ToList();
                Slots = _db.Slots.ToList();
                Subjects = _db.Subjects.ToList();
                Teachers = _db.Teachers.ToList();
            }catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
        }
    }
}
