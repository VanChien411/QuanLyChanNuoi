using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using QuanLyGiong_ThucAnChanNuoi.ViewModel;

using System.Collections.ObjectModel;


namespace QuanLyGiong_ThucAnChanNuoi
{
    public partial class HoaChatCam : Window
    {
        // ObservableCollection để lưu danh sách hóa chất
        private ObservableCollection<HoaChatModel> _dataList = new ObservableCollection<HoaChatModel>();

        public HoaChatCam()
        {
            InitializeComponent();

            // Gán dữ liệu vào DataGrid
            //DistrictDataGrid.ItemsSource = _dataList;

            // Khởi tạo dữ liệu mẫu
            //LoadSampleData();
        }

        private void LoadSampleData()
        {
            _dataList.Add(new HoaChatModel { MaDanhMuc = "HC01", TenDanhMuc = "Hóa chất A", Loai = "Hóa chất", MoTa = "Sử dụng trong chăn nuôi", NgayApDung = "2023-01-01" });
            _dataList.Add(new HoaChatModel { MaDanhMuc = "HC02", TenDanhMuc = "Vi sinh vật B", Loai = "Vi sinh vật", MoTa = "Thúc đẩy tiêu hóa", NgayApDung = "2023-03-15" });
            _dataList.Add(new HoaChatModel { MaDanhMuc = "HC03", TenDanhMuc = "Sản phẩm C", Loai = "Sản phẩm", MoTa = "Dùng trong thủy sản", NgayApDung = "2023-06-10" });
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //string keyword = SearchTextBox.Text.ToLower();

            //// Lọc danh sách dựa trên từ khóa
            //var filteredList = _dataList.Where(x =>
            //    x.MaDanhMuc.ToLower().Contains(keyword) ||
            //    x.TenDanhMuc.ToLower().Contains(keyword) ||
            //    x.Loai.ToLower().Contains(keyword) ||
            //    x.MoTa.ToLower().Contains(keyword) ||
            //    x.NgayApDung.ToLower().Contains(keyword)).ToList();

            //DistrictDataGrid.ItemsSource = filteredList;
        }

        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    var newItem = new HoaChatModel
            //    {
            //        MaDanhMuc = DistrictNameTextBox.Text,
            //        TenDanhMuc = ProvinceTextBox.Text,
            //        Loai = PostalCodeTextBox.Text,
            //        MoTa = PostalCodeTextBox1.Text,
            //        NgayApDung = PostalCodeTextBox2.Text
            //    };

            //    _dataList.Add(newItem);

            //    // Làm mới DataGrid
            //    DistrictDataGrid.ItemsSource = null;
            //    DistrictDataGrid.ItemsSource = _dataList;

            //    MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void EditDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItem = DistrictDataGrid.SelectedItem as HoaChatModel;

            //if (selectedItem != null)
            //{
            //    selectedItem.MaDanhMuc = DistrictNameTextBox.Text;
            //    selectedItem.TenDanhMuc = ProvinceTextBox.Text;
            //    selectedItem.Loai = PostalCodeTextBox.Text;
            //    selectedItem.MoTa = PostalCodeTextBox1.Text;
            //    selectedItem.NgayApDung = PostalCodeTextBox2.Text;

            //    // Làm mới DataGrid
            //    DistrictDataGrid.ItemsSource = null;
            //    DistrictDataGrid.ItemsSource = _dataList;

            //    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn một mục để sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private void DeleteDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItem = DistrictDataGrid.SelectedItem as HoaChatModel;

            //if (selectedItem != null)
            //{
            //    _dataList.Remove(selectedItem);

            //    // Làm mới DataGrid
            //    DistrictDataGrid.ItemsSource = null;
            //    DistrictDataGrid.ItemsSource = _dataList;

            //    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn một mục để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrWhiteSpace(SearchTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
        }
    }

    // Model dữ liệu
    public class HoaChatModel
    {
        public string MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public string Loai { get; set; }
        public string MoTa { get; set; }
        public string NgayApDung { get; set; }
    }
}
