namespace ClassScheduler.Domain.Events;

public class LearningEvent
{
    public LearningEvent(LearningEventArgs learningEventArgs)
    {
        VerifyConstructorArguments(learningEventArgs);

        if (learningEventArgs.Id.HasValue)
        {
            Id = learningEventArgs.Id.Value;
        }

        Description = learningEventArgs.Description;
        EndTime = learningEventArgs.EndTime;
        StartTime = learningEventArgs.StartTime;
        StudentCapacity = learningEventArgs.StudentCapacity;
        Title = learningEventArgs.Title;
        TotalHours = learningEventArgs.TotalHours;
    }

    public int StudentCapacity { get; private set; }

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

    public void UpdateStartAndEndTimes(DateTime startTime, DateTime endTime)
    {
        ValidateStartAndEndTimes(startTime, endTime);

        StartTime = startTime;
        EndTime = endTime;
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

    public void UpdateStudentCapacity(int numberOfSeats)
    {
        if (numberOfSeats <= 0)
            throw new ArgumentOutOfRangeException("Number of seats cannot be less than 0", nameof(numberOfSeats));

        StudentCapacity = numberOfSeats;
    }

    private static void VerifyConstructorArguments(LearningEventArgs args)
    {
        if (args == null)
        {
            throw new ArgumentNullException("Args cannot be null");
        }
        
        ValidateStartAndEndTimes(args.StartTime, args.EndTime);


        if (string.IsNullOrWhiteSpace(args.Title))
        {
            throw new ArgumentException("Title cannot be empty", nameof(args.Title));
        }

        if (string.IsNullOrWhiteSpace(args.Description))
        {
            throw new ArgumentException("Description cannot be empty", nameof(args.Description));
        }

        if (args.TotalHours <= 0)
        {
            throw new ArgumentException("Total hours cannot be less than 0", nameof(args.TotalHours));
        }

        if (args.TotalHours > 16)
        {
            throw new ArgumentException("Total hours cannot be more than 16", nameof(args.TotalHours));
        }

        if (args.StudentCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(args.StudentCapacity), "Student capacity cannot be less than 0");
        }
    }

    private static void ValidateStartAndEndTimes(DateTime startTime, DateTime endTime)
    {
        if (startTime < DateTime.Now)
        {
            throw new ArgumentException("Start time cannot be in the past", nameof(startTime));
        }

        if (endTime < DateTime.Now)
        {
            throw new ArgumentException("End time cannot be in the past", nameof(endTime));
        }

        if (endTime < startTime)
        {
            throw new ArgumentException("End time cannot be before start date", nameof(endTime));
        }
    }
}