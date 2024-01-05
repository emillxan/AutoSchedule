using AutoSchedule.BLL.CRUD.Slots;
using AutoSchedule.BLL.CRUD;
using AutoSchedule.DAL.Repositories;
using AutoSchedule.Domain.DTOs;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoSchedule.Domain.Enums;
namespace AutoSchedule.BLL.DTOs.Lessons;

public class LessonDTOService : ILessonDTOService
{
    private readonly IBaseService<Cabinet> _cabinetRepository;
    private readonly IBaseService<Squad> _squadRepository;
    private readonly IBaseService<Subject> _subjectRepository;
    private readonly IBaseService<Teacher> _teacherRepository;
    private readonly ILessonService _lessonService;
    public LessonDTOService(IBaseService<Cabinet> cabinetRepository,
        IBaseService<Squad> squadRepository,
        IBaseService<Subject> subjectRepository,
        IBaseService<Teacher> teacherRepository,
        ILessonService lessonService)
    {
        _cabinetRepository = cabinetRepository;
        _squadRepository = squadRepository;
        _subjectRepository = subjectRepository;
        _teacherRepository = teacherRepository;
        _lessonService = lessonService;
    }

    public async Task<IBaseResponse<LessonDTO>> GetDTOByLesson(Lesson Lesson)
    {
        try
        {
            var cabinet = _cabinetRepository.GetById(Lesson.CabinetId).Result.Data;
            var squad = _squadRepository.GetById(Lesson.SquadId).Result.Data;
            var subject = _subjectRepository.GetById(Lesson.SubjectId).Result.Data;
            var teacher = _teacherRepository.GetById(Lesson.TeacherId).Result.Data;

            var lessonDTO = new LessonDTO()
            {
                Id = Lesson.Id,
                Cabinet = cabinet,
                Squad = squad,
                Subject = subject,
                Teacher = teacher,
                Time = Lesson.Time,
                DayOfWeek = Lesson.DayOfWeek,
                WeekType = Lesson.WeekType == 0 ? WeekType.UpperWeek : WeekType.LowerWeek,
            };

            return new BaseResponse<LessonDTO>()
            {
                Data = lessonDTO,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<LessonDTO>()
            {
                Description = $"[GetCars] : {ex.Message}",
            };
        }
    }
}
