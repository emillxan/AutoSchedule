using AutoSchedule.Domain.DTOs;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchedule.BLL.DTOs.Lessons;

public interface ILessonDTOService
{
    Task<IBaseResponse<LessonDTO>> GetDTOByLesson(Lesson Lesson);
}
