using AutoSchedule.BLL.CRUD;
using AutoSchedule.BLL.CRUD.Cabinets;
using AutoSchedule.BLL.CRUD.Subjects;
using AutoSchedule.DAL;
using AutoSchedule.DAL.Interface;
using AutoSchedule.DAL.Repositories;
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


builder.Services.AddScoped<IBaseService<Squad>, SquadService>();
builder.Services.AddScoped<IBaseService<Cabinet>, CabinetService>();
builder.Services.AddScoped<IBaseService<Subject>, SubjectService>();




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
