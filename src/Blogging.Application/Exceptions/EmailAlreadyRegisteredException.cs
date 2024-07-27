namespace Blogging.Application.Exceptions;
public class EmailAlreadyRegisteredException : Exception
{
    public EmailAlreadyRegisteredException() : base("This Email is already registered") { }
}
