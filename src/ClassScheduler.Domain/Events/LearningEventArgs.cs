namespace ClassScheduler.Domain.Events;

public class LearningEventArgs
{
    public Guid? Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public int TotalHours { get; init; }
}