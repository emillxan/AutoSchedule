using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;

namespace AutoSchedule.DAL.Repositories;

public class CabinetRepository(AppDbContext db) : IBaseRepository<Cabinet>
{
    private readonly AppDbContext _db = db;
    public async Task<bool> Create(Cabinet entity)
    {
        await _db.Cabinet.AddAsync(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(Cabinet entity)
    {
        _db.Cabinet.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Cabinet> GetAll()
    {
        return _db.Cabinet;
    }

    public async Task<Cabinet> Update(Cabinet entity)
    {
        _db.Cabinet.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
