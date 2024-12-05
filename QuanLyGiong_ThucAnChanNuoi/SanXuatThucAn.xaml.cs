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

namespace QuanLyGiong_ThucAnChanNuoi
{
    public partial class SanXuatThucAn : Window
    {
        // ObservableCollection lưu trữ danh sách cơ sở
        private ObservableCollection<SanXuatThucAnModel> _dataList = new ObservableCollection<SanXuatThucAnModel>();

        public SanXuatThucAn()
        {
            InitializeComponent();

            // Gán dữ liệu vào DataGrid
            DistrictDataGrid.ItemsSource = _dataList;

            // Khởi tạo dữ liệu mẫu
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            _dataList.Add(new SanXuatThucAnModel { Id = 1, Name = "Cơ sở A", ProductType = "Thức ăn gia cầm", Region = "Miền Bắc", LastUpdated = DateTime.Now });
            _dataList.Add(new SanXuatThucAnModel { Id = 2, Name = "Cơ sở B", ProductType = "Thức ăn gia súc", Region = "Miền Nam", LastUpdated = DateTime.Now });
            _dataList.Add(new SanXuatThucAnModel { Id = 3, Name = "Cơ sở C", ProductType = "Thức ăn thủy sản", Region = "Miền Trung", LastUpdated = DateTime.Now });
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();

            // Lọc dữ liệu
            var filteredList = _dataList.Where(x =>
                x.Name.ToLower().Contains(keyword) ||
                x.ProductType.ToLower().Contains(keyword) ||
                x.Region.ToLower().Contains(keyword)).ToList();

            DistrictDataGrid.ItemsSource = filteredList;
        }

        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newItem = new SanXuatThucAnModel
                {
                    Id = _dataList.Count > 0 ? _dataList.Max(x => x.Id) + 1 : 1,
                    Name = ProvinceTextBox.Text,
                    ProductType = PostalCodeTextBox.Text,
                    Region = PostalCodeTextBox1.Text,
                    LastUpdated = DateTime.Parse(PostalCodeTextBox2.Text)
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
            var selectedItem = DistrictDataGrid.SelectedItem as SanXuatThucAnModel;

            if (selectedItem != null)
            {
                selectedItem.Name = ProvinceTextBox.Text;
                selectedItem.ProductType = PostalCodeTextBox.Text;
                selectedItem.Region = PostalCodeTextBox1.Text;
                selectedItem.LastUpdated = DateTime.Parse(PostalCodeTextBox2.Text);

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
            var selectedItem = DistrictDataGrid.SelectedItem as SanXuatThucAnModel;

            if (selectedItem != null)
            {
                _dataList.Remove(selectedItem);

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
    public class SanXuatThucAnModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public string Region { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
