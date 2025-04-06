using TempTake_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace TempTake_Server.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<ManagerWorker> ManagerWorkers { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserManager> UserManagers { get; set; }
    }
}