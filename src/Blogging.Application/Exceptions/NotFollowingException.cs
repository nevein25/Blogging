namespace Blogging.Application.Exceptions;
internal class NotFollowingException : Exception
{
    public NotFollowingException() : base("You cannot unfollow a user you are not already following.") { }

}
