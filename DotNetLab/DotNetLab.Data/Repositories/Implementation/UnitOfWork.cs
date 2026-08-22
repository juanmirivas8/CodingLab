using DotNetLab.Data.Context;
using DotNetLab.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace DotNetLab.Data.Repositories.Implementation;

public class UnitOfWork(ApplicationDbContext context): IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private IUserRepository? _userRepository;

    public IUserRepository Users => _userRepository ??= new UserRepository(context);

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}
