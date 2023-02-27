using ClassScheduler.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Test.Integration;

[TestClass]
public class StudentDbTest: DbTestBase
{
    private const string PartitionKey = "/id";
    
    private static DbContextOptions<StudentDbContext> _options = null!;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _options = new DbContextOptionsBuilder<StudentDbContext>()
            .UseCosmos(ConnectionString, DatabaseName)
            .Options;
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
    }

    [TestMethod]
    public async Task CanAddAndRemoveStudent()
    {
        var student = CreateStudentDto();

        await using var context = new StudentDbContext(_options);
        await context.Database.EnsureCreatedAsync();

        context.Students.Add(student);
        await context.SaveChangesAsync();

        var allStudents = await context.Students?.ToListAsync()!;
        Assert.AreEqual(1, allStudents.Count);

        context.Students.Remove(student);
        await context.SaveChangesAsync();

        allStudents = await context.Students?.ToListAsync()!;
        Assert.AreEqual(0, allStudents.Count);
    }

    private static StudentDto CreateStudentDto()
    {
        var student = new StudentDto
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "test@test.com",
            PartitionKey = PartitionKey
        };
        return student;
    }
}