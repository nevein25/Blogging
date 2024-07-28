namespace Blogging.Domain.Exceptions;
public class SelfFollowException : Exception
{
    public SelfFollowException()
    : base("You cannot follow yourself.") { }

    public SelfFollowException(string message)
        : base(message) { }

    public SelfFollowException(string message, Exception innerException)
        : base(message, innerException) { }
}
