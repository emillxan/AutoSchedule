using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.DAL.Repositories;

public class FacultyRepository(AppDbContext db) : IBaseRepository<Faculty>
{
    private readonly AppDbContext _db = db;


    public async Task<bool> Create(Faculty entity)
    {
        await _db.Faculty.AddAsync(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Faculty> GetAll()
    {
        return _db.Faculty;
    }

    public async Task<Faculty> GetById(int id)
    {
        return await _db.Faculty.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Faculty>> GetByPage(int pageNumber, int pageSize)
    {
        return await _db.Faculty
                         .OrderBy(s => s.Name)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
    }

    public async Task<int> GetCount()
    {
        return await _db.Faculty.CountAsync();
    }

    public IQueryable<Faculty> Search(string searchTerm)
    {
        var query = _db.Faculty.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(t => t.Name.Contains(searchTerm));
        }

        return query;
    }

    public async Task<Faculty> Update(Faculty entity)
    {
        _db.Faculty.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(Faculty entity)
    {
        _db.Faculty.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}
