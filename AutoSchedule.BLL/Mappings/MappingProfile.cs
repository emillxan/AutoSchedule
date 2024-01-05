using AutoMapper;
using AutoSchedule.BLL.DTOs.Departments;
using AutoSchedule.BLL.DTOs.Faculties;
using AutoSchedule.BLL.DTOs.Squads;
using AutoSchedule.Domain.Entities;

namespace AutoSchedule.BLL.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Squad, SquadReadDto>();
        CreateMap<SquadCreateDto, Squad>();
        CreateMap<SquadUpdateDto, Squad>()
            .ForMember(squad => squad.Id, opt => opt.Ignore());

        CreateMap<Faculty, FacultyReadDto>();
        CreateMap<FacultyCreateDto, Faculty>();
        CreateMap<FacultyUpdateDto, Faculty>()
            .ForMember(faculty => faculty.Id, opt => opt.Ignore());

        CreateMap<Department, DepartmentReadDto>();
        CreateMap<DepartmentCreateDto, Department>();
        CreateMap<DepartmentUpdateDto, Department>()
            .ForMember(department => department.Id, opt => opt.Ignore());
    }
}
