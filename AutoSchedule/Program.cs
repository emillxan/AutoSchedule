using AutoSchedule;
using AutoSchedule.BLL.Interfaces;
using AutoSchedule.BLL.Logic;
using AutoSchedule.BLL.Mappings;
using AutoSchedule.BLL.Services;
using AutoSchedule.DAL;
using AutoSchedule.DAL.Interface;
using AutoSchedule.DAL.Repositories;
using AutoSchedule.Domain.DTOs;
using AutoSchedule.Domain.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Test"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.InitializeRepositories();
builder.Services.InitializeServices();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "Описание API"
    });
    var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});



/*builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ILessonDTOService, LessonDTOService>();*/

/*
builder.Services.AddScoped<IScheduleBuilder, ScheduleBuilder>();*/


builder.Services.AddCors();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Auto Schedule API V1");
});

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

app.UseAuthorization();

app.MapControllers();

app.Run();
