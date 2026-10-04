using System.Collections.Generic;
using System.Linq;
using TransactionManagementSystem.Data;
using TransactionManagementSystem.Models;

namespace TransactionManagementSystem.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public User AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            return _context.Users
                .FirstOrDefault(u => u.Username == username.Trim() && u.Password == password && u.IsActive);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.OrderBy(u => u.FullName).ToList();
        }

        public User GetDefaultAdminUser()
        {
            return _context.Users.FirstOrDefault(u => u.Role == UserRole.Admin);
        }
    }
}
