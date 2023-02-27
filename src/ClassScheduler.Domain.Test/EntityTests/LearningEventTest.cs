using System.Runtime.CompilerServices;
using ClassScheduler.Domain.Entities;
using ClassScheduler.Domain.Exceptions;

namespace ClassScheduler.Domain.Test.EntityTests;

[TestClass]
public class LearningEventTest
{
    [TestMethod]
    public void CannotAddMoreStudentsWhenAtCapacity()
    {
        var learningEvent = CreateNewLearningEvent();

        learningEvent.UpdateStudentCapacity(2);
        learningEvent.AddStudent(CreateStudent());
        learningEvent.AddStudent(CreateStudent());

        Assert.ThrowsException<TooManyStudentsException>(() => learningEvent.AddStudent(CreateStudent()));
    }

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
            StudentCapacity = 10
        };

        var learningEvent = new LearningEvent(args);
    }

    [TestMethod]
    public void CanUpdateTotalHours()
    {
        var learningEvent = CreateNewLearningEvent();

        learningEvent.UpdateTotalHours(20);
        
        Assert.AreEqual(20, learningEvent.TotalHours);
    }

    [TestMethod]
    public void CannotUpdateTotalHoursBelow1()
    {
        var learningEvent = CreateNewLearningEvent();
        
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => learningEvent.UpdateTotalHours(0));
    }

    [TestMethod]
    public void CanUpdateStudentCapacity()
    {
        var learningEvent = CreateNewLearningEvent();
        
        learningEvent.UpdateStudentCapacity(20);
        
        Assert.AreEqual(20, learningEvent.StudentCapacity);
    }

    [TestMethod]
    public void CannotInitStudentCapacityBelow1()
    {
        var learningEventArgs = CreateNewLearningEventArgs();
        
        learningEventArgs.StudentCapacity = 0;

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new LearningEvent(learningEventArgs));
    }

    [TestMethod]
    public void CannotUpdateStudentCapacityBelow1()
    {
        var learningEvent = CreateNewLearningEvent();

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => learningEvent.UpdateStudentCapacity(0));
    }

    [TestMethod]
    public void CanConstructWithId()
    {
        var learningEventArgs = CreateNewLearningEventArgs();

        var learningEvent = new LearningEvent(learningEventArgs);
    }

    [TestMethod]
    public void CannotConstructWithNullLearningEventArgs()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new LearningEvent(null!));
    }

    [TestMethod]
    public void StartDateCannotBeInThePast()
    {
        var learningEventArgs = CreateNewLearningEventArgs();
        
        learningEventArgs.StartTime = DateTime.Now.AddDays(-1);

        Assert.ThrowsException<ArgumentException>(() => new LearningEvent(learningEventArgs));
    }

    [TestMethod]
    public void EndDateCannotBeInThePast()
    {
        var learningEventArgs = CreateNewLearningEventArgs();
        
        learningEventArgs.EndTime = DateTime.Now.AddDays(-1);

        Assert.ThrowsException<ArgumentException>(() => new LearningEvent(learningEventArgs));
    }

    [TestMethod]
    public void StartDateMustBeBeforeEndDate()
    {
        var learningEventArgs = CreateNewLearningEventArgs();

        var newStartTime = DateTime.Now.AddDays(3);
        var newEndTime = DateTime.Now;

        learningEventArgs.StartTime = newStartTime;
        learningEventArgs.EndTime = newEndTime;

        Assert.ThrowsException<ArgumentException>(() => new LearningEvent(learningEventArgs));
    }

    [TestMethod]
    public void CanUpdateStartAndEndDates()
    {
        var learningEvent = CreateNewLearningEvent();

        var newStartTime = DateTime.Now.AddDays(10);
        var newEndTime = DateTime.Now.AddDays(10 + 2);
        
        learningEvent.UpdateStartAndEndTimes(newStartTime, newEndTime);

        Assert.AreEqual(newStartTime, learningEvent.StartTime);
        Assert.AreEqual(newEndTime, learningEvent.EndTime);
    }

    [TestMethod]
    public void TotalHoursMustBeGreaterThan0()
    {
        var learningEventArgs = CreateNewLearningEventArgs();

        learningEventArgs.TotalHours = 0;

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => new LearningEvent(learningEventArgs));
    }

    [TestMethod]
    public void TitleCannotBeNullOrEmpty()
    {
        var learningEventArgs = CreateNewLearningEventArgs();

        learningEventArgs.Title = null;
        Assert.ThrowsException<ArgumentException>(() => new LearningEvent(learningEventArgs));

        learningEventArgs.Title = string.Empty;
        Assert.ThrowsException<ArgumentException>(() => new LearningEvent(learningEventArgs));
    }

    [TestMethod]
    public void DescriptionCannotBeNullOrEmpty()
    {
        var learningEventArgs = CreateNewLearningEventArgs();

        learningEventArgs.Description = null;
        Assert.ThrowsException<ArgumentException>(() => new LearningEvent(learningEventArgs));

        learningEventArgs.Description = string.Empty;
        Assert.ThrowsException<ArgumentException>(() => new LearningEvent(learningEventArgs));
    }

    [TestMethod]
    public void CanUpdateTitle()
    {
        var learningEvent = CreateNewLearningEvent();

        var newTitle = "New Title";
        learningEvent.UpdateTitle(newTitle);

        Assert.AreEqual(newTitle, learningEvent.Title);
    }

    [TestMethod]
    public void CannotUpdateTitleWithNullOrEmpty()
    {
        var learningEvent = CreateNewLearningEvent();
        
        Assert.ThrowsException<ArgumentException>(() => learningEvent.UpdateTitle(null));
        Assert.ThrowsException<ArgumentException>(() => learningEvent.UpdateTitle(string.Empty));
    }

    [TestMethod]
    public void CannotUpdateDescriptionWithNullOrEmpty()
    {
        var learningEvent = CreateNewLearningEvent();

        Assert.ThrowsException<ArgumentException>(() => learningEvent.UpdateDescription(null));
        Assert.ThrowsException<ArgumentException>(() => learningEvent.UpdateDescription(string.Empty));
    }
    
    [TestMethod]
    public void CanUpdateDescription()
    {
        var learningEvent = CreateNewLearningEvent();

        var newDescription = "New Description";
        learningEvent.UpdateDescription(newDescription);

        Assert.AreEqual(newDescription, learningEvent.Description);
    }

    [TestMethod]
    public void CanAddStudent()
    {
        var learningEvent = CreateNewLearningEvent();
        var student = CreateStudent();
        
        Assert.AreEqual(0, learningEvent.Students.Count);

        learningEvent.AddStudent(student);

        Assert.AreEqual(1, learningEvent.Students.Count);
    }


    [TestMethod]
    public void CanRemoveStudent()
    {
        var learningEvent = CreateNewLearningEvent();
        var student = CreateStudent();

        Assert.AreEqual(0, learningEvent.Students.Count);

        learningEvent.AddStudent(student);

        Assert.AreEqual(1, learningEvent.Students.Count);

        learningEvent.RemoveStudent(student);

        Assert.AreEqual(0, learningEvent.Students.Count);

    }

    private LearningEvent CreateNewLearningEvent()
    {
        return new LearningEvent(CreateNewLearningEventArgs());
    }

    private LearningEventArgs CreateNewLearningEventArgs()
    {
        return new LearningEventArgs()
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddDays(5),
            TotalHours = 12,
            Description = "Description",
            EndTime = DateTime.Now.AddDays(6),
            Title = "Title",
            StudentCapacity = 10
        };
    }
    private static Student CreateStudent()
    {
        return new Student(Guid.NewGuid(), "Jane", "Doe", "jane@test.com");
    }

}
