using DBDatas;

namespace AnimalTargetUsers.DBWork;
using Microsoft.EntityFrameworkCore;

public class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public UsersDbContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=animal_service;" +
                                 "User Id=postgres;Password=riv;");
    }
}