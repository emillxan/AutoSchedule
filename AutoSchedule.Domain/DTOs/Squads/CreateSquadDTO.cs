namespace AutoSchedule.Domain.DTOs.Squads;

public class CreateSquadDTO
{
    public string Number { get; set; }
    public List<int> SubjectIds { get; set; }
}
