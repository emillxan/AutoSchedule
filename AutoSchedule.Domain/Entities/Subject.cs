namespace AutoSchedule.Domain.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int WeeklyFrequency { get; set; }

    public int TotalHours { get; set; }
}


