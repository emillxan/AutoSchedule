using AutoSchedule.BLL.DTOs.Departments;
using AutoSchedule.BLL.DTOs.Squads;

namespace AutoSchedule.BLL.DTOs.Faculties;

public class FacultyReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<DepartmentReadDto> Departments { get; set; }
    public IEnumerable<SquadReadDto> Squads { get; set; }
}
