using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.DTOs;
using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.DAL.Repositories;

public class LessonRepository(AppDbContext db) : IBaseRepository<Lesson>
{
    private readonly AppDbContext _db = db;


    public async Task<bool> Create(Lesson entity)
    {
        await _db.Lesson.AddAsync(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Lesson> GetAll()
    {
        return _db.Lesson;
    }

    public async Task<Lesson> GetById(int id)
    {
        return await _db.Lesson.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Lesson>> GetByPage(int pageNumber, int pageSize)
    {
        return await _db.Lesson
                         .OrderBy(s => s.Id)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
    }

    public async Task<int> GetCount()
    {
        return await _db.Lesson.CountAsync();
    }

    public IQueryable<Lesson> Search(string searchTerm)
    {
        var query = _db.Lesson.AsQueryable();

/*        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(t => t.Name.Contains(searchTerm));
        }*/

        return query;
    }

    public async Task<Lesson> Update(Lesson entity)
    {
        _db.Lesson.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(Lesson entity)
    {
        _db.Lesson.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}
