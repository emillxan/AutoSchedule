using AutoSchedule.BLL.DTOs.Squads;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.Interfaces;

public interface ISquadService
{
    Task<IBaseResponse<SquadReadDto>> Create(SquadCreateDto model);
    Task<IBaseResponse<IEnumerable<SquadReadDto>>> GetAll();
    Task<IBaseResponse<SquadReadDto>> GetById(int id);
    Task<IBaseResponse<IEnumerable<SquadReadDto>>> GetByPage(int pageNumber, int pageSize);
    Task<IBaseResponse<int>> GetCount();
    Task<IBaseResponse<IEnumerable<SquadReadDto>>> Search(string searchTerm);
    Task<IBaseResponse<SquadReadDto>> Update(SquadUpdateDto model);
    Task<IBaseResponse<bool>> Delete(int id);
}
