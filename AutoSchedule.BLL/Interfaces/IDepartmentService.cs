using AutoSchedule.BLL.DTOs.Departments;
using AutoSchedule.BLL.DTOs.Faculties;
using AutoSchedule.BLL.DTOs.Squads;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.Interfaces;

public interface IDepartmentService
{
    Task<IBaseResponse<DepartmentReadDto>> Create(DepartmentCreateDto model);
    Task<IBaseResponse<IEnumerable<DepartmentReadDto>>> GetAll();
    Task<IBaseResponse<DepartmentReadDto>> GetById(int id);
    Task<IBaseResponse<IEnumerable<DepartmentReadDto>>> GetByPage(int pageNumber, int pageSize);
    Task<IBaseResponse<int>> GetCount();
    Task<IBaseResponse<IEnumerable<DepartmentReadDto>>> Search(string searchTerm);
    Task<IBaseResponse<DepartmentReadDto>> Update(DepartmentUpdateDto model);
    Task<IBaseResponse<bool>> Delete(int id);
}
