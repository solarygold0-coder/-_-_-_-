using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using TransactionManagementSystem.Data;
using TransactionManagementSystem.Models;
using TransactionManagementSystem.Services;

namespace TransactionManagementSystem
{
    public partial class ReportsWindow : Window
    {
        private readonly ReportService _reportService;

        public ReportsWindow()
        {
            InitializeComponent();
            var context = new AppDbContext();
            _reportService = new ReportService(context);
            LoadData();
        }

        private void LoadData()
        {
            var transactions = _reportService.GetAllTransactions();
            ReportsGrid.ItemsSource = transactions;
            IncomingSummaryText.Text = _reportService.GetTotalIncoming().ToString("N2") + " ر.س";
            OutgoingSummaryText.Text = _reportService.GetTotalOutgoing().ToString("N2") + " ر.س";
            PendingSummaryText.Text = _reportService.GetPendingTransactionsCount().ToString();
        }

        private void ExportCsvButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "التقارير_المعاملات.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                var rows = _reportService.GetAllTransactions();
                var lines = new List<string>
                {
                    "رقم المرجع,العنوان,النوع,الحالة,المبلغ,التاريخ"
                };

                foreach (var item in rows)
                {
                    lines.Add($"{item.ReferenceNumber},{item.Title},{item.Type},{item.Status},{item.Amount},{item.CreatedDate:yyyy/MM/dd}");
                }

                File.WriteAllLines(dialog.FileName, lines);
                MessageBox.Show("تم تصدير التقرير بنجاح.", "نجاح", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
