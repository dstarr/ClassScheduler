namespace ClassScheduler.Domain.Entities;

public class LearningEventArgs
{
    public Guid? Id { get; init; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TotalHours { get; set; }
    public int MaxStudents { get; set; }
    public List<Student> Students { get; set; } = null!;
}