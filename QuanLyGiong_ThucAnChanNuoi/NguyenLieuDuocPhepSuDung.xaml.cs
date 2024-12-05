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
    public partial class NguyenLieuDuocPhepSuDung : Window
    {
        // ObservableCollection để lưu danh sách nguyên liệu
        private ObservableCollection<NguyenLieuModel> _dataList = new ObservableCollection<NguyenLieuModel>();

        public NguyenLieuDuocPhepSuDung()
        {
            InitializeComponent();

            // Gán dữ liệu vào DataGrid
            DistrictDataGrid.ItemsSource = _dataList;

            // Khởi tạo dữ liệu mẫu
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            _dataList.Add(new NguyenLieuModel { MaNguyenLieu = "NL01", TenNguyenLieu = "Ngô", NhomNguyenLieu = "Ngũ cốc", GhiChu = "Dùng phổ biến" });
            _dataList.Add(new NguyenLieuModel { MaNguyenLieu = "NL02", TenNguyenLieu = "Đậu nành", NhomNguyenLieu = "Họ đậu", GhiChu = "Nguồn protein cao" });
            _dataList.Add(new NguyenLieuModel { MaNguyenLieu = "NL03", TenNguyenLieu = "Bột cá", NhomNguyenLieu = "Protein động vật", GhiChu = "Dùng trong thủy sản" });
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();

            // Lọc danh sách dựa trên từ khóa
            var filteredList = _dataList.Where(x =>
                x.MaNguyenLieu.ToLower().Contains(keyword) ||
                x.TenNguyenLieu.ToLower().Contains(keyword) ||
                x.NhomNguyenLieu.ToLower().Contains(keyword) ||
                x.GhiChu.ToLower().Contains(keyword)).ToList();

            DistrictDataGrid.ItemsSource = filteredList;
        }

        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newItem = new NguyenLieuModel
                {
                    MaNguyenLieu = DistrictNameTextBox.Text,
                    TenNguyenLieu = ProvinceTextBox.Text,
                    NhomNguyenLieu = PostalCodeTextBox.Text,
                    GhiChu = PostalCodeTextBox1.Text
                };

                _dataList.Add(newItem);

                // Làm mới DataGrid
                DistrictDataGrid.ItemsSource = null;
                DistrictDataGrid.ItemsSource = _dataList;

                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DistrictDataGrid.SelectedItem as NguyenLieuModel;

            if (selectedItem != null)
            {
                selectedItem.MaNguyenLieu = DistrictNameTextBox.Text;
                selectedItem.TenNguyenLieu = ProvinceTextBox.Text;
                selectedItem.NhomNguyenLieu = PostalCodeTextBox.Text;
                selectedItem.GhiChu = PostalCodeTextBox1.Text;

                // Làm mới DataGrid
                DistrictDataGrid.ItemsSource = null;
                DistrictDataGrid.ItemsSource = _dataList;

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DistrictDataGrid.SelectedItem as NguyenLieuModel;

            if (selectedItem != null)
            {
                _dataList.Remove(selectedItem);

                // Làm mới DataGrid
                DistrictDataGrid.ItemsSource = null;
                DistrictDataGrid.ItemsSource = _dataList;

                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrWhiteSpace(SearchTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
        }
    }

    // Model dữ liệu
    public class NguyenLieuModel
    {
        public string MaNguyenLieu { get; set; }
        public string TenNguyenLieu { get; set; }
        public string NhomNguyenLieu { get; set; }
        public string GhiChu { get; set; }
    }
}
