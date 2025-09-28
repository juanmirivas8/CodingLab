using DotNetLab.Data.Context;
using DotNetLab.Data.Repositories.Interfaces;
using DotNetLab.Model.Framework;
using Microsoft.EntityFrameworkCore;

namespace DotNetLab.Data.Repositories.Implementation;

public class UserRepository(ApplicationDbContext context): Repository<User>(context), IUserRepository
{
    public async Task<IEnumerable<User>> GetUsersByNameAsync(string name)
    {
        return await DbSet
            .Where(u => u.Name.Contains(name))
            .OrderBy(u => u.Name)
            .ToListAsync();
    }

    public async Task<User?> GetUserByNameAsync(string name)
    {
        return await DbSet
            .FirstOrDefaultAsync(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<User>> GetUsersCreatedAfterAsync(DateTime date)
    {
        return await DbSet
            .Where(u => u.CreatedDateTime > date)
            .OrderByDescending(u => u.CreatedDateTime)
            .ToListAsync();
    }

    public async Task<bool> UserExistsByNameAsync(string name)
    {
        return await DbSet
            .AnyAsync(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
