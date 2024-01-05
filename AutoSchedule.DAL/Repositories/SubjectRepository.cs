using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.DAL.Repositories;

public class SubjectRepository(AppDbContext db) : IBaseRepository<Subject>
{
    private readonly AppDbContext _db = db;


    public async Task<bool> Create(Subject entity)
    {
        await _db.Subject.AddAsync(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Subject> GetAll()
    {
        return _db.Subject;
    }

    public async Task<Subject> GetById(int id)
    {
        return await _db.Subject.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Subject>> GetByPage(int pageNumber, int pageSize)
    {
        return await _db.Subject
                         .OrderBy(s => s.Name)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
    }

    public async Task<int> GetCount()
    {
        return await _db.Subject.CountAsync();
    }

    public IQueryable<Subject> Search(string searchTerm)
    {
        var query = _db.Subject.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(t => t.Name.Contains(searchTerm));
        }

        return query;
    }

    public async Task<Subject> Update(Subject entity)
    {
        _db.Subject.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(Subject entity)
    {
        _db.Subject.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}
