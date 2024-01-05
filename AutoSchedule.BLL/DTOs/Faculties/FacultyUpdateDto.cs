using System.ComponentModel.DataAnnotations;

namespace AutoSchedule.BLL.DTOs.Faculties;

public class FacultyUpdateDto
{
    [Required(ErrorMessage = "Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}
