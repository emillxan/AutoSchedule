using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.DAL.Repositories;

public class TeacherRepository(AppDbContext db) : IBaseRepository<Teacher>
{
    private readonly AppDbContext _db = db;


    public async Task<bool> Create(Teacher entity)
    {
        await _db.Teacher.AddAsync(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Teacher> GetAll()
    {
        return _db.Teacher;
    }

    public async Task<Teacher> GetById(int id)
    {
        return await _db.Teacher.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Teacher>> GetByPage(int pageNumber, int pageSize)
    {
        return await _db.Teacher
                         .OrderBy(s => s.Name)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
    }

    public async Task<int> GetCount()
    {
        return await _db.Teacher.CountAsync();
    }

    public IQueryable<Teacher> Search(string searchTerm)
    {
        var query = _db.Teacher.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(t => t.Name.Contains(searchTerm));
        }

        return query;
    }

    public async Task<Teacher> Update(Teacher entity)
    {
        _db.Teacher.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(Teacher entity)
    {
        _db.Teacher.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}
