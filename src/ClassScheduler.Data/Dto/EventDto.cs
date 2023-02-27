using ClassScheduler.Domain;

namespace ClassScheduler.Data.Dto;

public class EventDto
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
}