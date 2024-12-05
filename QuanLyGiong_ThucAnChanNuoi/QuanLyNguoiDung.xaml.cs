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
    public partial class QuanLyNguoiDung : Window
    {
        // Danh sách người dùng mẫu
        private List<User> _users;
        private User _selectedUser;

        public QuanLyNguoiDung()
        {
            InitializeComponent();
            LoadData();
        }

        // Tải dữ liệu mẫu
        private void LoadData()
        {
            _users = new List<User>
            {
                new User { Id = 1, FullName = "Nguyễn Văn A", Email = "nguyenvana@gmail.com", Role = "Quản trị viên", Status = "Kích hoạt" },
                new User { Id = 2, FullName = "Trần Thị B", Email = "tranthib@gmail.com", Role = "Người dùng", Status = "Khóa" }
            };

            DistrictDataGrid.ItemsSource = _users;
        }

        // Xử lý tìm kiếm
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();

            // Lọc danh sách theo từ khóa
            var filteredList = _users.Where(u =>
                u.FullName.ToLower().Contains(keyword) ||
                u.Email.ToLower().Contains(keyword) ||
                u.Role.ToLower().Contains(keyword) ||
                u.Status.ToLower().Contains(keyword)).ToList();

            DistrictDataGrid.ItemsSource = filteredList;

            if (!filteredList.Any())
            {
                MessageBox.Show("Không tìm thấy người dùng phù hợp.");
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(SearchTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        // Hiển thị chi tiết khi chọn một người dùng trong DataGrid
        private void DistrictDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DistrictDataGrid.SelectedItem is User user)
            {
                _selectedUser = user;

                // Hiển thị thông tin chi tiết trong các TextBox
                DistrictNameTextBox.Text = user.Id.ToString();
                ProvinceTextBox.Text = user.FullName;
                PostalCodeTextBox.Text = user.Email;
            }
        }

        // Thêm người dùng mới
        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new User
            {
                Id = _users.Count + 1,
                FullName = ProvinceTextBox.Text,
                Email = PostalCodeTextBox.Text,
                Role = "Người dùng", // Mặc định
                Status = "Kích hoạt" // Mặc định
            };

            _users.Add(newUser);
            RefreshDataGrid();
            MessageBox.Show("Thêm người dùng thành công!");
        }

        // Sửa thông tin người dùng
        private void EditDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUser != null)
            {
                // Cập nhật thông tin người dùng đã chọn
                _selectedUser.FullName = ProvinceTextBox.Text;
                _selectedUser.Email = PostalCodeTextBox.Text;

                RefreshDataGrid();
                MessageBox.Show("Cập nhật thông tin người dùng thành công!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng để sửa.");
            }
        }

        // Xóa người dùng
        private void DeleteDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUser != null)
            {
                _users.Remove(_selectedUser);
                RefreshDataGrid();
                MessageBox.Show("Xóa người dùng thành công!");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa.");
            }
        }

        // Làm mới DataGrid
        private void RefreshDataGrid()
        {
            DistrictDataGrid.ItemsSource = null;
            DistrictDataGrid.ItemsSource = _users;
        }
    }

    // Lớp User (đại diện cho dữ liệu người dùng)
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
