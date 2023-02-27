namespace ClassScheduler.Domain.Entities;

public class Admin : UserBase
{
    public Admin(Guid id, string firstName, string lastName, string email) : 
        base(id, firstName, lastName, email)
    {
    }
}