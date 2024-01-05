namespace AutoSchedule.Domain.Entities;

public class Faculty
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Навигационное свойство для связанных кафедр
    public ICollection<Department> Departments { get; set; }
    public ICollection<Squad> Squads { get; set; }
}
