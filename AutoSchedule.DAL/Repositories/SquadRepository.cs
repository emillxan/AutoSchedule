using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public IQueryable<Squad> GetAll()
    {
        return _db.Squad;
    }

    public async Task<Squad> GetById(int id)
    {
        return await _db.Squad.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Squad>> GetByPage(int pageNumber, int pageSize)
    {
        return await _db.Squad
                         .OrderBy(s => s.Number)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
    }

    public async Task<int> GetCount()
    {
        return await _db.Squad.CountAsync();
    }

    public IQueryable<Squad> Search(string searchTerm)
    {
        var query = _db.Squad.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(t => t.Number.Contains(searchTerm));
        }

        return query;
    }

    public async Task<Squad> Update(Squad entity)
    {
        _db.Squad.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(Squad entity)
    {
        _db.Squad.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}
