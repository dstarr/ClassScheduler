namespace ClassScheduler.Domain.Test.UserTests;

[TestClass]
public class UserBaseTest
{
    private const string TestEmail = "test@test.com";
    private const string TestFirstName = "TestFirstName";
    private const string TestLastName = "TestLastName";


    private TestUser _user = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _user = new TestUser(TestFirstName, TestLastName, TestEmail);
    }

    [TestMethod]
    public void ConstructorSetsProperties()
    {
        Assert.AreEqual(TestFirstName, _user.FirstName);
        Assert.AreEqual(TestLastName, _user.LastName);
        Assert.AreEqual(TestEmail, _user.Email);
    }

    [TestMethod]
    public void ConstructWithId()
    {
        _user = new TestUser(TestFirstName, TestLastName, TestEmail);
    }

    [TestMethod]
    public void UpdateFirstName()
    {
        Assert.AreEqual(TestFirstName, _user.FirstName);

        _user.UpdateFirstName("John");
        Assert.AreEqual("John", _user.FirstName);
    }

    [TestMethod]
    public void UpdateLastName()
    {
        Assert.AreEqual(TestLastName, _user.LastName);

        _user.UpdateLastName("Smith");

        Assert.AreEqual("Smith", _user.LastName);
    }

    [TestMethod]
    public void UpdateEmail()
    {
        Assert.AreEqual(TestEmail, _user.Email);

        _user.UpdateEmail("david@starr.com");

        Assert.AreEqual("david@starr.com", _user.Email);
    }

    [TestMethod]
    public void RejectMalformedEmailOnUpdate()
    {
        const string badEmail = "david@starr";

        Assert.ThrowsException<ArgumentException>(() => _user.UpdateEmail(badEmail));
    }

    [TestMethod]
    public void RejectMalformedEmailOnConstruction()
    {
        const string badEmail = "david@starr";

        Assert.ThrowsException<ArgumentException>(() => new TestUser(TestFirstName, TestLastName, badEmail));
    }

    [TestMethod]
    public void FirstNameCannotBeEmpty()
    {
        Assert.ThrowsException<ArgumentException>(() => new TestUser("", TestLastName, TestEmail));
    }

    [TestMethod]
    public void LastNameCannotBeEmpty()
    {
        Assert.ThrowsException<ArgumentException>(() => new TestUser(TestFirstName, "", TestEmail));
    }
}