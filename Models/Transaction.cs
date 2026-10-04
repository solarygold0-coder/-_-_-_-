using System;

namespace TransactionManagementSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);
        public string ContactName { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime? CompletedDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public User User { get; set; }
    }

    public enum TransactionType
    {
        Outgoing = 1,
        Incoming = 2
    }

    public enum TransactionStatus
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        Overdue = 4,
        Cancelled = 5
    }
}
