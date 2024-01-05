﻿namespace AutoSchedule.Domain.Entities;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }
    public ICollection<Squad> Squads { get; set; }
}
