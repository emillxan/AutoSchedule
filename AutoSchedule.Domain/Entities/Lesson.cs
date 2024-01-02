namespace AutoSchedule.Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public Squad Squad { get; set; }
    public Subject Subject { get; set; }
    public Cabinet Cabinet { get; set; }
    public Teacher Teacher { get; set; }
    public DateTime Time { get; set; }
}
