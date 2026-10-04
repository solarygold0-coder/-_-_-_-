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

        public void AddTransaction(Transaction transaction)
        {
            transaction.CreatedDate = DateTime.Now;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
        }

        public Transaction GetTransactionById(int id)
        {
            return _context.Transactions.FirstOrDefault(t => t.Id == id);
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }

        public List<Transaction> GetTransactionsByType(TransactionType type)
        {
            return _context.Transactions.Where(t => t.Type == type).ToList();
        }

        public List<Transaction> GetTransactionsByStatus(TransactionStatus status)
        {
            return _context.Transactions.Where(t => t.Status == status).ToList();
        }

        public List<Transaction> SearchTransactions(string searchTerm)
        {
            return _context.Transactions
                .Where(t => t.Title.Contains(searchTerm) || 
                           t.ReferenceNumber.Contains(searchTerm) ||
                           t.ContactName.Contains(searchTerm))
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