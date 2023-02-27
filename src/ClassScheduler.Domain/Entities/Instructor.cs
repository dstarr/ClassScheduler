namespace ClassScheduler.Domain.Entities;

public class Instructor : UserBase
{
    public Instructor(string firstName, string lastName, string email)
        : base(firstName, lastName, email)
    {

    }
}