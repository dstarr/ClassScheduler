using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Repositories;
using ClassScheduler.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClassScheduler.Data.Test.Integration.Repositories;

[TestClass]
public class StudentRepositoryTest // : DbTestBase
{
    private StudentRepository _studentRepository = null!;


    internal static string ConnectionString = null!;
    internal static string DatabaseName = null!;
    internal DbContextOptions<StudentDbContext> CosmosOptions = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration configuration = configurationBuilder.Build();

        ConnectionString = configuration.GetSection("ConnectionString").Value ?? throw new InvalidOperationException();
        DatabaseName = configuration.GetSection("DatabaseName").Value ?? throw new InvalidOperationException();

        CosmosOptions = new DbContextOptionsBuilder<StudentDbContext>()
            .UseCosmos(ConnectionString, DatabaseName)
            .Options;
 
        var dbContext = new StudentDbContext(CosmosOptions);
        
        _studentRepository = new StudentRepository(dbContext);
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        var client = new CosmosClient(ConnectionString);

        var container = client.GetContainer(DatabaseName, "StudentDbContext");
        await container.DeleteContainerAsync();
    }

    [TestMethod]
    public void CanGetAllAsync()
    {
        var students = _studentRepository.GetAll();

        Assert.IsNotNull(students);
    }
    
    [TestMethod]
    public async Task AddAsync_Student_AddsStudent()
    {
        var student = CreateStudent();

        var students = _studentRepository.GetAll();
        var initCountOfStudents = students.Count();

        await _studentRepository.AddAsync(student);

        students = _studentRepository.GetAll();

        Assert.AreEqual(initCountOfStudents + 1, students.Count());
    }

    [TestMethod]
    public async Task GetByIdAsync_Student_ReturnsStudent()
    {
        var student = CreateStudent();

        await _studentRepository.AddAsync(student);
        
        var studentFromDb = await _studentRepository.GetByIdAsync(student.Id);

        Assert.IsNotNull(studentFromDb);
    }

    [TestMethod]
    public async Task Update_Student_UpdatesStudent()
    {
        var student = CreateStudent();

        await _studentRepository.AddAsync(student);

        student.UpdateFirstName("Updated First Name");

        await _studentRepository.UpdateAsync(student);

        var studentFromDb = await _studentRepository.GetByIdAsync(student.Id);

        Assert.AreEqual(student.FirstName, studentFromDb.FirstName);
    }

    [TestMethod]
    public async Task Remove_Student_RemovesStudent()
    {
        var student = CreateStudent();

        await _studentRepository.AddAsync(student);

        await _studentRepository.RemoveAsync(student);

        var studentFromDb = await _studentRepository.GetByIdAsync(student.Id);

        Assert.IsNull(studentFromDb);
    }

    private Student CreateStudent()
    {
        return new Student(Guid.NewGuid(), "FirstName", "LastName", "test@test.com");
    }
    
    
}