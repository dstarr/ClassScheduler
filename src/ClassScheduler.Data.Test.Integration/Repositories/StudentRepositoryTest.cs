﻿using System.Security.Cryptography.X509Certificates;
using ClassScheduler.Data.DbContexts;
using ClassScheduler.Data.Repositories;
using ClassScheduler.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClassScheduler.Data.Test.Integration.Repositories;

[TestClass]
public class StudentRepositoryTest : DbTestBase
{
    private StudentDbContext _studentDbContext = null!;

    private StudentRepository _studentRepository = null!;

    [TestInitialize]
    public async Task TestInitialize()
    {
         _studentDbContext = new StudentDbContext(CosmosOptions);
         await _studentDbContext.Database.EnsureCreatedAsync();

         _studentRepository = new StudentRepository(_studentDbContext);
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        // delete the container
        var cosmosClient = _studentDbContext.Database.GetCosmosClient();
        var container = cosmosClient.GetContainer(DatabaseName, "Students");
        await container.DeleteContainerAsync();

        await _studentDbContext.DisposeAsync();
        await _studentRepository.DisposeAsync();
    }

    [TestMethod]
    public async Task CanGetStudentsAllAsync()
    {
        var students = (await _studentRepository.GetAllAsync()).ToList();

        Assert.IsNotNull(students);
    }
    
    [TestMethod]
    public async Task CanAddStudentAsync()
    {
        var student = CreateStudent();

        var initCountOfStudents = _studentDbContext.Students.Count();

        await _studentRepository.AddAsync(student);

        var finalNumStudents = _studentDbContext.Students.Count();
        
        Assert.AreEqual(initCountOfStudents + 1, finalNumStudents);

    }
        
    [TestMethod]
    public async Task CanGetStudentByIdAsync()
    {
        var student = CreateStudent();

        await _studentRepository.AddAsync(student);
        
        var studentFromDb = await _studentRepository.GetByIdAsync(student.Id);

        Assert.IsNotNull(studentFromDb);
        Assert.AreEqual(student.Id, studentFromDb.Id);
    }

    [TestMethod]
    public async Task CanUpdateStudentAsync()
    {
        
        var student = CreateStudent();
        const string updatedFirstName = "Updated First Name";

        await _studentRepository.AddAsync(student);

        student.UpdateFirstName(updatedFirstName);

        _studentRepository.Update(student);

        var studentFromDb = await _studentRepository.GetByIdAsync(student.Id);

        Assert.AreEqual(updatedFirstName, studentFromDb.FirstName);
    }

    [TestMethod]
    public async Task CanRemoveStudentAsync()
    {
        var student = CreateStudent();

        await _studentRepository.AddAsync(student);

        await _studentRepository.RemoveAsync(student);

        var studentFromDb = await _studentRepository.GetByIdAsync(student.Id);

        Assert.IsNull(studentFromDb);
    }

    private static Student CreateStudent()
    {
        return new Student(Guid.NewGuid(), "FirstName", "LastName", "test@test.com");
    }
    
    
}