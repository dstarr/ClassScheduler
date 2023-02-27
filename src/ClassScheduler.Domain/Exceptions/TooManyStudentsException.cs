namespace ClassScheduler.Domain.Exceptions;

public class TooManyStudentsException : Exception
{
    public override string Message {
        get => "Tried to add more students than class capacity.";
    }
}