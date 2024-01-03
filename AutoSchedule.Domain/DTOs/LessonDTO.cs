using AutoSchedule.Domain.Entities;

namespace AutoSchedule.Domain.DTOs;

public class LessonDTO
{
    public int Id { get; set; }
    public Squad Squad { get; set; }
    public Subject Subject { get; set; }
    public Cabinet Cabinet { get; set; }
    public Teacher Teacher { get; set; }
    public DateTime Time { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
}
