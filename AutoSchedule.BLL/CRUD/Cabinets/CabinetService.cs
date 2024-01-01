using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD.Cabinets;

public class CabinetService(IBaseRepository<Cabinet> subjectRepository) : IBaseService<Cabinet>
{
    private readonly IBaseRepository<Cabinet> _subjectRepository = subjectRepository;

    public IBaseResponse<List<Cabinet>> GetAll()
    {
        try
        {
            var subjects = _subjectRepository.GetAll().ToList();

            if (!subjects.Any())
            {
                return new BaseResponse<List<Cabinet>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Cabinet>>()
            {
                Data = subjects,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Cabinet>>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }
}
