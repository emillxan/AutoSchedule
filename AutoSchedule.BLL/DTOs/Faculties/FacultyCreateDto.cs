using System.ComponentModel.DataAnnotations;

namespace AutoSchedule.BLL.DTOs.Faculties;

public class FacultyCreateDto
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}
