using AutoSchedule.BLL.Interfaces;
using AutoSchedule.BLL.Services;
using AutoSchedule.DAL.Interface;
using AutoSchedule.DAL.Repositories;
using AutoSchedule.Domain.Entities;

namespace AutoSchedule;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<Squad>, SquadRepository>();
        services.AddScoped<IBaseRepository<Cabinet>, CabinetRepository>();
        services.AddScoped<IBaseRepository<Subject>, SubjectRepository>();
        services.AddScoped<IBaseRepository<Teacher>, TeacherRepository>();
        services.AddScoped<IBaseRepository<Lesson>, LessonRepository>();
        services.AddScoped<IBaseRepository<Faculty>, FacultyRepository>();
        services.AddScoped<IBaseRepository<Department>, DepartmentRepository>();


    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<ISquadService, SquadService>();
        services.AddScoped<IFacultyService, FacultyService>();
        services.AddScoped<IDepartmentService, DepartmentService>();

    }
}
