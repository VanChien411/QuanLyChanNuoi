using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using QuanLyGiong_ThucAnChanNuoi.Properties;

namespace QuanLyGiong_ThucAnChanNuoi
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Tự động điền thông tin đăng nhập nếu đã lưu
            usernameTextBox.Text = Settings.Default.Username;
            passwordBox.Password = Settings.Default.Password;
            rememberPasswordCheckBox.IsChecked = !string.IsNullOrEmpty(Settings.Default.Username);
            this.WindowState = WindowState.Maximized;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Visibility == Visibility.Visible
                                ? passwordBox.Password
                                : passwordTextBox.Text;
            bool rememberPassword = rememberPasswordCheckBox.IsChecked ?? false;

            // Thay đổi logic xác thực ở đây
            if (await IsValidLogin(username, password))
            {
                // Lưu thông tin đăng nhập nếu chọn "Nhớ mật khẩu"
                if (rememberPassword)
                {
                    Settings.Default.Username = username;
                    Settings.Default.Password = password;
                }
                else
                {
                    Settings.Default.Username = "";
                    Settings.Default.Password = "";
                }

                // Lưu cài đặt
                Settings.Default.Save();



                // Lấy giá trị loại người dùng được chọn
                var selectedRole = (userRoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                // Chuyển hướng theo từng loại người dùng
                if (selectedRole == "Quản trị hệ thống")
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else if (selectedRole == "Chuyên viên chăn nuôi")
                {
                    SpecialistWindow specialistWindow = new SpecialistWindow();
                    specialistWindow.Show();
                }
                else if (selectedRole == "Người dùng")
                {
                    UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                }

                // Đóng cửa sổ đăng nhập sau khi chuyển hướng
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private async Task<bool> IsValidLogin(string username, string password)
        {
            using (var context = new QuanLyGiongVaThucAnChanNuoiContext()) {
                try {
                    var user = await context.NguoiDungs.Include(c => c.ChucVu).FirstOrDefaultAsync(x => x.TenDn == username && x.MatKhau == password && x.TrangThai == true);
                    if (user != null)
                    {
                        Debug.WriteLine( user.ChucVu?.TenChucVu);
                    }
                }
                catch (Exception ex) { 
                }
               
            }
            // Kiểm tra thông tin đăng nhập
            // Thay đổi logic này để kiểm tra từ cơ sở dữ liệu hoặc từ danh sách hợp lệ
            return username == "admin" && password == "123456"; // Ví dụ thông tin đăng nhập hợp lệ
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Text = passwordBox.Password;
            passwordTextBox.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = passwordTextBox.Text;
            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Visibility == Visibility.Visible)
            {
                passwordTextBox.Text = passwordBox.Password;
            }
        }

    }
}
