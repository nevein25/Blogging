namespace Blogging.Domain.Repositories;
public interface IUsersRepository
{
    Task<bool> IsUserExistById(int id);
}
