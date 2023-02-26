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

    [TestMethod]
    public void CannotConstructWithNullLearningEventArgs()
    {
        Assert.ThrowsException<System.ArgumentNullException>(() => new LearningEvent(null));
    }

    [TestMethod]
    public void StartDateCannotBeInThePast()
    {
        _learningEventArgs.StartTime = DateTime.Now.AddDays(-1);

        Assert.ThrowsException<System.ArgumentException>(() => new LearningEvent(_learningEventArgs));
    }

    [TestMethod]
    public void EndDateCannotBeInThePast()
    {
        _learningEventArgs.EndTime = DateTime.Now.AddDays(-1);

        Assert.ThrowsException<System.ArgumentException>(() => new LearningEvent(_learningEventArgs));
    }

    [TestMethod]
    public void StartDateMustBeBeforeEndDate()
    {
        var newStartTime = DateTime.Now.AddDays(3);
        var newEndTime = DateTime.Now;

        _learningEventArgs.StartTime = newStartTime;
        _learningEventArgs.EndTime = newEndTime;

        Assert.ThrowsException<System.ArgumentException>(() => new LearningEvent(_learningEventArgs));
    }

    [TestMethod]
    public void CanUpdateStartAndEndDates()
    {
        var newStartTime = DateTime.Now.AddDays(10);
        var newEndTime = DateTime.Now.AddDays(10+2);

        var learningEvent = new LearningEvent(_learningEventArgs);

        learningEvent.UpdateStartAndEndTimes(newStartTime, newEndTime);
        
        Assert.AreEqual(newStartTime, learningEvent.StartTime);
        Assert.AreEqual(newEndTime, learningEvent.EndTime);
    }

    [TestMethod]
    public void TotalHoursMustBeGreaterThan0()
    {
        _learningEventArgs.TotalHours = 0;
        
        // Act & Assert
        Assert.ThrowsException<System.ArgumentException>(() => new LearningEvent(_learningEventArgs));
    }

    [TestMethod]
    public void TotalHoursMustBeLessThan17()
    {
        _learningEventArgs.TotalHours = 17;

        // Act & Assert
        Assert.ThrowsException<System.ArgumentException>(() => new LearningEvent(_learningEventArgs));
    }

    [TestMethod]
    public void TitleCannotBeNullOrEmpty()
    {
        _learningEventArgs.Title = null;
        Assert.ThrowsException<System.ArgumentException>(() => new LearningEvent(_learningEventArgs));

        _learningEventArgs.Title = string.Empty;
        Assert.ThrowsException<System.ArgumentException>(() => new LearningEvent(_learningEventArgs));
    }

    [TestMethod]
    public void DescriptionCannotBeNullOrEmpty()
    {
        _learningEventArgs.Description = null;
        Assert.ThrowsException<System.ArgumentException>(() => new LearningEvent(_learningEventArgs));

        _learningEventArgs.Description = string.Empty;
        Assert.ThrowsException<System.ArgumentException>(() => new LearningEvent(_learningEventArgs));
    }

    [TestMethod]
    public void CanUpdateTitle()
    {
        var learningEvent = new LearningEvent(_learningEventArgs);

        var newTitle = "New Title";
        learningEvent.UpdateTitle(newTitle);

        Assert.AreEqual(newTitle, learningEvent.Title);
    }

    [TestMethod]
    public void CannotUpdateTitleWithNullOrEmpty()
    {
        var learningEvent = new LearningEvent(_learningEventArgs);

        Assert.ThrowsException<System.ArgumentException>(() => learningEvent.UpdateTitle(null));
        Assert.ThrowsException<System.ArgumentException>(() => learningEvent.UpdateTitle(string.Empty));
    }

    [TestMethod]
    public void CannotUpdateDescriptionWithNullOrEmpty()
    {
        var learningEvent = new LearningEvent(_learningEventArgs);

        Assert.ThrowsException<System.ArgumentException>(() => learningEvent.UpdateDescription(null));
        Assert.ThrowsException<System.ArgumentException>(() => learningEvent.UpdateDescription(string.Empty));
    }

    [TestMethod]
    public void CanUpdateDescription()
    {
        var learningEvent = new LearningEvent(_learningEventArgs);

        var newDescription = "New Description";
        learningEvent.UpdateDescription(newDescription);

        Assert.AreEqual(newDescription, learningEvent.Description);
    }
}
