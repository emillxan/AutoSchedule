using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;

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

    public async Task<bool> Delete(Teacher entity)
    {
        _db.Teacher.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Teacher> GetAll()
    {
        return _db.Teacher;
    }

    public async Task<Teacher> Update(Teacher entity)
    {
        _db.Teacher.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
