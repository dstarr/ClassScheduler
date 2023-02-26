using ClassScheduler.Domain.Events;

namespace ClassScheduler.Domain.Test.EventsTests;

[TestClass]
public class LearningEventTest
{
    private readonly LearningEventArgs _learningEventArgs = new LearningEventArgs()
    {
        Id = Guid.NewGuid(),
        StartTime = DateTime.Now.AddDays(5),
        TotalHours = 12,
        Description = "Description",
        EndTime = DateTime.Now.AddDays(6),
        Title = "Title",
    };

    [TestMethod]
    public void CanConstructWithoutId()
    {
        var args = new LearningEventArgs()
        {
            StartTime = DateTime.Now.AddDays(5),
            TotalHours = 12,
            Description = "Description",
            EndTime = DateTime.Now.AddDays(6),
            Title = "Title",
        };
        
        var learningEvent = new LearningEvent(args);
    }

    [TestMethod]
    public void CanConstructWithId()
    {
        var learningEvent = new LearningEvent(_learningEventArgs);
    }
}
