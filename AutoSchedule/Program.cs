using AutoSchedule.BLL.CRUD;
using AutoSchedule.BLL.CRUD.Cabinets;
using AutoSchedule.BLL.CRUD.Slots;
using AutoSchedule.BLL.CRUD.Squads;
using AutoSchedule.BLL.CRUD.Subjects;
using AutoSchedule.BLL.CRUD.Teachers;
using AutoSchedule.BLL.DTOs.Lessons;
using AutoSchedule.BLL.Logic;
using AutoSchedule.DAL;
using AutoSchedule.DAL.Interface;
using AutoSchedule.DAL.Repositories;
using AutoSchedule.Domain.DTOs;
using AutoSchedule.Domain.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Test"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IBaseRepository<Squad>, SquadRepository>();
builder.Services.AddScoped<IBaseRepository<Cabinet>, CabinetRepository>();
builder.Services.AddScoped<IBaseRepository<Subject>, SubjectRepository>();
builder.Services.AddScoped<IBaseRepository<Teacher>, TeacherRepository>();
builder.Services.AddScoped<IBaseRepository<Lesson>, LessonRepository>();


builder.Services.AddScoped<IBaseService<Squad>, SquadService>();
builder.Services.AddScoped<ISquadService, SquadService>();
builder.Services.AddScoped<IBaseService<Subject>, SubjectService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IBaseService<Teacher>, TeacherService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IBaseService<Cabinet>, CabinetService>();
builder.Services.AddScoped<ICabinetService, CabinetService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ILessonDTOService, LessonDTOService>();


builder.Services.AddScoped<IScheduleBuilder, ScheduleBuilder>();


builder.Services.AddCors();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));


app.UseAuthorization();

app.MapControllers();

app.Run();
