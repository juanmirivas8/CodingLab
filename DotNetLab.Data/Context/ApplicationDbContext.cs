using DotNetLab.Model.Framework;
using Microsoft.EntityFrameworkCore;

namespace DotNetLab.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    
}
