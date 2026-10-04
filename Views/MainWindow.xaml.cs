using System;
using System.Windows;
using TransactionManagementSystem.Models;
using TransactionManagementSystem.ViewModels;

namespace TransactionManagementSystem
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private readonly User _loggedInUser;
        private Transaction _editingTransaction;

        public MainWindow() : this(new User
        {
            FullName = "مسؤول النظام",
            Username = "admin"
        })
        {
        }

        public MainWindow(User loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser ?? new User { FullName = "مسؤول النظام", Username = "admin" };
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
            this.Title = "نظام إدارة المعاملات الصادرة والواردة - " + _loggedInUser.FullName;
            NewButton_Click(null, null);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchTransactions(SearchTextBox.Text);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var transaction = _editingTransaction ?? _viewModel.CreateNewTransaction();

            transaction.ReferenceNumber = string.IsNullOrWhiteSpace(ReferenceNumberText.Text) ? transaction.ReferenceNumber : ReferenceNumberText.Text.Trim();
            transaction.Title = TitleText.Text.Trim();
            transaction.Description = DescriptionText.Text.Trim();
            transaction.Type = TypeCombo.SelectedIndex == 0 ? TransactionType.Outgoing : TransactionType.Incoming;
            transaction.Status = StatusCombo.SelectedIndex switch
            {
                0 => TransactionStatus.Pending,
                1 => TransactionStatus.InProgress,
                2 => TransactionStatus.Completed,
                3 => TransactionStatus.Overdue,
                _ => TransactionStatus.Cancelled
            };

            if (decimal.TryParse(AmountText.Text, out var amount))
            {
                transaction.Amount = amount;
            }

            transaction.Category = string.IsNullOrWhiteSpace(CategoryText.Text) ? "عام" : CategoryText.Text.Trim();
            transaction.ContactName = ContactNameText.Text.Trim();
            transaction.ContactPhone = ContactPhoneText.Text.Trim();
            transaction.ContactEmail = ContactEmailText.Text.Trim();
            transaction.Notes = NotesText.Text.Trim();

            if (transaction.Id == 0)
            {
                transaction.CreatedDate = DateTime.Now;
            }

            _viewModel.AddOrUpdateTransaction(transaction);
            _editingTransaction = null;
            NewButton_Click(sender, e);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_editingTransaction == null) return;

            _viewModel.DeleteTransaction(_editingTransaction.Id);
            _editingTransaction = null;
            NewButton_Click(sender, e);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            _editingTransaction = _viewModel.CreateNewTransaction();
            PopulateForm(_editingTransaction);
        }

        private void TransactionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TransactionsGrid.SelectedItem is Transaction selected)
            {
                _editingTransaction = selected;
                PopulateForm(selected);
            }
        }

        private void PopulateForm(Transaction transaction)
        {
            ReferenceNumberText.Text = transaction.ReferenceNumber;
            TitleText.Text = transaction.Title;
            DescriptionText.Text = transaction.Description;
            TypeCombo.SelectedIndex = transaction.Type == TransactionType.Outgoing ? 0 : 1;
            StatusCombo.SelectedIndex = (int)transaction.Status - 1;
            AmountText.Text = transaction.Amount.ToString();
            CategoryText.Text = transaction.Category;
            ContactNameText.Text = transaction.ContactName;
            ContactPhoneText.Text = transaction.ContactPhone;
            ContactEmailText.Text = transaction.ContactEmail;
            NotesText.Text = transaction.Notes;
        }
    }
}
