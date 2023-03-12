using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Dto;
using ClassScheduler.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Test.Integration.Contexts;

[TestClass]
public class StudentDbContextTest : DbTestBase
{
    private static DbContextOptions<StudentDbContext> _options = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _options = new DbContextOptionsBuilder<StudentDbContext>()
            .UseCosmos(ConnectionString, DatabaseName)
            .Options;
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        using var client = new CosmosClient(ConnectionString);

        var container = client.GetContainer(DatabaseName, "Students");
        await container.DeleteContainerAsync();

    }

    [TestMethod]
    public async Task CanRetrieveAllStudents()
    {
        await using var context = new StudentDbContext(_options);
        await context.Database.EnsureCreatedAsync();

        await context.Students.AddAsync(CreateStudentDto());
        await context.Students.AddAsync(CreateStudentDto());
        await context.Students.AddAsync(CreateStudentDto());
        await context.SaveChangesAsync();

        Assert.AreEqual(3, context.Students.Count());

        await foreach (var student in context.Students)
        {
            context.Students.Remove(student);
        }
        
        await context.SaveChangesAsync();
        
        Assert.AreEqual(0, context.Students.Count());
    }

    [TestMethod]
    public async Task CanUpdateStudent()
    {
        var student = CreateStudentDto();

        await using var context = new StudentDbContext(_options);
        await context.Database.EnsureCreatedAsync();

        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();

        var result1 = await context.Students.FirstOrDefaultAsync(s => s.Id == student.Id);
        Assert.IsNotNull(result1);

        result1.FirstName = "Jane";

        context.Students?.Update(result1);
        await context.SaveChangesAsync();

        var result2 = context.Students?.FirstOrDefaultAsync(s => s.Id == student.Id).Result;
        Assert.IsNotNull(result2);

        Assert.AreEqual("Jane", student.FirstName);

        context.Students?.Remove(result2);
        await context.SaveChangesAsync();

        await context.DisposeAsync();
    }

    [TestMethod]
    public async Task CanAddStudentAsync()
    {
        var student = CreateStudentDto();

        await using var context = new StudentDbContext(_options);
        await context.Database.EnsureCreatedAsync();

        var startNumStudents = await context.Students.CountAsync();
        context.Students.Add(student);
        await context.SaveChangesAsync();

        var endNumStudents = await context.Students.CountAsync();

        Assert.AreEqual(startNumStudents+ 1, endNumStudents);
    }

    private static StudentDto CreateStudentDto()
    {
        return new StudentDto
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "test@test.com"
        };
    }
}