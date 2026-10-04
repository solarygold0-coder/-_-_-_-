using System;
using System.Collections.Generic;

namespace TransactionManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    public enum UserRole
    {
        Admin = 1,
        Manager = 2,
        Employee = 3
    }
}
