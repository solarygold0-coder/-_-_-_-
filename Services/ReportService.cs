using System;
using System.Collections.Generic;
using System.Linq;
using TransactionManagementSystem.Data;
using TransactionManagementSystem.Models;

namespace TransactionManagementSystem.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.OrderByDescending(t => t.CreatedDate).ToList();
        }

        public decimal GetTotalIncoming()
        {
            return _context.Transactions.Where(t => t.Type == TransactionType.Incoming).Sum(t => t.Amount);
        }

        public decimal GetTotalOutgoing()
        {
            return _context.Transactions.Where(t => t.Type == TransactionType.Outgoing).Sum(t => t.Amount);
        }

        public int GetPendingTransactionsCount()
        {
            return _context.Transactions.Count(t => t.Status == TransactionStatus.Pending);
        }

        public Dictionary<string, decimal> GetMonthlySummary()
        {
            var result = new Dictionary<string, decimal>();
            var startDate = DateTime.Now.AddMonths(-5);

            for (int i = 0; i < 6; i++)
            {
                var monthDate = startDate.AddMonths(i);
                var label = monthDate.ToString("yyyy/MM");
                result[label] = _context.Transactions
                    .Where(t => t.CreatedDate.Year == monthDate.Year && t.CreatedDate.Month == monthDate.Month)
                    .Sum(t => t.Amount);
            }

            return result;
        }
    }
}
