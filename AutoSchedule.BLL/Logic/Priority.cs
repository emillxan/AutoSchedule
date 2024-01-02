using AutoSchedule.BLL.CRUD;
using AutoSchedule.BLL.CRUD.Slots;
using AutoSchedule.Domain.Entities;

namespace AutoSchedule.BLL.Logic;

public interface IPriority
{
    public void Start();
    public List<Lesson> StartR();
    public Schedule Create();
}

public class Priority(IBaseService<Cabinet> cabinetRepository,
        IBaseService<Squad> squadRepository,
        IBaseService<Subject> subjectRepository,
        IBaseService<Teacher> teacherRepository,
        ILessonService lessonService) : IPriority
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
        CreateMap();

    }
    public List<Lesson> StartR()
    {
        GetStartList();

        var result = CreateMap().Lessons;
        return result;

        /*var result = _lessonService.GetAll().Data;
        return result;*/
    }
    public Schedule Create()
    {
        GetStartList();

        var result = CreateMap();
        return result;
    }

    public Schedule CreateMap()
    {
        var lId = 0;
        var schedule = new Schedule();
        // ...
        foreach (var squad in squads)
        {
            foreach (var subjectId in squad.SubjectIds)
            {
                var subject = subjects.FirstOrDefault(s => s.Id == subjectId);
                if (subject == null)
                {
                    continue; // Если предмет не найден, переходим к следующему
                }

                int lessonCount = 0;
                bool lessonScheduled = false;

                while (lessonCount < ScheduleConstants.MaxLessonsPerDay && !lessonScheduled)
                {
                    var lessonTime = CalculateLessonTime(lessonCount, DateTime.Now); // Расчет времени начала пары
                    var availableCabinet = FindAvailableCabinet(cabinets, lessonTime, schedule);
                    var availableTeacher = FindAvailableTeacher(teachers, subject, lessonTime, schedule);

                    if (availableTeacher != null && availableCabinet != null)
                    {
                        var lesson = new Lesson
                        {
                            Subject = subject,
                            Teacher = availableTeacher,
                            Cabinet = availableCabinet,
                            Squad = squad,
                            Time = lessonTime
                        };

                        if (schedule.AddLesson(lesson))
                        {
                            lessonScheduled = true; // Урок запланирован
                        }
                    }

                    if (!lessonScheduled)
                    {
                        lessonCount++; // Переход к следующему временному слоту
                    }
                }
            }
        }
        // ...





        return schedule;
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

    /*    public void CreateMap1()
        {
            var cabinetsMap = new Dictionary<string, int>();
            foreach (var cabinet in cabinets) 
            {
                cabinetsMap.Add(cabinet.Number, cabinet.NeedComputer);
            }
            var cabinetsMapS = cabinetsMap.OrderByDescending(x => x.Value).ToList();


            var subjectsMap = new Dictionary<string, int>();
            foreach (var cabinet in subjects)
            {
                subjectsMap.Add(cabinet.Name, cabinet.NeedComputer);
            }
            var subjectsMapS = subjectsMap.OrderByDescending(x => x.Value).ToList();


            var cabinetSubject = new Dictionary<string, string>();
            for (var i = 0; i < subjectsMapS.Count(); i++)
            {
                cabinetSubject.Add(subjectsMapS[i].Key, cabinetsMapS[i].Key);
            }

        }*/

    /* public async void CreateMap()
     {
         var shudeBit = new int[5, 4];
         var shudeSubject = new int[5, 4];
         var shudeCabinet = new int[5, 4];

         foreach (var squad in squads)
         {
             var sqadSubjects = _squadSubjectService.GetBySquad(squad.Id);

             var subjectList = new List<Subject>();

             foreach (var sqadSubject in sqadSubjects.Data)
             {
                 subjectList.Add(_subjectRepository.GetById(sqadSubject.Id).Result.Data);
             }

             foreach (var subject in subjectList)
             {
                 if (subject.NeedComputer == cabinets.OrderByDescending(x => x.Number).FirstOrDefault().NeedComputer)
                 {
                     for (int i = 0; i < 5; i++)
                     {
                         for (int j = 0; j < 4; j++)
                         {
                             if (shudeBit[i,j] == 0)
                             {
                                 shudeBit[i, j] = 1;
                                 shudeSubject[i, j] = subject.Id;
                                 shudeCabinet[i, j] = 1;
                             }
                         }
                     } 
                 }
             }

         }
     }*/
}

public class Schedule
{
    public List<Lesson> Lessons { get; set; }

    public Schedule()
    {
        Lessons = new List<Lesson>();
    }

    public bool AddLesson(Lesson lesson)
    {
        // Проверка на пересечения
        if (IsLessonConflicting(lesson))
        {
            return false; // Урок конфликтует с существующим расписанием
        }

        Lessons.Add(lesson);
        return true;
    }

    private bool IsLessonConflicting(Lesson newLesson)
    {
        foreach (var existingLesson in Lessons)
        {
            if (existingLesson.Time == newLesson.Time) // Проверяем совпадение времени
            {
                if (existingLesson.Cabinet.Id == newLesson.Cabinet.Id ||
                    existingLesson.Teacher.Id == newLesson.Teacher.Id ||
                    existingLesson.Squad.Id == newLesson.Squad.Id) // Проверяем, занята ли группа
                {
                    return true; // Найден конфликт
                }
            }
        }
        return false;
    }
}

public static class ScheduleConstants
{
    public static readonly TimeSpan StartOfDay = new TimeSpan(9, 0, 0); // 9:00 утра
    public static readonly TimeSpan DurationOfLesson = new TimeSpan(0, 90, 0); // 90 минут
    public static readonly TimeSpan BreakTime = new TimeSpan(0, 10, 0); // 10 минут
    public static readonly int MaxLessonsPerDay = 4; // Максимум 4 пары в день



}


