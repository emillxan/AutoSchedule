namespace AutoSchedule.Domain.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> SubjectIds { get; set; }
}
