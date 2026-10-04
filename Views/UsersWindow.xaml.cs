using System.Linq;
using System.Windows;
using TransactionManagementSystem.Data;
using TransactionManagementSystem.Models;

namespace TransactionManagementSystem
{
    public partial class UsersWindow : Window
    {
        private readonly AppDbContext _context;

        public UsersWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _context.Users.Select(u => new UserSummary
            {
                Id = u.Id,
                FullName = u.FullName,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role.ToString(),
                IsActive = u.IsActive,
                CreatedDate = u.CreatedDate
            }).ToList();

            UsersGrid.ItemsSource = users;
        }

        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            var fullName = FullNameText.Text.Trim();
            var username = UsernameText.Text.Trim();
            var email = EmailText.Text.Trim();
            var password = PasswordBox.Password.Trim();
            var role = RoleCombo.SelectedIndex switch
            {
                0 => UserRole.Admin,
                1 => UserRole.Employee,
                _ => UserRole.Manager
            };

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("يرجى إدخال جميع الحقول المطلوبة.", "تنبيه", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var existing = _context.Users.FirstOrDefault(u => u.Username == username || u.Email == email);
            if (existing != null)
            {
                MessageBox.Show("اسم المستخدم أو البريد الإلكتروني موجود مسبقًا.", "تنبيه", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _context.Users.Add(new User
            {
                FullName = fullName,
                Username = username,
                Email = email,
                Password = password,
                Role = role,
                IsActive = true,
                CreatedDate = System.DateTime.Now
            });

            _context.SaveChanges();
            LoadUsers();
            MessageBox.Show("تم حفظ المستخدم بنجاح.", "نجاح", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
