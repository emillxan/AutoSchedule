using System.ComponentModel.DataAnnotations;

namespace AutoSchedule.BLL.DTOs.Departments;

public class DepartmentCreateDto
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "FacultyId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "FacultyId must be a positive integer.")]
    public int FacultyId { get; set; }
}