namespace ClassScheduler.Data.Dto;

public class LearningEventDto
{
    public int StudentCapacity { get; set; }

    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int TotalHours { get; set; }

    public List<StudentDto> Students { get; set; } = null!;
}
