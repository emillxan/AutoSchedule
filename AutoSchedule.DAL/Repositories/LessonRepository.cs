using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.DTOs;
using AutoSchedule.Domain.Entities;

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

    public async Task<bool> Delete(Lesson entity)
    {
        _db.Lesson.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Lesson> GetAll()
    {
        return _db.Lesson;
    }

    public async Task<Lesson> Update(Lesson entity)
    {
        _db.Lesson.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
