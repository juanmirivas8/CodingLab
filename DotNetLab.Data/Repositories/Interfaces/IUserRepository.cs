using DotNetLab.Model.Framework;

namespace DotNetLab.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetUsersByNameAsync(string name);
    Task<User?> GetUserByNameAsync(string name);
    Task<IEnumerable<User>> GetUsersCreatedAfterAsync(DateTime date);
    Task<bool> UserExistsByNameAsync(string name);
}
