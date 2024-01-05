using System.ComponentModel.DataAnnotations;

namespace AutoSchedule.BLL.DTOs.Squads;

public class SquadCreateDto
{
    [Required(ErrorMessage = "Number is required")]
    [StringLength(10, ErrorMessage = "Number cannot be longer than 10 characters")]
    public string Number { get; set; }

    [Required(ErrorMessage = "Subject IDs are required")]
    public List<int> SubjectIds { get; set; }

    [Required(ErrorMessage = "Department ID is required")]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "Faculty ID is required")]
    public int FacultyId { get; set; }
}
