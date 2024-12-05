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

using System.Collections.ObjectModel;


namespace QuanLyGiong_ThucAnChanNuoi
{
    public partial class QuanLySanXuatTinhPhoi : Window
    {

        // Mô hình dữ liệu
        public class Organization
        {
            public int Index { get; set; } // ID
            public string Name { get; set; } // Tên tổ chức/cá nhân
            public string Address { get; set; } // Địa chỉ
            public string Type { get; set; } // Loại hình
        }
        // Dữ liệu danh sách tổ chức/cá nhân
        private ObservableCollection<Organization> _organizations;

        public QuanLySanXuatTinhPhoi()
        {
            InitializeComponent();
            LoadData();
        }

        // Tải dữ liệu ban đầu
        private void LoadData()
        {
            _organizations = new ObservableCollection<Organization>
            {
                new Organization { Index = 1, Name = "Công ty ABC", Address = "Hà Nội", Type = "Tinh" },
                new Organization { Index = 2, Name = "Hộ gia đình XYZ", Address = "TP HCM", Type = "Phôi" }
            };

            DistrictDataGrid.ItemsSource = _organizations;
        }

        // Xử lý tìm kiếm
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();
            var filteredList = _organizations.Where(org =>
                org.Name.ToLower().Contains(keyword) ||
                org.Address.ToLower().Contains(keyword) ||
                org.Type.ToLower().Contains(keyword)).ToList();

            DistrictDataGrid.ItemsSource = new ObservableCollection<Organization>(filteredList);
        }

        // Placeholder TextBox Tìm kiếm

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(SearchTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        // Thêm mới tổ chức/cá nhân
        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DistrictNameTextBox.Text, out int id))
            {
                _organizations.Add(new Organization
                {
                    Index = id,
                    Name = ProvinceTextBox.Text,
                    Address = PostalCodeTextBox.Text,
                    Type = PostalCodeTextBox2.Text
                });
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("ID phải là số hợp lệ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Sửa thông tin tổ chức/cá nhân
        private void EditDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            if (DistrictDataGrid.SelectedItem is Organization selectedOrg)
            {
                selectedOrg.Name = ProvinceTextBox.Text;
                selectedOrg.Address = PostalCodeTextBox.Text;
                selectedOrg.Type = PostalCodeTextBox2.Text;

                DistrictDataGrid.Items.Refresh();
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục trong danh sách.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Xóa tổ chức/cá nhân
        private void DeleteDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            if (DistrictDataGrid.SelectedItem is Organization selectedOrg)
            {
                _organizations.Remove(selectedOrg);
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

}
