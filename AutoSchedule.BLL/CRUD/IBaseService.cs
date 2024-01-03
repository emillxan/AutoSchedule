using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD;

public interface IBaseService<T>
{
    IBaseResponse<List<T>> GetAll();
    Task<IBaseResponse<T>> GetById(int id);
    Task<IBaseResponse<T>> Edit(T model);
    Task<IBaseResponse<bool>> Delete(int id);
}
