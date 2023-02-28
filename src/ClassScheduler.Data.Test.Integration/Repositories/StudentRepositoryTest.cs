    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Repositories;
using ClassScheduler.Data.Test.Integration;
using ClassScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassScheduler.Data.Test.Integration.Repositories;

[TestClass]
public class StudentRepositoryTest : DbTestBase
{
    private StudentRepository _studentRepository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var options = new DbContextOptionsBuilder<StudentDbContext>()
            .UseCosmos(ConnectionString, DatabaseName)
            .Options;

        var dbContext = new StudentDbContext(options);
        _studentRepository = new StudentRepository(dbContext);
    }

    [TestMethod]
    public async Task AddAsync_Student_AddsStudent()
    {
        var student = CreateStudent();

        await _studentRepository.AddAsync(student);
        // await _studentRepository.SaveChangesAsync();

        var students = await _studentRepository.GetAllAsync();

        Assert.AreEqual(1, students.Count());

        foreach (var studentFromDb in students)
        {
            _studentRepository.Remove(studentFromDb);
        }
        await _studentRepository.SaveChangesAsync();
    }

    [TestMethod]
    public async Task GetByIdAsync_Student_ReturnsStudent()
    {
        var student = CreateStudent();

        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveChangesAsync();

        var studentFromDb = await _studentRepository.GetByIdAsync(student.Id);

        Assert.IsNotNull(studentFromDb);
    }

    [TestMethod]
    public async Task Update_Student_UpdatesStudent()
    {
        var student = CreateStudent();

        await _studentRepository.AddAsync(student);

        student.UpdateFirstName("Updated First Name");

        _studentRepository.Update(student);

        var studentFromDb = await _studentRepository.GetByIdAsync(student.Id);

        Assert.AreEqual(student.FirstName, studentFromDb.FirstName);
    }

    [TestMethod]
    public async Task Remove_Student_RemovesStudent()
    {
        var student = CreateStudent();

        await _studentRepository.AddAsync(student);

        _studentRepository.Remove(student);

        await _studentRepository.SaveChangesAsync();

        var studentFromDb = await _studentRepository.GetByIdAsync(student.Id);

        Assert.IsNull(studentFromDb);
    }

    private Student CreateStudent()
    {
        return new Student(Guid.NewGuid(), "FirstName", "LastName", "test@test.com");
    }
    
    
}