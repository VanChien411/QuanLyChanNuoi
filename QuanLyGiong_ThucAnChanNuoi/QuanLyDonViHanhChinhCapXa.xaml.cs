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
    /// <summary>
    /// Interaction logic for QuanLyDonViHanhChinhCapXa.xaml
    /// </summary>
    public partial class QuanLyDonViHanhChinhCapXa : Window
    {
        // Danh sách dữ liệu mẫu
        private List<District1> _districts;

        public QuanLyDonViHanhChinhCapXa()
        {
            InitializeComponent();
            LoadData();
        }
        // Tải dữ liệu mẫu
        private void LoadData()
        {
            _districts = new List<District1>
            {
                new District1 { Id = 1, Name = "Xã A", Province = "Huyện X" },
                new District1 { Id = 2, Name = "Xã B", Province = "Huyện Y" },
                new District1 { Id = 3, Name = "Xã C", Province = "Huyện Z" }
            };

            // Hiển thị dữ liệu lên DataGrid
            DistrictDataGrid.ItemsSource = _districts;
        }
        // Xử lý sự kiện Tìm kiếm
        private void SearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Ẩn placeholder nếu có dữ liệu trong TextBox
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(SearchTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();

            // Lọc danh sách theo từ khóa
            var filteredList = _districts.Where(d =>
                d.Name.ToLower().Contains(keyword) ||
                d.Province.ToLower().Contains(keyword)).ToList();

            // Cập nhật DataGrid
            DistrictDataGrid.ItemsSource = filteredList;

            if (filteredList.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả nào phù hợp.");
            }
        }

        // Các phương thức thêm, sửa, xóa
        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thêm mới đơn vị hành chính thành công!");
        }

        private void EditDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cập nhật thông tin đơn vị hành chính thành công!");
        }

        private void DeleteDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Xóa đơn vị hành chính thành công!");
        }

       
    }
    // Định nghĩa lớp District
    public class District1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
    }

}
