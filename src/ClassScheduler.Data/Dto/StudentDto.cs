namespace ClassScheduler.Data.Dto;

public class StudentDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PartitionKey { get; set; } = null!;
}