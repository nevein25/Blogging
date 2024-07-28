namespace Blogging.Application.Exceptions;
public class AlreadyFollowingException : Exception
{
    public AlreadyFollowingException() : base("You are already following this user.") { }
}
