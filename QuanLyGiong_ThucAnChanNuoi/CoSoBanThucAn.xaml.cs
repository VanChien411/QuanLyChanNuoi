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
    public partial class CoSoBanThucAn : Window
    {
        // ObservableCollection để lưu danh sách cơ sở
        private ObservableCollection<CoSoBanThucAnModel> _dataList = new ObservableCollection<CoSoBanThucAnModel>();

        public CoSoBanThucAn()
        {
            InitializeComponent();

            // Gán dữ liệu vào DataGrid
            DistrictDataGrid.ItemsSource = _dataList;

            // Khởi tạo dữ liệu mẫu
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            _dataList.Add(new CoSoBanThucAnModel { Index = 1, Name = "Cơ sở A", Address = "Hà Nội", AnimalType = "Gia cầm", Status = "Hoạt động" });
            _dataList.Add(new CoSoBanThucAnModel { Index = 2, Name = "Cơ sở B", Address = "Hồ Chí Minh", AnimalType = "Gia súc", Status = "Tạm ngừng" });
            _dataList.Add(new CoSoBanThucAnModel { Index = 3, Name = "Cơ sở C", Address = "Đà Nẵng", AnimalType = "Thủy sản", Status = "Hoạt động" });
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();

            // Lọc danh sách dựa trên từ khóa
            var filteredList = _dataList.Where(x =>
                x.Name.ToLower().Contains(keyword) ||
                x.Address.ToLower().Contains(keyword) ||
                x.AnimalType.ToLower().Contains(keyword) ||
                x.Status.ToLower().Contains(keyword)).ToList();

            DistrictDataGrid.ItemsSource = filteredList;
        }

        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newItem = new CoSoBanThucAnModel
                {
                    Index = _dataList.Count > 0 ? _dataList.Max(x => x.Index) + 1 : 1,
                    Name = ProvinceTextBox.Text,
                    Address = PostalCodeTextBox.Text,
                    AnimalType = PostalCodeTextBox1.Text,
                    Status = PostalCodeTextBox2.Text
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
            var selectedItem = DistrictDataGrid.SelectedItem as CoSoBanThucAnModel;

            if (selectedItem != null)
            {
                selectedItem.Name = ProvinceTextBox.Text;
                selectedItem.Address = PostalCodeTextBox.Text;
                selectedItem.AnimalType = PostalCodeTextBox1.Text;
                selectedItem.Status = PostalCodeTextBox2.Text;

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
            var selectedItem = DistrictDataGrid.SelectedItem as CoSoBanThucAnModel;

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
    public class CoSoBanThucAnModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AnimalType { get; set; }
        public string Status { get; set; }
    }
}
