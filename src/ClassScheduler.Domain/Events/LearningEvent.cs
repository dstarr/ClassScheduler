namespace ClassScheduler.Domain.Events;

public class LearningEvent
{
    public LearningEvent(LearningEventArgs learningEventArgs)
    {
        VerifyConstructorArguments(learningEventArgs);

        Title = learningEventArgs.Title;
        Description = learningEventArgs.Description;
        StartTime = learningEventArgs.StartTime;
        EndTime = learningEventArgs.EndTime;
        TotalHours = learningEventArgs.TotalHours;
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public DateTime StartTime { get; private set; }

    public DateTime EndTime { get; private set; }

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

    public void UpdateStartTime(DateTime startDate)
    {
        if (startDate < DateTime.Now)
        {
            throw new ArgumentException("Start date cannot be in the past", nameof(startDate));
        }

        StartTime = startDate;
    }

    public void UpdateEndTime(DateTime endDate)
    {
        if (endDate < DateTime.Now)
        {
            throw new ArgumentException("End date cannot be in the past", nameof(endDate));
        }

        EndTime = endDate;
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

    private static void VerifyConstructorArguments(LearningEventArgs args)
    {
        if (string.IsNullOrWhiteSpace(args.Title))
        {
            throw new ArgumentException("Title cannot be empty", nameof(args.Title));
        }

        if (string.IsNullOrWhiteSpace(args.Description))
        {
            throw new ArgumentException("Description cannot be empty", nameof(args.Description));
        }

        if (args.TotalHours < 0)
        {
            throw new ArgumentException("Total hours cannot be less than 0", nameof(args.TotalHours));
        }

        if (args.TotalHours > 16)
        {
            throw new ArgumentException("Total hours cannot be more than 16", nameof(args.TotalHours));
        }

        if (args.StartTime < DateTime.Now)
        {
            throw new ArgumentException("Start date cannot be in the past", nameof(args.StartTime));
        }

        if (args.EndTime < DateTime.Now)
        {
            throw new ArgumentException("End date cannot be in the past", nameof(args.EndTime));
        }
    }
    
}