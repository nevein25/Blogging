namespace Blogging.Application.Exceptions;
public class UsernameTakenException : Exception
{
    public UsernameTakenException() : base("Username is taken") { }
}
