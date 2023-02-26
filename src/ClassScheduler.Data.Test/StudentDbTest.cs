using System.Diagnostics;
using ClassScheduler.Data.Dto;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace ClassScheduler.Data.Test;

[TestClass]
public class StudentDbTest
{
    private const string AccountKey = "cuDdgi38yi4abLAE8d9K1Wgi7rfNsGtOwSSjdaCxQDWKZTTLqKrwsjZnESYSiLq5txHf5yGIpIScACDbMc9V6Q==";
    private const string AppName = "ClassSchedulerApp";
    private const string ContainerName = "StudentDto";
    private const string DatabaseName = "ClassScheduler";
    private const string EndpointUri = "https://class-scheduler-db.documents.azure.com:443/";
    private const string PartitionKey = "/id";
    private const string ConnectionString = "AccountEndpoint=https://class-scheduler-db.documents.azure.com:443/;AccountKey=cuDdgi38yi4abLAE8d9K1Wgi7rfNsGtOwSSjdaCxQDWKZTTLqKrwsjZnESYSiLq5txHf5yGIpIScACDbMc9V6Q==";

    DbContextOptions<StudentDbContext> _options = null!;
    
    [TestInitialize]
    public async Task TestInitialize()
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