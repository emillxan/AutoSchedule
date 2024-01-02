﻿namespace AutoSchedule.Domain.Entities;

public class Squad
{
    public int Id { get; set; }
    public string Number { get; set; }
    public List<int> SubjectIds { get; set; }
}
