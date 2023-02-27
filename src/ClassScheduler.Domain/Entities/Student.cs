namespace ClassScheduler.Domain.Entities;

public class Student : UserBase
{
    public Student(Guid id, string firstName, string lastName, string email) : 
        base(id, firstName, lastName, email)
    {}
}
