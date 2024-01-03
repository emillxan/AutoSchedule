namespace AutoSchedule.Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public int SquadId { get; set; }
    public int SubjectId { get; set; }
    public int CabinetId { get; set; }
    public int TeacherId { get; set; }
    public DateTime Time { get; set; } 
}
