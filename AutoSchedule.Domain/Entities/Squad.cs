using System.ComponentModel.DataAnnotations.Schema;

namespace AutoSchedule.Domain.Entities;

public class Squad
{
    public int Id { get; set; }
    public string Number { get; set; }
    public List<int> SubjectIds { get; set; }

    // public int NumberOfStudents { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }
}
