using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public IQueryable<Cabinet> GetAll()
    {
        return _db.Cabinet;
    }

    public async Task<Cabinet> GetById(int id)
    {
        return await _db.Cabinet.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Cabinet>> GetByPage(int pageNumber, int pageSize)
    {
        return await _db.Cabinet
                         .OrderBy(s => s.Number)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
    }

    public async Task<int> GetCount()
    {
        return await _db.Cabinet.CountAsync();
    }

    public IQueryable<Cabinet> Search(string searchTerm)
    {
        var query = _db.Cabinet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(t => t.Number.Contains(searchTerm));
        }

        return query;
    }

    public async Task<Cabinet> Update(Cabinet entity)
    {
        _db.Cabinet.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(Cabinet entity)
    {
        _db.Cabinet.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}
