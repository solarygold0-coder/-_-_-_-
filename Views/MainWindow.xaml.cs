using System.Windows;
using TransactionManagementSystem.ViewModels;

namespace TransactionManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}