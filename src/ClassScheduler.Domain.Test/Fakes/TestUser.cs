using ClassScheduler.Domain.Entities;

namespace ClassScheduler.Domain.Test.Fakes;

public class TestUser : UserBase
{
    public TestUser(Guid id, string firstName, string lastName, string email)
        : base(id, firstName, lastName, email)
    {
    }

}