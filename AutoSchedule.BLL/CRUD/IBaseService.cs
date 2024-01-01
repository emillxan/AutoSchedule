using AutoSchedule.Domain.Responce;

namespace AutoSchedule.BLL.CRUD;

public interface IBaseService<T>
{
    IBaseResponse<List<T>> GetAll();
}
