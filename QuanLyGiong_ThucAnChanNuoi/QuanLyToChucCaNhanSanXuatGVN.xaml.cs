using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using QuanLyGiong_ThucAnChanNuoi.ViewModel;
namespace QuanLyGiong_ThucAnChanNuoi
{
    public partial class QuanLyToChucCaNhanSanXuatGVN : Window
    {
        // Mô hình dữ liệu cho tổ chức/cá nhân
        public class Organization
        {
            public int Index { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Type { get; set; }
        }
        // Dữ liệu mẫu lưu trữ các tổ chức/cá nhân
        private ObservableCollection<Organization> _organizations;

        public QuanLyToChucCaNhanSanXuatGVN()
        {
            InitializeComponent();
            DataContext = new QuanLySanXuatGiongVNViewModel();
            //LoadData();
        }

        // Load dữ liệu ban đầu
        private void LoadData()
        {
            //_organizations = new ObservableCollection<Organization>
            //{
            //    new Organization { Index = 1, Name = "Công ty A", Address = "Hà Nội", Phone = "0123456789", Type = "Tổ chức" },
            //    new Organization { Index = 2, Name = "Cá nhân B", Address = "TP HCM", Phone = "0987654321", Type = "Cá nhân" }
            //};

            //DistrictDataGrid.ItemsSource = _organizations;
        }

        // Xử lý sự kiện Tìm kiếm
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //string keyword = SearchTextBox.Text.ToLower();
            //var filteredList = _organizations.Where(org =>
            //    org.Name.ToLower().Contains(keyword) ||
            //    org.Address.ToLower().Contains(keyword) ||
            //    org.Phone.Contains(keyword) ||
            //    org.Type.ToLower().Contains(keyword)).ToList();

            //DistrictDataGrid.ItemsSource = new ObservableCollection<Organization>(filteredList);
        }

        // Thêm mới tổ chức/cá nhân
        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            //if (int.TryParse(DistrictNameTextBox.Text, out int id))
            //{
            //    _organizations.Add(new Organization
            //    {
            //        Index = id,
            //        Name = ProvinceTextBox.Text,
            //        Address = PostalCodeTextBox.Text,
            //        Phone = ProvinceTextBox.Text,
            //        Type = ProvinceTextBox.Text
            //    });
            //}
            //else
            //{
            //    MessageBox.Show("ID phải là số hợp lệ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        // Sửa thông tin tổ chức/cá nhân
        private void EditDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            //if (DistrictDataGrid.SelectedItem is Organization selectedOrg)
            //{
            //    selectedOrg.Name = ProvinceTextBox.Text;
            //    selectedOrg.Address = PostalCodeTextBox.Text;
            //    DistrictDataGrid.Items.Refresh();
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn một mục trong danh sách.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        // Xóa tổ chức/cá nhân
        private void DeleteDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            //if (DistrictDataGrid.SelectedItem is Organization selectedOrg)
            //{
            //    _organizations.Remove(selectedOrg);
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn một mục để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        // Placeholder TextBox Tìm kiếm
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(SearchTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }

    
}