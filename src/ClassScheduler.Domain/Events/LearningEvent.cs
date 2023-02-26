namespace ClassScheduler.Domain.Events;

public class LearningEvent
{
    public LearningEvent(Guid id, string title, string description, DateTime startDate, DateTime endDate, int totalHours)
    {
        VerifyConstructorArguments(id, title, description, startDate, endDate, totalHours);

        Id = id;
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        TotalHours = totalHours;
    }

    public LearningEvent(string title, string description, DateTime startDate, DateTime endDate, int totalHours)
    {
        VerifyConstructorArguments(title, description, startDate, endDate, totalHours);

        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        TotalHours = totalHours;
    }


    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public int TotalHours { get; private set; }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty", nameof(title));
        }

        Title = title;
    }

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be empty", nameof(description));
        }

        Description = description;
    }

    public void UpdateStartDate(DateTime startDate)
    {
        if (startDate < DateTime.Now)
        {
            throw new ArgumentException("Start date cannot be in the past", nameof(startDate));
        }

        StartDate = startDate;
    }

    public void UpdateEndDate(DateTime endDate)
    {
        if (endDate < DateTime.Now)
        {
            throw new ArgumentException("End date cannot be in the past", nameof(endDate));
        }

        EndDate = endDate;
    }

    public void UpdateTotalHours(int totalHours)
    {
        if (totalHours < 0)
        {
            throw new ArgumentException("Total hours cannot be less than 0", nameof(TotalHours));
        }

        if (totalHours > 16)
        {
            throw new ArgumentException("Total hours cannot be more than 16", nameof(TotalHours));
        }

        TotalHours = totalHours;
    }

    private static void VerifyConstructorArguments(string title, string description, DateTime startDate, DateTime endDate, int totalHours)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be empty", nameof(description));
        }

        if (totalHours < 0)
        {
            throw new ArgumentException("Total hours cannot be less than 0", nameof(TotalHours));
        }

        if (totalHours > 16)
        {
            throw new ArgumentException("Total hours cannot be more than 16", nameof(TotalHours));
        }

        if (startDate < DateTime.Now)
        {
            throw new ArgumentException("Start date cannot be in the past", nameof(startDate));
        }

        if (endDate < DateTime.Now)
        {
            throw new ArgumentException("End date cannot be in the past", nameof(endDate));
        }
    }
    
    private static void VerifyConstructorArguments(Guid id, string title, string description, DateTime startDate,
        DateTime endDate, int totalHours)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        VerifyConstructorArguments(title, description, startDate, endDate, totalHours);
    }
}