using AutoSchedule.Domain.DTOs.Subjects;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD.Subjects;

public interface ISubjectService : IBaseService<Subject>
{
    Task<IBaseResponse<Subject>> Create(CreateSubjectDTO model);
}
