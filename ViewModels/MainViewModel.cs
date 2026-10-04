using System.Collections.ObjectModel;
using System.Linq;
using TransactionManagementSystem.Data;
using TransactionManagementSystem.Models;
using TransactionManagementSystem.Services;

namespace TransactionManagementSystem.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AppDbContext _context;
        private readonly TransactionService _transactionService;
        private ObservableCollection<Transaction> _transactions = new ObservableCollection<Transaction>();
        private Transaction _selectedTransaction;
        private decimal _totalIncoming;
        private decimal _totalOutgoing;
        private int _pendingCount;

        public MainViewModel()
        {
            _context = new AppDbContext();
            _transactionService = new TransactionService(_context);
            _context.Database.EnsureCreated();
            SeedData();
            LoadTransactions();
        }

        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set { _transactions = value; OnPropertyChanged(); }
        }

        public Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set
            {
                _selectedTransaction = value;
                OnPropertyChanged();
            }
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

        public void LoadTransactions()
        {
            Transactions.Clear();
            var items = _transactionService.GetAllTransactions();
            foreach (var item in items)
            {
                Transactions.Add(item);
            }

            TotalIncoming = _transactionService.GetTotalAmount(TransactionType.Incoming);
            TotalOutgoing = _transactionService.GetTotalAmount(TransactionType.Outgoing);
            PendingCount = _transactionService.GetTransactionCount(TransactionStatus.Pending);
        }

        public void AddOrUpdateTransaction(Transaction transaction)
        {
            if (transaction == null) return;

            if (transaction.Id == 0)
            {
                _transactionService.AddTransaction(transaction);
            }
            else
            {
                _transactionService.UpdateTransaction(transaction);
            }

            LoadTransactions();
        }

        public void DeleteTransaction(int id)
        {
            _transactionService.DeleteTransaction(id);
            LoadTransactions();
        }

        public void SearchTransactions(string searchText)
        {
            var results = _transactionService.SearchTransactions(searchText);
            Transactions.Clear();
            foreach (var item in results)
            {
                Transactions.Add(item);
            }
        }

        public Transaction CreateNewTransaction()
        {
            return new Transaction
            {
                ReferenceNumber = "TR-" + System.DateTime.Now.ToString("yyyyMMddHHmmss"),
                Title = string.Empty,
                Description = string.Empty,
                Type = TransactionType.Incoming,
                Status = TransactionStatus.Pending,
                Amount = 0,
                Category = "عام",
                CreatedDate = System.DateTime.Now,
                DueDate = System.DateTime.Now.AddDays(7),
                ContactName = string.Empty,
                ContactPhone = string.Empty,
                ContactEmail = string.Empty,
                Notes = string.Empty
            };
        }

        private void SeedData()
        {
            if (_context.Transactions.Any()) return;

            var seedTransactions = new[]
            {
                new Transaction
                {
                    ReferenceNumber = "TR-20260101",
                    Title = "مبيعات شهرية",
                    Description = "معاملة واردة من العميل الأول",
                    Type = TransactionType.Incoming,
                    Status = TransactionStatus.Completed,
                    Amount = 2500,
                    Category = "مبيعات",
                    ContactName = "أحمد علي",
                    ContactPhone = "0501234567",
                    ContactEmail = "ahmed@test.com",
                    Notes = "تمت التسوية كاملة",
                    CreatedDate = System.DateTime.Now.AddDays(-5),
                    DueDate = System.DateTime.Now.AddDays(2),
                    CompletedDate = System.DateTime.Now.AddDays(-3)
                },
                new Transaction
                {
                    ReferenceNumber = "TR-20260102",
                    Title = "شراء مواد مكتبية",
                    Description = "مشتريات داخلية لمكتب الشركة",
                    Type = TransactionType.Outgoing,
                    Status = TransactionStatus.Pending,
                    Amount = 780,
                    Category = "مشتريات",
                    ContactName = "شركة المثالي",
                    ContactPhone = "0557654321",
                    ContactEmail = "sales@company.com",
                    Notes = "بانتظار الدفع",
                    CreatedDate = System.DateTime.Now.AddDays(-2),
                    DueDate = System.DateTime.Now.AddDays(4)
                }
            };

            _context.Transactions.AddRange(seedTransactions);
            _context.SaveChanges();
        }
    }
}
