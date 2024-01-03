using AutoSchedule.DAL.Interface;
using AutoSchedule.DAL.Repositories;
using AutoSchedule.Domain.DTOs.Cabinets;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.BLL.CRUD.Cabinets;

public class CabinetService(IBaseRepository<Cabinet> cabinetRepository) : ICabinetService
{
    private readonly IBaseRepository<Cabinet> _cabinetRepository = cabinetRepository;


    public async Task<IBaseResponse<Cabinet>> Create(CreateCabinetDTO model)
    {
        try
        {
            var cabinet = new Cabinet()
            {
                Number = model.Number,
            };
            await _cabinetRepository.Create(cabinet);

            return new BaseResponse<Cabinet>()
            {
                StatusCode = StatusCode.OK,
                Data = cabinet
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Cabinet>()
            {
                Description = $"[Create] : {ex.Message}",
            };
        }
    }

    public IBaseResponse<List<Cabinet>> GetAll()
    {
        try
        {
            var cabinets = _cabinetRepository.GetAll().ToList();

            if (!cabinets.Any())
            {
                return new BaseResponse<List<Cabinet>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Cabinet>>()
            {
                Data = cabinets,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Cabinet>>()
            {
                Description = $"[GetAll] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<Cabinet>> GetById(int id)
    {
        try
        {
            var cabinet = _cabinetRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (cabinet == null)
            {
                return new BaseResponse<Cabinet>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<Cabinet>()
            {
                Data = cabinet,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Cabinet>()
            {
                Description = $"[GetById] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<Cabinet>> Edit(Cabinet model)
    {
        try
        {
            var cabinet = await _cabinetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (cabinet == null)
            {
                return new BaseResponse<Cabinet>()
                {
                    Description = "Cabinet not found",
                };
            }

            cabinet.Number = model.Number;

            await _cabinetRepository.Update(cabinet);

            return new BaseResponse<Cabinet>()
            {
                Data = cabinet,
                StatusCode = StatusCode.OK,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Cabinet>()
            {
                Description = $"[Edit] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<bool>> Delete(int id)
    {
        try
        {
            var cabinet = await _cabinetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (cabinet == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Car not found",
                    Data = false
                };
            }
            await _cabinetRepository.Delete(cabinet);

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
                Description = $"[DeleteCar] : {ex.Message}",
            };
        }
    }
}
