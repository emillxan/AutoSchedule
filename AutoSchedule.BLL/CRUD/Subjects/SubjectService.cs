using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD.Subjects;

public class SubjectService(IBaseRepository<Subject> cabinetRepository) : IBaseService<Subject>
{
    private readonly IBaseRepository<Subject> _cabinetRepository = cabinetRepository;

    public IBaseResponse<List<Subject>> GetAll()
    {
        try
        {
            var cabines = _cabinetRepository.GetAll().ToList();

            if (!cabines.Any())
            {
                return new BaseResponse<List<Subject>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Subject>>()
            {
                Data = cabines,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Subject>>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }
}
