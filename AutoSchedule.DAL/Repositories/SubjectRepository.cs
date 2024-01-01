using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;

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

    public async Task<bool> Delete(Subject entity)
    {
        _db.Subject.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Subject> GetAll()
    {
        return _db.Subject;
    }

    public async Task<Subject> Update(Subject entity)
    {
        _db.Subject.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}
