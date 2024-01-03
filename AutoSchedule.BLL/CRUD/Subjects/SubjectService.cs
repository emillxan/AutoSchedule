using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.DTOs.Subjects;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.BLL.CRUD.Subjects;

public class SubjectService(IBaseRepository<Subject> subjectRepository) : ISubjectService
{
    private readonly IBaseRepository<Subject> _subjectRepository = subjectRepository;


    public async Task<IBaseResponse<Subject>> Create(CreateSubjectDTO model)
    {
        try
        {
            var subject = new Subject()
            {
                Name = model.Name,
                WeeklyFrequency = model.WeeklyFrequency
            };
            await _subjectRepository.Create(subject);     

            return new BaseResponse<Subject>()
            {
                StatusCode = StatusCode.OK,
                Data = subject
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Subject>()
            {
                Description = $"[Create] : {ex.Message}",
            };
        }
    }

    public IBaseResponse<List<Subject>> GetAll()
    {
        try
        {
            var subjects = _subjectRepository.GetAll().ToList();

            if (!subjects.Any())
            {
                return new BaseResponse<List<Subject>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Subject>>()
            {
                Data = subjects,
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

    public async Task<IBaseResponse<Subject>> GetById(int id)
    {
        try
        {
            var subject = _subjectRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (subject == null)
            {
                return new BaseResponse<Subject>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<Subject>()
            {
                Data = subject,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Subject>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<Subject>> Edit(Subject model)
    {
        try
        {
            var subject = await _subjectRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (subject == null)
            {
                return new BaseResponse<Subject>()
                {
                    Description = "Subject not found",
                };
            }

            subject.Name = model.Name;
            subject.WeeklyFrequency = model.WeeklyFrequency;

            await _subjectRepository.Update(subject);

            return new BaseResponse<Subject>()
            {
                Data = subject,
                StatusCode = StatusCode.OK,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Subject>()
            {
                Description = $"[Edit] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<bool>> Delete(int id)
    {
        try
        {
            var subject = await _subjectRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (subject == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Subject not found",
                    Data = false
                };
            }
            await _subjectRepository.Delete(subject);

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
