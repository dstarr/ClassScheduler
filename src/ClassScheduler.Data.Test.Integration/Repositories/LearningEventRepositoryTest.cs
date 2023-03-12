using System.Runtime.CompilerServices;
using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Repositories;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Test.Integration.Repositories;

[TestClass()]
public class LearningEventRepositoryTest : DbTestBase
{

    private LearningEventDbContext _learningEventDbContext = null!;

    private LearningEventRepository _learningEventRepository = null!;

    [TestInitialize]
    public async Task TestInitialize()
    {
        var cosmosOptions = new DbContextOptionsBuilder<LearningEventDbContext>()
            .UseCosmos(ConnectionString, DatabaseName)
            .Options;

        _learningEventDbContext = new LearningEventDbContext(cosmosOptions);
        await _learningEventDbContext.Database.EnsureCreatedAsync();

        _learningEventRepository = new LearningEventRepository(_learningEventDbContext);
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        // delete the container
        var cosmosClient = _learningEventDbContext.Database.GetCosmosClient();
        var container = cosmosClient.GetContainer(DatabaseName, "LearningEvents");
        await container.DeleteContainerAsync();

        await _learningEventDbContext.DisposeAsync();
        await _learningEventRepository.DisposeAsync();
    }

    [TestMethod]
    public async Task CanAddLearningEventAsync()
    {
        // arrange
        var learningEvent = CreateLearningEvent();

        var initCountOfLearningEvents = _learningEventDbContext.LearningEvents.Count();

        // act
        await _learningEventRepository.AddAsync(learningEvent);
        await _learningEventRepository.SaveChangesAsync();

        // assert
        var finalNumLearningEvents = _learningEventDbContext.LearningEvents.Count();

        Assert.AreEqual(initCountOfLearningEvents + 1, finalNumLearningEvents);
    }

    private LearningEvent CreateLearningEvent()
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
        {};
    }
}