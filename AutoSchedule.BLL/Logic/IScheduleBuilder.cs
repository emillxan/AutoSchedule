using AutoSchedule.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchedule.BLL.Logic;

public interface IScheduleBuilder
{
    public void Start();
    public List<LessonDTO> StartR();
    public Schedule Create();
}
