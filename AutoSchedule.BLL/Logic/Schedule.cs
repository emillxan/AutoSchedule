using AutoSchedule.BLL.CRUD.Slots;
using AutoSchedule.Domain.DTOs;

namespace AutoSchedule.BLL.Logic;

public class Schedule
{
    public List<LessonDTO> Lessons { get; set; }

    public Schedule()
    {
        Lessons = new List<LessonDTO>();
    }

    public bool AddLesson(LessonDTO lesson)
    {
        // Проверка на пересечения
        if (IsLessonConflicting(lesson))
        {
            return false; // Урок конфликтует с существующим расписанием
        }

        Lessons.Add(lesson);
        return true;
    }

    private bool IsLessonConflicting(LessonDTO newLesson)
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
