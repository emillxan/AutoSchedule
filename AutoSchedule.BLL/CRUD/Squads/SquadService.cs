/*using AutoSchedule.BLL.CRUD.Squads;
using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.DTOs.Squads;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.BLL.CRUD.Cabinets;

public class SquadService(IBaseRepository<Squad> squadRepository) : ISquadService
{
    private readonly IBaseRepository<Squad> _squadRepository = squadRepository;


    public async Task<IBaseResponse<Squad>> Create(CreateSquadDTO model)
    {
        try
        {
            var subject = new Squad()
            {
                Number = model.Number,
                SubjectIds = model.SubjectIds,
            };
            await _squadRepository.Create(subject);

            return new BaseResponse<Squad>()
            {
                StatusCode = StatusCode.OK,
                Data = subject
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Squad>()
            {
                Description = $"[Create] : {ex.Message}",
            };
        }
    }

    public IBaseResponse<List<Squad>> GetAll()
    {
        try
        {
            var squads = _squadRepository.GetAll().ToList();

            if (!squads.Any())
            {
                return new BaseResponse<List<Squad>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Squad>>()
            {
                Data = squads,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Squad>>()
            {
                Description = $"[GetAll] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<Squad>> GetById(int id)
    {
        try
        {
            var squad = _squadRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (squad == null)
            {
                return new BaseResponse<Squad>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<Squad>()
            {
                Data = squad,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Squad>()
            {
                Description = $"[GetById] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<Squad>> Edit(Squad model)
    {
        try
        {
            var squad = await _squadRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (squad == null)
            {
                return new BaseResponse<Squad>()
                {
                    Description = "Squad not found",
                };
            }

            squad.Number = model.Number;
            squad.SubjectIds = model.SubjectIds;

            await _squadRepository.Update(squad);

            return new BaseResponse<Squad>()
            {
                Data = squad,
                StatusCode = StatusCode.OK,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Squad>()
            {
                Description = $"[Edit] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<bool>> Delete(int id)
    {
        try
        {
            var squad = await _squadRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (squad == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Squad not found",
                    Data = false
                };
            }
            await _squadRepository.Delete(squad);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Delete] : {ex.Message}",
            };
        }
    }
}
*/