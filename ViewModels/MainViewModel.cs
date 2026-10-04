using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TransactionManagementSystem.Data;
using TransactionManagementSystem.Models;
using TransactionManagementSystem.Services;

namespace TransactionManagementSystem.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AppDbContext _context;
        private readonly TransactionService _transactionService;
        private ObservableCollection<Transaction> _transactions;
        private Transaction _selectedTransaction;
        private decimal _totalIncoming;
        private decimal _totalOutgoing;
        private int _pendingCount;

        public MainViewModel()
        {
            _context = new AppDbContext();
            _transactionService = new TransactionService(_context);
            _transactions = new ObservableCollection<Transaction>();
            LoadData();
        }

        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set { _transactions = value; OnPropertyChanged(); }
        }

        public Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set { _selectedTransaction = value; OnPropertyChanged(); }
        }

        public decimal TotalIncoming
        {
            get => _totalIncoming;
            set { _totalIncoming = value; OnPropertyChanged(); }
        }

        public decimal TotalOutgoing
        {
            get => _totalOutgoing;
            set { _totalOutgoing = value; OnPropertyChanged(); }
        }

        public int PendingCount
        {
            get => _pendingCount;
            set { _pendingCount = value; OnPropertyChanged(); }
        }

        private void LoadData()
        {
            var transactions = _transactionService.GetAllTransactions();
            Transactions.Clear();
            foreach (var transaction in transactions)
            {
                Transactions.Add(transaction);
            }

            TotalIncoming = _transactionService.GetTotalAmount(TransactionType.Incoming);
            TotalOutgoing = _transactionService.GetTotalAmount(TransactionType.Outgoing);
            PendingCount = _transactionService.GetTransactionCount(TransactionStatus.Pending);
        }

        public void RefreshData()
        {
            LoadData();
        }
    }
}