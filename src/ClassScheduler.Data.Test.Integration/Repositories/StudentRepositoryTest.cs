using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Repositories;
using ClassScheduler.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Test.Integration.Repositories;

[TestClass]
public class StudentRepositoryTest : DbTestBase
{
    private StudentRepository _studentRepository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
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
        Assert.AreEqual(0, students.Count());

        await _studentRepository.AddAsync(student);

        students = _studentRepository.GetAll();

        Assert.AreEqual(1, students.Count());

        foreach (var studentFromDb in students)
        {
            await _studentRepository.RemoveAsync(studentFromDb);
        }
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