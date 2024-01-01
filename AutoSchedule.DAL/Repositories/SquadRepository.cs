using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;

namespace AutoSchedule.DAL.Repositories;

public class SquadRepository(AppDbContext db) : IBaseRepository<Squad>
{
    private readonly AppDbContext _db = db;
    public async Task<bool> Create(Squad entity)
    {
        await _db.Squad.AddAsync(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(Squad entity)
    {
        _db.Squad.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Squad> GetAll()
    {
        return _db.Squad;
    }

    public async Task<Squad> Update(Squad entity)
    {
        _db.Squad.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
