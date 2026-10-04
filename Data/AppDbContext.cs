using Microsoft.EntityFrameworkCore;
using TransactionManagementSystem.Models;

namespace TransactionManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TransactionManagement.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تكوين علاقات النموذج
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId);

            // البيانات الأولية
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    FullName = "مسؤول النظام",
                    Email = "admin@system.com",
                    Password = "admin123",
                    Role = UserRole.Admin,
                    IsActive = true,
                    CreatedDate = System.DateTime.Now
                }
            );
        }
    }
}