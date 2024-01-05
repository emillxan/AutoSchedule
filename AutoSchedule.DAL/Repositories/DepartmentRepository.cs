using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.DAL.Repositories;

public class DepartmentRepository(AppDbContext db) : IBaseRepository<Department>
{
    private readonly AppDbContext _db = db;


    public async Task<bool> Create(Department entity)
    {
        await _db.Department.AddAsync(entity);
        await _db.SaveChangesAsync();

        return true;
    }

    public IQueryable<Department> GetAll()
    {
        return _db.Department;
    }

    public async Task<Department> GetById(int id)
    {
        return await _db.Department.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Department>> GetByPage(int pageNumber, int pageSize)
    {
        return await _db.Department
                         .OrderBy(s => s.Name)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
    }

    public async Task<int> GetCount()
    {
        return await _db.Department.CountAsync();
    }

    public IQueryable<Department> Search(string searchTerm)
    {
        var query = _db.Department.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(t => t.Name.Contains(searchTerm));
        }

        return query;
    }

    public async Task<Department> Update(Department entity)
    {
        _db.Department.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> Delete(Department entity)
    {
        _db.Department.Remove(entity);
        await _db.SaveChangesAsync();

        return true;
    }
}
