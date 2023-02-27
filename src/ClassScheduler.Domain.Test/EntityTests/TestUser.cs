using ClassScheduler.Domain.Entities;

namespace ClassScheduler.Domain.Test.EntityTests;

public class TestUser : UserBase
{
    public TestUser(string firstName, string lastName, string email) 
        : base(firstName, lastName, email)
    {
    }

}