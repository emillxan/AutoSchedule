namespace AutoSchedule.BLL.DTOs.Squads;

public class SquadReadDto
{
    public int Id { get; set; }
    public string Number { get; set; }
    public List<int> SubjectIds { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } 
    public int FacultyId { get; set; }
    public string FacultyName { get; set; }
}
