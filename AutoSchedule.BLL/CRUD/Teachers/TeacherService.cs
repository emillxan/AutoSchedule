using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.DTOs.Teachers;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.BLL.CRUD.Teachers;

public class TeacherService(IBaseRepository<Teacher> teacherRepository) : ITeacherService
{
    private readonly IBaseRepository<Teacher> _teacherRepository = teacherRepository;


    public async Task<IBaseResponse<Teacher>> Create(CreateTeacherDTO model)
    {
        try
        {
            var teacher = new Teacher()
            {
                Name = model.Name,
                SubjectIds = model.SubjectIds,
            };
            await _teacherRepository.Create(teacher);

            return new BaseResponse<Teacher>()
            {
                StatusCode = StatusCode.OK,
                Data = teacher
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Teacher>()
            {
                Description = $"[Create] : {ex.Message}",
            };
        }
    }

    public IBaseResponse<List<Teacher>> GetAll()
    {
        try
        {
            var teachers = _teacherRepository.GetAll().ToList();

            if (!teachers.Any())
            {
                return new BaseResponse<List<Teacher>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Teacher>>()
            {
                Data = teachers,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Teacher>>()
            {
                Description = $"[GetAll] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<Teacher>> GetById(int id)
    {
        try
        {
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (teacher == null)
            {
                return new BaseResponse<Teacher>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<Teacher>()
            {
                Data = teacher,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Teacher>()
            {
                Description = $"[GetById] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<Teacher>> Edit(Teacher model)
    {
        try
        {
            var teacher = await _teacherRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
            if (teacher == null)
            {
                return new BaseResponse<Teacher>()
                {
                    Description = "Teacher not found",
                };
            }

            teacher.Name = model.Name;
            teacher.SubjectIds = model.SubjectIds;

            await _teacherRepository.Update(teacher);

            return new BaseResponse<Teacher>()
            {
                Data = teacher,
                StatusCode = StatusCode.OK,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Teacher>()
            {
                Description = $"[Edit] : {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<bool>> Delete(int id)
    {
        try
        {
            var teacher = await _teacherRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (teacher == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Teacher not found",
                    Data = false
                };
            }
            await _teacherRepository.Delete(teacher);

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
