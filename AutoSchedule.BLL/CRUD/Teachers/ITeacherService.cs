using AutoSchedule.Domain.DTOs.Teachers;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD.Teachers;

public interface ITeacherService : IBaseService<Teacher>
{
    Task<IBaseResponse<Teacher>> Create(CreateTeacherDTO model);
}
