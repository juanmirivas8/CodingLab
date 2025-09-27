using DotNetLab.Model.Framework;
using DotNetLab.Services.Interfaces;

namespace DotNetLab.Services.Implementation;

public class UserService : IUserService
{
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }
    public async Task<User?> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<User> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
    public async Task<User?> UpdateUserAsync(int id, User user)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}
