using Assignment.Models;
using Assignment.Ultils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace Assignment.Pages.TimeTable
{
    public class CreateByFileModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly Assignment.Models.TimeTableContext _context;
        private readonly Logger _log;

        public CreateByFileModel(IWebHostEnvironment hostingEnvironment, Assignment.Models.TimeTableContext context, Logger log)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _log = log;
        }

        public List<Class> Classes { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Slot> Slots { get; set; }
        public List<Timetable> TimeTables { get; set; }



        public Class ClassId { get; set; }


        public async Task<IActionResult> OnGetViewByClassAsync(string? classId)
        {
            try
            {
                ClassId = classId == null ? Classes.FirstOrDefault() : Classes.FirstOrDefault(x => x.Id == classId);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToPage("/Error", new {ErrorDetail = "có lỗi xảy ra"});
        }

        public async Task<IActionResult> OnPostAsync(IFormFile csvFile)
        {
            try
            {
                List<Timetable> timeTables = new List<Timetable>();
                List<String> error = new List<string>();
                if (csvFile != null && csvFile.Length > 0)
                {
                    await GetDataAsync();
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", csvFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await csvFile.CopyToAsync(stream);
                    }

                    using (var reader = new StreamReader(filePath))
                    {
                        int index = 1;
                        while (!reader.EndOfStream)
                        {
                            var line = await reader.ReadLineAsync();
                            var values = line.Split(',');
                            if (values.Length != 5)
                            {
                                error.Add($"Dòng {index} - [{line}] - Dữ liệu đầu vào thừa hoặc thiếu");
                            }
                            else
                            {
                                var timetable = new Timetable()
                                {
                                    ClassCode = values[0],
                                    SubjectCode = values[1],
                                    RoomCode = values[2],
                                    TeacherCode = values[3],
                                    SlotCode = values[4],
                                };
                                string errorCheck = ValidationTimeTable(timetable);
                                if (errorCheck == string.Empty)
                                {
                                    timetable.Id = @Guid.NewGuid().ToString("N");
                                    timetable.CreateTime = DateTime.Now;
                                    timeTables.Add(timetable);
                                    //Save in db
                                    TimeTables.Add(timetable);
                                    _context.Timetables.Add(timetable);
                                    _context.SaveChanges();
                                }
                                else
                                {
                                    error.Add($"Dòng {index} - [{line}] - {errorCheck}");
                                }
                            }
                            index++;
                        }
                    }
                    ViewData["SuccessData"] = timeTables;
                    ViewData["ErrorData"] = error;
                }
            }
            catch(Exception ex) {
                _log.LogError(ex.Message);
            }
            return Page();
        }

        private async Task GetDataAsync()
        {
            try
            {
                Classes =await _context.Classes.Where(x => x.Status == true).ToListAsync();
                Subjects =await _context.Subjects.Where(x => x.Status == true).ToListAsync();
                Rooms = await _context.Rooms.Where(x => x.Status == true).ToListAsync();
                Teachers = await _context.Teachers.Where(x => x.Status == true).ToListAsync(); 
                Slots = await _context.Slots.ToListAsync();
                TimeTables = await _context.Timetables.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string ValidationTimeTable(Timetable timeTable)
        {
            Boolean isError = false;
            String error = String.Empty;

            //compare value with Db
            if (!isError)
            {
                var result = CheckValidData(timeTable);
                isError = result.Item1;
                error = result.Item2;
            }

            // 1 room chi co 1 slot 
            if (!isError)
            {
                var result = CheckValidRoomSlot(timeTable);
                isError = result.Item1;
                error = result.Item2;
            }
            if (!isError)
            {
                var result = CheckValidClassSubject(timeTable);
                isError = result.Item1;
                error = result.Item2;
            }
            if (!isError)
            {
                var result = CheckValidClassSlot(timeTable);
                isError = result.Item1;
                error = result.Item2;
            }
            if (!isError)
            {
                var result = CheckValidTeacherSlot(timeTable);
                isError = result.Item1;
                error = result.Item2;
            }
            return error;
        }
        //1 teacher have only 1 slot
        private (Boolean, String) CheckValidTeacherSlot(Timetable timeTable)
        {
            (Boolean, String) result = (false, String.Empty);
            try
            {
                var timetable = TimeTables.Where(x => x.TeacherCode == timeTable.TeacherCode)
                                            .Where(x => x.SlotCode == timeTable.SlotCode)
                                            .FirstOrDefault();
                if (timetable != null)
                {
                    return (true, $" 1 EditModel have only 1 slot code {timeTable.SlotCode} conflict with {timetable}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }
        //1 class have only slot
        private (Boolean, String) CheckValidClassSlot(Timetable timeTable)
        {
            (Boolean, String) result = (false, String.Empty);
            try
            {
                var timetable = TimeTables.Where(x => x.ClassCode == timeTable.ClassCode)
                                            .Where(x => x.SlotCode == timeTable.SlotCode)
                                            .FirstOrDefault();
                if (timetable != null)
                {
                    return (true, $" 1 Class have only 1 slot code {timeTable.SlotCode} conflict with {timetable}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }
        //1 class have only subjectcode
        private (Boolean, String) CheckValidClassSubject(Timetable timeTable)
        {
            (Boolean, String) result = (false, String.Empty);
            try
            {
                var timetable = TimeTables.Where(x => x.ClassCode == timeTable.ClassCode)
                                            .Where(x => x.SubjectCode == timeTable.SubjectCode)
                                            .FirstOrDefault();
                if (timetable != null)
                {
                    return (true, $" 1 Class have only 1 EditModel {timeTable.SubjectCode} conflict with {timetable}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }

        //1 room 1 slot
        private (Boolean, String) CheckValidRoomSlot(Timetable timeTable)
        {
            (Boolean, String) result = (false, String.Empty);
            try
            {
                var timetable = TimeTables.Where(x => x.RoomCode == timeTable.RoomCode)
                                            .Where(x => x.SlotCode == timeTable.SlotCode)
                                            .FirstOrDefault();
                if (timetable != null)
                {
                    return (true, $" 1 EditModel have only 1 slot conflict with {timetable}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }


        //compare value with Db
        private (Boolean, String) CheckValidData(Timetable timeTable)
        {
            (Boolean, String) result = (false, String.Empty);
            try
            {
                if(Classes.FirstOrDefault(x => x.Code == timeTable.ClassCode) == null)
                {
                    result.Item1 = true;
                    result.Item2 = "Code class is not exist";
                }else if (Subjects.FirstOrDefault(x => x.Code == timeTable.SubjectCode) == null)
                {
                    result.Item1 = true;
                    result.Item2 = "Code subject is not exist";
                }
                else if (Rooms.FirstOrDefault(x => x.Code == timeTable.RoomCode) == null)
                {
                    result.Item1 = true;
                    result.Item2 = "Code room is not exist";
                }
                else if (Teachers.FirstOrDefault(x => x.Code == timeTable.TeacherCode) == null)
                {
                    result.Item1 = true;
                    result.Item2 = "Code teacher is not exist";
                }
                else if (Slots.FirstOrDefault(x => x.Code == timeTable.SlotCode) == null)
                {
                    result.Item1 = true;
                    result.Item2 = "Code slot is not exist";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");  
            }
            return result;
        }
    }
}
