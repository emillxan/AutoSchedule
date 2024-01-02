﻿using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD.Cabinets;

public class SquadService(IBaseRepository<Squad> squadRepository) : IBaseService<Squad>
{
    private readonly IBaseRepository<Squad> _squadRepository = squadRepository;

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
                Description = $"[GetCars] : {ex.Message}",
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
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }
}
