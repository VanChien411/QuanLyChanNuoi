using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;




namespace QuanLyGiong_ThucAnChanNuoi
{

    public partial class PhanQuyenNguoiDung : Window
    {
        // Danh sách người dùng (giả lập)
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FullName = "Nguyễn Văn A", Email = "a@example.com", Role = "Quản trị viên", Status = "Kích hoạt" },
            new User { Id = 2, FullName = "Trần Thị B", Email = "b@example.com", Role = "Người dùng", Status = "Khóa" },
            new User { Id = 3, FullName = "Lê Văn C", Email = "c@example.com", Role = "Khách", Status = "Kích hoạt" }
        };

        public PhanQuyenNguoiDung()
        {
            InitializeComponent();
            LoadUserData();
        }

        // Hiển thị dữ liệu trong DataGrid
        private void LoadUserData()
        {
            UserDataGrid1.ItemsSource = _users;
        }

        // Xử lý sự kiện tìm kiếm
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox1.Text.ToLower();
            var filteredUsers = _users.Where(user =>
                user.FullName.ToLower().Contains(keyword) ||
                user.Email.ToLower().Contains(keyword) ||
                user.Role.ToLower().Contains(keyword)).ToList();

            UserDataGrid1.ItemsSource = filteredUsers;
        }

        // Hiển thị hoặc ẩn Placeholder khi người dùng nhập dữ liệu
        private void SearchTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock1.Visibility = string.IsNullOrEmpty(SearchTextBox1.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        // Xử lý sự kiện cập nhật thông tin người dùng
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid1.SelectedItem is User selectedUser)
            {
                string selectedRole = (RoleComboBox1.SelectedItem as ComboBoxItem)?.Content.ToString();
                string selectedStatus = (StatusComboBox1.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (selectedRole != null && selectedStatus != null)
                {
                    selectedUser.Role = selectedRole;
                    selectedUser.Status = selectedStatus;

                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    UserDataGrid1.Items.Refresh(); // Làm mới DataGrid
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn vai trò và trạng thái!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng để cập nhật!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Xử lý sự kiện thêm người dùng mới
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Giả lập thêm người dùng mới
            int newId = _users.Max(u => u.Id) + 1;
            _users.Add(new User
            {
                Id = newId,
                FullName = "Người dùng mới",
                Email = "newuser@example.com",
                Role = "Khách",
                Status = "Kích hoạt"
            });

            MessageBox.Show("Thêm người dùng mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            UserDataGrid1.Items.Refresh(); // Làm mới DataGrid
        }
    }

   
}

