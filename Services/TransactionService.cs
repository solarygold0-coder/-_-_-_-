using System;
using System.Collections.Generic;
using System.Linq;
using TransactionManagementSystem.Data;
using TransactionManagementSystem.Models;

namespace TransactionManagementSystem.Services
{
    public class TransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            transaction.CreatedDate = DateTime.Now;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }

        public void DeleteTransaction(int id)
        {
            var existing = _context.Transactions.FirstOrDefault(t => t.Id == id);
            if (existing != null)
            {
                _context.Transactions.Remove(existing);
                _context.SaveChanges();
            }
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.OrderByDescending(t => t.CreatedDate).ToList();
        }

        public List<Transaction> SearchTransactions(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return GetAllTransactions();
            }

            var txt = searchText.Trim();
            return _context.Transactions
                .Where(t => t.ReferenceNumber.Contains(txt)
                    || t.Title.Contains(txt)
                    || t.ContactName.Contains(txt)
                    || t.Category.Contains(txt))
                .OrderByDescending(t => t.CreatedDate)
                .ToList();
        }

        public decimal GetTotalAmount(TransactionType type)
        {
            return _context.Transactions
                .Where(t => t.Type == type)
                .Sum(t => t.Amount);
        }

        public int GetTransactionCount(TransactionStatus status)
        {
            return _context.Transactions.Count(t => t.Status == status);
        }
    }
}
