using AutoSchedule.Domain.DTOs;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD.Slots;

public interface ILessonService
{
    Task<IBaseResponse<Lesson>> Create(Lesson model);
    IBaseResponse<List<Lesson>> GetAll();
    Task<IBaseResponse<Lesson>> GetById(int id);
    Task<IBaseResponse<IQueryable<Lesson>>> GetBySquadId(int squadId);

    /*    Task<IBaseResponse<Lesson>> GetBySquadId(int id);
        Task<IBaseResponse<Lesson>> GetBySubjectId(int id);
        Task<IBaseResponse<Lesson>> GetByCabinetId(int id);
        Task<IBaseResponse<Lesson>> GetByTeacherId(int id);*/
}
