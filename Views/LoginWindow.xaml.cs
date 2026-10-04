using System.Windows;
using TransactionManagementSystem.Data;
using TransactionManagementSystem.Services;

namespace TransactionManagementSystem
{
    public partial class LoginWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly UserService _userService;

        public LoginWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
            _userService = new UserService(_context);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            var user = _userService.AuthenticateUser(username, password);
            if (user == null)
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة.", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }
    }
}
