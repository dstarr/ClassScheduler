using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Repositories;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Test.Integration.Repositories;

[TestClass()]
public class LearningEventRepositoryTest : DbTestBase
{
    private LearningEventRepository _learningEventRepository = null!;
    private AppDbContext _dbContext = null!;

    [TestInitialize]
    public async Task TestInitialize()
    {
        var cosmosOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseCosmos(ConnectionString, DatabaseName)
            .Options;

        _dbContext = new AppDbContext(cosmosOptions);
        await _dbContext.Database.EnsureCreatedAsync();

        _learningEventRepository = new LearningEventRepository(_dbContext);
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        // delete the container
        var cosmosClient = _dbContext.Database.GetCosmosClient();
        var container = cosmosClient.GetContainer(DatabaseName, "LearningEvents");
        await container.DeleteContainerAsync();

        await _dbContext.DisposeAsync();
        await _learningEventRepository.DisposeAsync();
    }

    [TestMethod]
    public async Task CanGetLearningEventByIdAsync()
    {
        // arrange
        var learningEvent = CreateLearningEvent();

        await _learningEventRepository.AddAsync(learningEvent);

        // act
        var learningEventFromDb = await _learningEventRepository.GetByIdAsync(learningEvent.Id);

        // assert
        Assert.IsNotNull(learningEventFromDb);
        Assert.AreEqual(learningEvent.Id, learningEventFromDb.Id);
    }

    [TestMethod]
    public async Task CanGetAllLearningEventsAsync()
    {
        // arrange
        await _learningEventRepository.AddAsync(CreateLearningEvent());
        await _learningEventRepository.AddAsync(CreateLearningEvent());
        await _learningEventRepository.AddAsync(CreateLearningEvent());
        await _learningEventRepository.AddAsync(CreateLearningEvent());

        // act
        var learningEventsFromDb = await _learningEventRepository.GetAllAsync();

        // assert
        Assert.IsNotNull(learningEventsFromDb);
        Assert.IsTrue(learningEventsFromDb.Any());
        Assert.AreEqual(4, learningEventsFromDb.Count());
    }

    [TestMethod]
    public async Task CanDeleteLearningEventAsync()
    {
        // arrange
        var learningEvent = CreateLearningEvent();

        await _learningEventRepository.AddAsync(learningEvent);

        // act
        await _learningEventRepository.DeleteAsync(learningEvent.Id);

        // assert
        var learningEventFromDb = await _learningEventRepository.GetByIdAsync(learningEvent.Id);

        Assert.IsNull(learningEventFromDb);
    }

    [TestMethod]
    public async Task CanUpdateLearningEventAsync()
    {
        // arrange
        var startTime = DateTime.Now.AddDays(2);
        var endTime = DateTime.Now.AddDays(3);
        const string updatedTitle = "Updated Title";
        const string updatedDescription = "Updated Description";
        
        var learningEvent = CreateLearningEvent();

        await _learningEventRepository.AddAsync(learningEvent);

        learningEvent = await _learningEventRepository.GetByIdAsync(learningEvent.Id);

        // act
        learningEvent.UpdateTitle(updatedTitle);
        learningEvent.UpdateDescription(updatedDescription);
        learningEvent.UpdateStartAndEndTimes(startTime, endTime);
        learningEvent.UpdateStudentCapacity(20);
        learningEvent.UpdateTotalHours(20);
        learningEvent.AddStudent(CreateStudent());
        learningEvent.AddStudent(CreateStudent());

        await _learningEventRepository.UpdateAsync(learningEvent);

        // assert
        var learningEventFromDb = await _learningEventRepository.GetByIdAsync(learningEvent.Id);

        Assert.IsNotNull(learningEventFromDb);

        Assert.AreEqual(learningEvent.Id, learningEventFromDb.Id);
        Assert.AreEqual(updatedTitle, learningEventFromDb.Title);
        Assert.AreEqual(updatedDescription, learningEventFromDb.Description);
        Assert.AreEqual(startTime, learningEventFromDb.StartTime);
        Assert.AreEqual(endTime, learningEventFromDb.EndTime);
        Assert.AreEqual(20, learningEventFromDb.StudentCapacity);
        Assert.AreEqual(20, learningEventFromDb.TotalHours);
        Assert.AreEqual(2, learningEventFromDb.Students.Count);
    }

    [TestMethod]
    public async Task CanAddLearningEventAsync()
    {
        // arrange
        var learningEvent = CreateLearningEvent();

        var initCountOfLearningEvents = (await _learningEventRepository.GetAllAsync()).Count;

        // act
        await _learningEventRepository.AddAsync(learningEvent);

        // assert
        var finalNumLearningEvents = (await _learningEventRepository.GetAllAsync()).Count;

        Assert.AreEqual(initCountOfLearningEvents + 1, finalNumLearningEvents);
    }

    private static LearningEvent CreateLearningEvent()
    {
        var learningEventArgs = new LearningEventArgs()
        {
            Title = "Test Learning Event",
            Description = "Test Learning Event Description",
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(2),
            MaxStudents = 10,
            Id = Guid.NewGuid(),
            TotalHours = 12,
            Students = new List<Student>()
        };

        return new LearningEvent(learningEventArgs)
        { };
    }

    private static Student CreateStudent()
    {
        return new Student(Guid.NewGuid(), "First Name", "Last Name", "test@test.com")
        {};
    }
}