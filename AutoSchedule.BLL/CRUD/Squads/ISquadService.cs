using AutoSchedule.Domain.DTOs.Squads;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD.Squads;

public interface ISquadService : IBaseService<Squad>
{
    Task<IBaseResponse<Squad>> Create(CreateSquadDTO model);
}
