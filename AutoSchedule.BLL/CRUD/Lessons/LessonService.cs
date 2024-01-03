using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.DTOs;
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
                SquadId = model.SquadId,
                CabinetId = model.CabinetId,
                SubjectId = model.SubjectId,
                TeacherId = model.TeacherId,
                Time = model.Time,
                DayOfWeek = model.DayOfWeek,
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

    public async Task<IBaseResponse<IQueryable<Lesson>>> GetBySquadId(int squadId)
    {
        try
        {
            var subjects = _subjectRepository.GetAll().Where(x => x.SquadId == squadId);

            if (!subjects.Any())
            {
                return new BaseResponse<IQueryable<Lesson>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<IQueryable<Lesson>>()
            {
                Data = subjects,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IQueryable<Lesson>>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }

}
