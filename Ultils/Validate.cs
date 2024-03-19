using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Licenses;
using static System.Reflection.Metadata.BlobBuilder;

namespace Assignment.Ultils
{
    public class Validate
    {
        private readonly TimeTableContext _tableContext;
        private readonly Logger _logger;
        private readonly string TAG = "Valiate";
        private List<Class> Classes { get; set; }
        private List<Subject> Subjects { get; set; }
        private List<Room> Rooms { get; set; }
        private List<Teacher> Teachers { get; set; }
        private List<Slot> Slots { get; set; }
        private List<Timetable> TimeTables { get; set; }

        public Validate(TimeTableContext tableContext, Logger logger)
        {
            _tableContext = tableContext;
            _logger = logger;
        }

        public async Task<String> ValidationTimeTableAsync(Timetable timeTable)
        {
            await GetDataAsync();
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

        private async Task GetDataAsync()
        {
            try
            {
                Classes =await _tableContext.Classes.ToListAsync();
                Teachers = await _tableContext.Teachers.ToListAsync();
                Rooms = await _tableContext.Rooms.ToListAsync();
                Slots = await _tableContext.Slots.ToListAsync();
                TimeTables = await _tableContext.Timetables.ToListAsync();
            }
            catch(Exception e) {
                _logger.LogError(TAG + "- GetDataAsync - " + e.Message);
            }
        }

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
                    return (true, $" 1 Teacher have only 1 slot code {timeTable.SlotCode} conflict with {timetable}");
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
                    return (true, $" 1 Class have only 1 Subject {timeTable.SubjectCode} conflict with {timetable}");
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
                    return (true, $" 1 Room have only 1 slot conflict with {timetable}");
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
                if (Classes.FirstOrDefault(x => x.Code == timeTable.ClassCode) == null)
                {
                    result.Item1 = true;
                    result.Item2 = "Code class is not exist";
                }
                else if (Subjects.FirstOrDefault(x => x.Code == timeTable.SubjectCode) == null)
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }
    }
}
