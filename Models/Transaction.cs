using System;

namespace TransactionManagementSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Notes { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Category { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
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