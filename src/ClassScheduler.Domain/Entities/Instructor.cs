namespace ClassScheduler.Domain.Entities;

public class Instructor : UserBase
{
    public Instructor(Guid id, string firstName, string lastName, string email) : 
        base(id, firstName, lastName, email)
    {
    }
}