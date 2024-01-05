using AutoSchedule.BLL.DTOs.Faculties;
using AutoSchedule.BLL.DTOs.Squads;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.Interfaces;

public interface IFacultyService
{
    Task<IBaseResponse<FacultyReadDto>> Create(FacultyCreateDto model);
    Task<IBaseResponse<IEnumerable<FacultyReadDto>>> GetAll();
    Task<IBaseResponse<FacultyReadDto>> GetById(int id);
    Task<IBaseResponse<IEnumerable<FacultyReadDto>>> GetByPage(int pageNumber, int pageSize);
    Task<IBaseResponse<int>> GetCount();
    Task<IBaseResponse<IEnumerable<FacultyReadDto>>> Search(string searchTerm);
    Task<IBaseResponse<FacultyReadDto>> Update(FacultyUpdateDto model);
    Task<IBaseResponse<bool>> Delete(int id);
}
