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
    public partial class QuanLyLichSuTruyCap : Window
    {
        // Danh sách lịch sử truy cập (giả lập)
        private List<AccessHistory> _historyData = new List<AccessHistory>
        {
            new AccessHistory { Id = 1, UserName = "Nguyễn Văn A", Action = "Đăng nhập", Time = DateTime.Now.AddHours(-2).ToString("dd/MM/yyyy HH:mm"), IpAddress = "192.168.1.1" },
            new AccessHistory { Id = 2, UserName = "Trần Thị B", Action = "Sửa dữ liệu", Time = DateTime.Now.AddHours(-1).ToString("dd/MM/yyyy HH:mm"), IpAddress = "192.168.1.2" },
            new AccessHistory { Id = 3, UserName = "Lê Văn C", Action = "Xóa dữ liệu", Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm"), IpAddress = "192.168.1.3" }
        };

        public QuanLyLichSuTruyCap()
        {
            InitializeComponent();
            LoadHistoryData();
        }

        // Hiển thị dữ liệu trong DataGrid
        private void LoadHistoryData()
        {
            HistoryDataGrid.ItemsSource = _historyData;
        }

        // Tìm kiếm trong danh sách lịch sử
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();
            var filteredData = _historyData.Where(history =>
                history.UserName.ToLower().Contains(keyword) ||
                history.Action.ToLower().Contains(keyword) ||
                history.Time.Contains(keyword) ||
                history.IpAddress.Contains(keyword)).ToList();

            HistoryDataGrid.ItemsSource = filteredData;
        }

        // Xóa tất cả lịch sử truy cập
        private void DeleteHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ lịch sử truy cập không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _historyData.Clear();
                LoadHistoryData();
                MessageBox.Show("Đã xóa toàn bộ lịch sử!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Xuất báo cáo lịch sử truy cập
        private void ExportReportButton_Click(object sender, RoutedEventArgs e)
        {
            // Giả lập xuất báo cáo (xuất ra file CSV hoặc Excel)
            MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    // Mô hình dữ liệu lịch sử truy cập
    public class AccessHistory
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Time { get; set; }
        public string IpAddress { get; set; }
    }
}

