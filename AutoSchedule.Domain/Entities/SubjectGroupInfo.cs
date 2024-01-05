namespace AutoSchedule.Domain.Entities;

public class SubjectGroupInfo
{
    public int SubjectId { get; set; }
    public int SquadId { get; set; }
    public int LectureHours { get; set; }
    public int SeminarHours { get; set; }
    public int LabHours { get; set; }
    public int TotalWeeks { get; set; }
}
