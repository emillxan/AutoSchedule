using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD.Teachers;

public class TeacherService(IBaseRepository<Teacher> cabinetRepository) : IBaseService<Teacher>
{
    private readonly IBaseRepository<Teacher> _cabinetRepository = cabinetRepository;

    public IBaseResponse<List<Teacher>> GetAll()
    {
        try
        {
            var cabines = _cabinetRepository.GetAll().ToList();

            if (!cabines.Any())
            {
                return new BaseResponse<List<Teacher>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Teacher>>()
            {
                Data = cabines,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Teacher>>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<Teacher>> GetById(int id)
    {
        try
        {
            var cabines = _cabinetRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (cabines == null)
            {
                return new BaseResponse<Teacher>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<Teacher>()
            {
                Data = cabines,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Teacher>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }
}
