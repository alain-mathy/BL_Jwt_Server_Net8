using BL_Jwt_Server_Net8.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL_Jwt_Server_Net8.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
