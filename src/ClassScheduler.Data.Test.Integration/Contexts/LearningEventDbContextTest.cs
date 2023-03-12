using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Dto;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Test.Integration.Contexts;

[TestClass]
public class LearningEventDbContextTest : DbTestBase
{
    private static DbContextOptions<LearningEventDbContext> _options = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _options = new DbContextOptionsBuilder<LearningEventDbContext>()
            .UseCosmos(ConnectionString, DatabaseName)
            .Options;
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        var client = new CosmosClient(ConnectionString);

        var container = client.GetContainer(DatabaseName, "LearningEvents");
        await container.DeleteContainerAsync();
    }

    [TestMethod]
    public async Task CanAddAndRemoveLearningEvent()
    {
        var learningEventDto = CreateNewLearningEventDto();

        await using var context = new LearningEventDbContext(_options);
        await context.Database.EnsureCreatedAsync();

        context.LearningEvents.Add(learningEventDto);
        await context.SaveChangesAsync();

        Assert.AreEqual(1, context.LearningEvents.Count());

        context.LearningEvents.Remove(learningEventDto);
        await context.SaveChangesAsync();

        Assert.AreEqual(0, context.LearningEvents.Count());
    }

    [TestMethod]
    public async Task CanUpdateLearningEventAsync()
    {
        var learningEventDto = CreateNewLearningEventDto();

        await using var context = new LearningEventDbContext(_options);
        await context.Database.EnsureCreatedAsync();

        context.LearningEvents.Add(learningEventDto);
        await context.SaveChangesAsync();

        Assert.AreEqual(1, context.LearningEvents.Count());

        learningEventDto.Description = "New Description";
        context.LearningEvents.Update(learningEventDto);
        await context.SaveChangesAsync();

        Assert.AreEqual(1, context.LearningEvents.Count());
        Assert.AreEqual("New Description", context.LearningEvents.First().Description);
    }

    [TestMethod]
    public async Task CanAddAndRemoveLearningEventAsync()
    {
        var learningEventDto = CreateNewLearningEventDto();

        await using var context = new LearningEventDbContext(_options);
        await context.Database.EnsureCreatedAsync();

        context.LearningEvents.Add(learningEventDto);
        await context.SaveChangesAsync();

        Assert.AreEqual(1, context.LearningEvents.Count());

        context.LearningEvents.Remove(learningEventDto);
        await context.SaveChangesAsync();

        Assert.AreEqual(0, context.LearningEvents.Count());
    }

    private LearningEventDto CreateNewLearningEventDto()
    {
        var studentIds = new List<string>
        {
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        };

        return new LearningEventDto
        {
            Id = Guid.NewGuid(),
            StudentIds = studentIds,
            Description = "Description",
            EndTime = DateTime.Now.AddDays(5),
            StartTime = DateTime.Now.AddDays(3),
            StudentCapacity = 40,
            Title = "Title",
            TotalHours = 12
        };
    }
}