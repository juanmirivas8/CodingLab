using DotNetLab.Model.Framework;
using DotNetLab.Services.Interfaces;
using DotNetLab.Data.Repositories.Interfaces;

namespace DotNetLab.Services.Implementation;

public class UserService(IUnitOfWork unitOfWork): IUserService
{

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await unitOfWork.Users.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await unitOfWork.Users.GetByIdAsync(id);
    }

    public async Task<User> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new User
        {
            // Id will be generated automatically by the database (DEFAULT NEWID())
            Name = createUserDto.Name,
            CreatedBy = createUserDto.CreatedBy,
            LastModifiedBy = createUserDto.LastModifiedBy,
            CreatedDateTime = DateTime.UtcNow,
            LastModifiedDateTime = DateTime.UtcNow
        };
        
        await unitOfWork.Users.AddAsync(user);
        await unitOfWork.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(Guid id, User user)
    {
        var existingUser = await unitOfWork.Users.GetByIdAsync(id);
        if (existingUser == null)
            return null;

        existingUser.Name = user.Name;
        unitOfWork.Users.Update(existingUser);
        await unitOfWork.SaveChangesAsync();
        
        return existingUser;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
            return false;

        unitOfWork.Users.Remove(user);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}
