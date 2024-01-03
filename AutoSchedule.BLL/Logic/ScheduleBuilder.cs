using AutoSchedule.BLL.CRUD;
using AutoSchedule.BLL.CRUD.Slots;
using AutoSchedule.Domain.DTOs;
using AutoSchedule.Domain.Entities;

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

    public Schedule GenerateWeeklySchedule()
    {
        var schedule = new Schedule();
        Dictionary<int, int> subjectWeeklyCount = new Dictionary<int, int>();

        foreach (var squad in squads)
        {
            subjectWeeklyCount.Clear();

            for (int day = 0; day < 5; day++) // Пять учебных дней
            {
                int lessonCount = 0;
                DateTime currentDate = DateTime.Now.AddDays(day); // Дата для текущего учебного дня

                foreach (var subjectId in squad.SubjectIds)
                {
                    if (!subjectWeeklyCount.ContainsKey(subjectId))
                    {
                        subjectWeeklyCount[subjectId] = 0;
                    }

                    var subject = subjects.FirstOrDefault(s => s.Id == subjectId);
                    if (subject == null || subjectWeeklyCount[subjectId] >= subject.WeeklyFrequency)
                    {
                        continue; // Переходим к следующему предмету, если текущий уже достиг своей недельной частоты
                    }

                    while (lessonCount < ScheduleConstants.MaxLessonsPerDay)
                    {
                        var lessonTime = CalculateLessonTime(lessonCount, currentDate); // Расчет времени начала пары
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
                                Time = lessonTime
                            };

                            if (schedule.AddLesson(lesson))
                            {
                                subjectWeeklyCount[subjectId]++; // Увеличиваем счетчик проведенных уроков по предмету
                                lessonCount++;
                                break; // Урок запланирован, переходим к следующему предмету
                            }
                        }
                        lessonCount++; // Переход к следующему временному слоту
                    }
                }
            }
        }

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
}
