namespace AutoSchedule.Domain.DTOs.Teachers;

public class CreateTeacherDTO
{
    public string Name { get; set; }
    public List<int> SubjectIds { get; set; }
}
