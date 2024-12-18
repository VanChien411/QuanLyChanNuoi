using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using System;

using System.Windows;


namespace QuanLyGiong_ThucAnChanNuoi
{
    /// <summary>
    /// Interaction logic for QuenMatKhau.xaml
    /// </summary>
    public partial class QuenMatKhau : Window
    {
        public QuenMatKhau()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
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
        private void ReShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            RepasswordTextBox.Text = RepasswordBox.Password;
            RepasswordTextBox.Visibility = Visibility.Visible;
            RepasswordBox.Visibility = Visibility.Collapsed;
        }

        private void ReShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            RepasswordBox.Password = RepasswordTextBox.Text;
            RepasswordTextBox.Visibility = Visibility.Collapsed;
            RepasswordBox.Visibility = Visibility.Visible;
        }

        private void RepasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RepasswordBox.Visibility == Visibility.Visible)
            {
                RepasswordTextBox.Text = RepasswordBox.Password;
            }
        }



        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(usernameTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập");
                return;

            }
            else if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                return;

            }
            else if (string.IsNullOrEmpty(RepasswordTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu");
                return;

            }
            else if (passwordTextBox.Text != RepasswordTextBox.Text) {
                MessageBox.Show("Mật khẩu và mật khẩu nhập lại không trùng khớp");
                return;

            }
            using (var context = new QuanLyGiongVaThucAnChanNuoiContext())
            {
                try
                {
                    var user = await context.NguoiDungs.Include(c => c.ChucVu).FirstOrDefaultAsync(x => x.TenDn == usernameTextBox.Text && x.TrangThai == true);
                    if (user != null)
                    {
                        user.MatKhau = passwordTextBox.Text;
                        context.SaveChanges();
                        MessageBox.Show("Đổi mật khẩu thành công");
                    }
                    else {
                        MessageBox.Show("Tên đăng nhập không tồn tại");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi kết nối {ex} ");
                }

            }
        }
    }
}
