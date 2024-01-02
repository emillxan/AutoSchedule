using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD.Slots;

public class LessonService(IBaseRepository<Lesson> subjectRepository) : ILessonService
{
    private readonly IBaseRepository<Lesson> _subjectRepository = subjectRepository;

    public async Task<IBaseResponse<Lesson>> Create(Lesson model)
    {
        try
        {
            var slot = new Lesson()
            {
                Squad = model.Squad,
                Cabinet = model.Cabinet,
                Subject = model.Subject,
                Teacher = model.Teacher,
                Time = model.Time,
            };
            await _subjectRepository.Create(slot);

            return new BaseResponse<Lesson>()
            {
                Data = slot,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Lesson>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }

    public IBaseResponse<List<Lesson>> GetAll()
    {
        try
        {
            var subjects = _subjectRepository.GetAll().ToList();

            if (!subjects.Any())
            {
                return new BaseResponse<List<Lesson>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Lesson>>()
            {
                Data = subjects,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Lesson>>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }


    public async Task<IBaseResponse<Lesson>> GetById(int id)
    {
        try
        {
            var subjects = _subjectRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (subjects == null)
            {
                return new BaseResponse<Lesson>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<Lesson>()
            {
                Data = subjects,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Lesson>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }


}
