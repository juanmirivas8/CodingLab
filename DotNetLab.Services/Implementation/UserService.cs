using DotNetLab.Model.Framework;
using DotNetLab.Model.IntegrationEvents;
using DotNetLab.Services.Interfaces;
using DotNetLab.Data.Repositories.Interfaces;

namespace DotNetLab.Services.Implementation;

public class UserService(IUnitOfWork unitOfWork, IMessageBus messageBus): IUserService
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
        
        // Publish integration event
        await messageBus.PublishAsync(
            exchange: "user.events",
            routingKey: "user.created",
            message: new UserUpsertedIntegrationEvent
            {
                Data = user.Id.ToString(),
                CorrelationId = Guid.NewGuid()
            });
        
        return user;
    }

    public async Task<User?> UpdateUserAsync(Guid id, User user)
    {
        var existingUser = await unitOfWork.Users.GetByIdAsync(id);
        if (existingUser == null)
            return null;

        existingUser.Name = user.Name;
        existingUser.LastModifiedDateTime = DateTime.UtcNow;
        unitOfWork.Users.Update(existingUser);
        await unitOfWork.SaveChangesAsync();
        
        // Publish integration event
        await messageBus.PublishAsync(
            exchange: "user.events",
            routingKey: "user.updated",
            message: new UserUpsertedIntegrationEvent
            {
                Data = existingUser.Id.ToString(),
                CorrelationId = Guid.NewGuid()
            });
        
        return existingUser;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
            return false;

        unitOfWork.Users.Remove(user);
        await unitOfWork.SaveChangesAsync();
        
        // Publish integration event
        await messageBus.PublishAsync(
            exchange: "user.events",
            routingKey: "user.deleted",
            message: new UserUpsertedIntegrationEvent
            {
                Data = user.Id.ToString(),
                CorrelationId = Guid.NewGuid()
            });
        
        return true;
    }
}
