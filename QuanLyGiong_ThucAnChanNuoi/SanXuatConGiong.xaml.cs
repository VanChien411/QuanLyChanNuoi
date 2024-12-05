using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyGiong_ThucAnChanNuoi
{

    public partial class SanXuatConGiong : Window
    {
        public ObservableCollection<Organization> Organizations { get; set; }

        public SanXuatConGiong()
        {
            InitializeComponent();

            // Khởi tạo dữ liệu mẫu
            Organizations = new ObservableCollection<Organization>
            {
                new Organization { Id = 1, Name = "Công ty ABC", Address = "Hà Nội", Phone = "0123456789", Email = "abc@company.com" },
                new Organization { Id = 2, Name = "Công ty XYZ", Address = "Hồ Chí Minh", Phone = "0987654321", Email = "xyz@company.com" }
            };

            // Gán nguồn dữ liệu cho DataGrid
            OrganizationDataGrid.ItemsSource = Organizations;
        }

        // Phương thức xử lý sự kiện cho nút Thêm mới
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newOrg = new Organization
            {
                Id = Organizations.Count + 1,  // Tự động tăng ID
                Name = NameTextBox.Text,
                Address = AddressTextBox.Text,
                Phone = PhoneTextBox.Text,
                Email = EmailTextBox.Text
            };

            Organizations.Add(newOrg);  // Thêm tổ chức vào danh sách
            ResetForm();  // Reset form
        }

        // Phương thức xử lý sự kiện cho nút Sửa
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có tổ chức nào được chọn không
            if (OrganizationDataGrid.SelectedItem is Organization selectedOrg)
            {
                // Cập nhật thông tin tổ chức
                selectedOrg.Name = NameTextBox.Text;
                selectedOrg.Address = AddressTextBox.Text;
                selectedOrg.Phone = PhoneTextBox.Text;
                selectedOrg.Email = EmailTextBox.Text;

                // Cập nhật lại DataGrid
                OrganizationDataGrid.Items.Refresh();
                ResetForm();  // Reset form sau khi sửa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tổ chức/cá nhân cần sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Phương thức xử lý sự kiện cho nút Xóa
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có tổ chức nào được chọn không
            if (OrganizationDataGrid.SelectedItem is Organization selectedOrg)
            {
                Organizations.Remove(selectedOrg);  // Xóa tổ chức khỏi danh sách
                ResetForm();  // Reset form sau khi xóa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tổ chức/cá nhân cần xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Phương thức xử lý sự kiện cho nút Reset
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();  // Reset form khi nhấn nút Reset
        }

        // Phương thức reset lại tất cả các ô nhập liệu
        private void ResetForm()
        {
            NameTextBox.Clear();  // Xóa nội dung ô tên tổ chức
            AddressTextBox.Clear();  // Xóa nội dung ô địa chỉ
            PhoneTextBox.Clear();  // Xóa nội dung ô số điện thoại
            EmailTextBox.Clear();  // Xóa nội dung ô email
            OrganizationDataGrid.SelectedItem = null;  // Bỏ chọn tổ chức trong DataGrid
        }
    }

    // Lớp Organization dùng để lưu thông tin của tổ chức/cá nhân
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}