using AutoSchedule.Domain.Enums;

namespace AutoSchedule.Domain.Responce;

public interface IBaseResponse<T>
{
    string Description { get; }
    T Data { get; }
    StatusCode StatusCode { get; }
}
