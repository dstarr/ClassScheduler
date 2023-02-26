namespace ClassScheduler.Domain.Events;

public class LearningEventArgs
{
    public Guid? Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TotalHours { get; set; }
}