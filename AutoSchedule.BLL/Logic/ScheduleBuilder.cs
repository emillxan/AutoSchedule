using AutoSchedule.BLL.CRUD;
using AutoSchedule.BLL.CRUD.Slots;
using AutoSchedule.Domain.DTOs;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;

namespace AutoSchedule.BLL.Logic;

public class ScheduleBuilder(IBaseService<Cabinet> cabinetRepository,
        IBaseService<Squad> squadRepository,
        IBaseService<Subject> subjectRepository,
        IBaseService<Teacher> teacherRepository,
        ILessonService lessonService) : IScheduleBuilder
{
    private readonly IBaseService<Cabinet> _cabinetRepository = cabinetRepository;
    private readonly IBaseService<Squad> _squadRepository = squadRepository;
    private readonly IBaseService<Subject> _subjectRepository = subjectRepository;
    private readonly IBaseService<Teacher> _teacherRepository = teacherRepository;
    private readonly ILessonService _lessonService = lessonService;

    List<Cabinet> cabinets = new List<Cabinet>();
    List<Squad> squads = new List<Squad>();
    List<Subject> subjects = new List<Subject>();
    List<Teacher> teachers = new List<Teacher>();

    public void GetStartList()
    {
        cabinets = _cabinetRepository.GetAll().Data;
        squads = _squadRepository.GetAll().Data;
        subjects = _subjectRepository.GetAll().Data;
        teachers = _teacherRepository.GetAll().Data;
    }

    public void Start()
    {
        GetStartList();
        GenerateWeeklySchedule();

    }
    public List<LessonDTO> StartR()
    {
        GetStartList();

        var result = GenerateWeeklySchedule().Lessons;

        foreach (var lesson in result)
        {
            _lessonService.Create(new Lesson
            {
                CabinetId = lesson.Cabinet.Id,
                SquadId = lesson.Squad.Id,
                TeacherId = lesson.Teacher.Id,
                SubjectId = lesson.Subject.Id,
                Time = lesson.Time,
                DayOfWeek = lesson.DayOfWeek,
            });
        }
        return result;

        /*var result = _lessonService.GetAll().Data;
        return result;*/
    }
    public Schedule Create()
    {
        GetStartList();

        var result = GenerateWeeklySchedule();
        return result;
    }

    private WeekType GetWeekType(int weekNumber)
    {
        return weekNumber % 2 == 0 ? WeekType.UpperWeek : WeekType.LowerWeek;
    }

    public Schedule GenerateWeeklySchedule()
    {
        var schedule = new Schedule();
        DateTime startOfWeek = GetMondayOfCurrentWeek();
        Dictionary<int, int> subjectWeeklyCount = new Dictionary<int, int>(); // Счетчик уроков по предметам

        foreach (var squad in squads)
        {
            for (int week = 0; week < 2; week++) // Две недели: верхняя и нижняя
            {
                subjectWeeklyCount.Clear();

                foreach (var subjectId in squad.SubjectIds)
                {
                    var subject = subjects.FirstOrDefault(s => s.Id == subjectId);
                    if (subject == null) continue;

                    int totalPairs = (int)Math.Ceiling((double)subject.TotalHours / 2);
                    int pairsPerWeek = totalPairs / 15;
                    int additionalPairs = totalPairs % 15;

                    WeekType weekType = GetWeekType(week);
                    int pairsThisWeek = weekType == WeekType.UpperWeek ? pairsPerWeek : pairsPerWeek + (additionalPairs > 0 ? 1 : 0);

                    subjectWeeklyCount[subjectId] = 0; // Сброс счетчика уроков для предмета на новой неделе

                    for (int day = 0; day < 5; day++) // Пять учебных дней
                    {
                        DateTime currentDate = startOfWeek.AddDays(week * 7 + day);
                        int lessonCount = 0;
                        while (subjectWeeklyCount[subjectId] < pairsThisWeek && lessonCount < ScheduleConstants.MaxLessonsPerDay)
                        {
                            var lessonTime = CalculateLessonTime(lessonCount, currentDate);
                            var availableCabinet = FindAvailableCabinet(cabinets, lessonTime, schedule);
                            var availableTeacher = FindAvailableTeacher(teachers, subject, lessonTime, schedule);

                            if (availableTeacher != null && availableCabinet != null)
                            {
                                var lesson = new LessonDTO
                                {
                                    Subject = subject,
                                    Teacher = availableTeacher,
                                    Cabinet = availableCabinet,
                                    Squad = squad,
                                    Time = lessonTime,
                                    DayOfWeek = currentDate.DayOfWeek,
                                    WeekType = weekType
                                };

                                if (schedule.AddLesson(lesson))
                                {
                                    subjectWeeklyCount[subjectId]++;
                                    break;
                                }
                            }
                            lessonCount++;
                        }
                    }
                }
            }
            startOfWeek = startOfWeek.AddDays(7); // Переход к следующей учебной неделе
        }

        return schedule;


    }




    public static DateTime GetMondayOfCurrentWeek()
    {
        DateTime today = DateTime.Today;
        int daysSinceMonday = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
        // Если сегодня до понедельника, отнимаем ещё 7 дней
        if (daysSinceMonday < 0)
        {
            daysSinceMonday += 7;
        }
        return today.AddDays(-daysSinceMonday);
    }

    public Cabinet FindAvailableCabinet(List<Cabinet> cabinets, DateTime lessonTime, Schedule schedule)
    {
        foreach (var cabinet in cabinets)
        {
            bool isCabinetFree = true;

            foreach (var lesson in schedule.Lessons)
            {
                if (lesson.Time == lessonTime && lesson.Cabinet.Id == cabinet.Id)
                {
                    isCabinetFree = false;
                    break; // Кабинет занят в это время
                }
            }

            if (isCabinetFree)
            {
                return cabinet; // Найден свободный кабинет
            }
        }

        return null; // Нет свободных кабинетов в это время
    }

    public Teacher FindAvailableTeacher(List<Teacher> teachers, Subject subject, DateTime lessonTime, Schedule schedule)
    {
        foreach (var teacher in teachers)
        {
            if (teacher.SubjectIds.Contains(subject.Id))
            {
                bool isTeacherFree = true;

                foreach (var lesson in schedule.Lessons)
                {
                    if (lesson.Time == lessonTime && lesson.Teacher.Id == teacher.Id)
                    {
                        isTeacherFree = false;
                        break; // Учитель занят в это время
                    }
                }

                if (isTeacherFree)
                {
                    return teacher; // Найден свободный учитель
                }
            }
        }

        return null; // Нет свободных учителей для этого предмета в это время
    }

    public static DateTime CalculateLessonTime(int lessonNumber, DateTime day)
    {
        // Общее время - это время начала дня плюс (продолжительность пары + перерыв) * количество предыдущих пар
        TimeSpan totalTime = ScheduleConstants.StartOfDay +
                            (ScheduleConstants.DurationOfLesson + ScheduleConstants.BreakTime) * lessonNumber;

        return day.Date + totalTime;
    }
}
