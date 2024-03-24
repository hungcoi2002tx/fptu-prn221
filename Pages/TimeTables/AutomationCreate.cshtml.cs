using Assignment.Models;
using Assignment.Models.EditModel;
using Assignment.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Assignment.Pages.TimeTables
{
    public class AutomationCreateModel : PageModel
    {
        private readonly TimeTableContext _db;
        private readonly Logger _log;
        private readonly Validate _valid;
        private readonly IMapper _mapper;

        public AutomationCreateModel(TimeTableContext db, Logger log, Validate valid, IMapper mapper)
        {
            _db = db;
            _log = log;
            _valid = valid;
            _mapper = mapper;
        }

        public TimeTableEditModel EditModel { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<Subject> Subjects { get; set; }
        [BindProperty(SupportsGet = true)]

        public List<Models.Class> Classes { get; set; }
        [BindProperty(SupportsGet = true)]

        public List<Teacher> Teachers { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<string> Errors { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<string> Success { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                await GetBaseDataAsync();
            }catch (Exception ex)
            {
                _log.LogError($"AutomationCreate - OnGetAsync - {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostAsync(string subjectCode, string[] classIds, string[] teacherIds, int[] numberOfSlot)
        {
            try
            {
                await GetBaseDataAsync();
                var classCorrect = classIds.ToList();

                //check 1 class chi co 1 ma mon thoi
                foreach (var item in classIds)
                {
                    var error = await _valid.CheckValidClassSubjectAsync(item, subjectCode);
                    if(error != null)
                    {
                        Errors.Add(error);
                        classCorrect.Remove(item);
                    }
                }
                if(Errors?.Any() == true)
                {
                    return Page();
                }
                else
                {
                    for (int i = 0; i < teacherIds.Length; i++)
                    {
                        int currentInput = numberOfSlot[i];
                        for (int j = 0; j < classCorrect.Count(); j++)
                        {
                            if (currentInput == 0)
                            {
                                break;
                            }
                            else
                            {
                                var status = await AddTimeTableAsync(teacherIds[i], subjectCode, classCorrect[j]);
                                if(status != null)
                                {
                                    Success.Add(status);
                                    classCorrect.Remove(classCorrect[j]);
                                    j--;
                                    currentInput--;
                                }
                            }
                        }
                        if(currentInput != 0)
                        {
                            Errors.Add($"Khong the them {currentInput} {teacherIds[i]} - {subjectCode}");
                        }
                    }
                }             
                
            }
            catch (Exception ex)
            {
                _log.LogError($"AutomationCreate - OnPostAsync - {ex.Message}");
            }
            return Page();
        }

        private async Task<string> AddTimeTableAsync(string teacherCode, string subjectCode, string classCode)
        {
            try
            {
                var timeTable = await _valid.AddAutomaticAsync(subjectCode, classCode, teacherCode);
                if(timeTable != null)
                {
                    _db.Timetables.AddAsync(timeTable);
                    _db.SaveChanges();
                    return $"Add thành công {timeTable}";
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"AddTimeTableAsync - GetBaseDataAsync - {ex.Message}");
            }
            return null;
        }

        private async Task GetBaseDataAsync()
        {
            try
            {
                Subjects = await _db.Subjects.ToListAsync();
                Classes = await _db.Classes.ToListAsync();
                Teachers = await _db.Teachers.Include(x => x.Timetables).ToListAsync();
            }
            catch (Exception ex)
            {
                _log.LogError($"AutomationCreate - GetBaseDataAsync - {ex.Message}");
            }
        }
    }
}
