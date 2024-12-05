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
    public partial class QuanLyLichSuTacDongHeThong : Window
    {
        public QuanLyLichSuTacDongHeThong()
        {
           InitializeComponent();

        }

        // Danh sách mẫu cho Lịch sử tác động hệ thống
        private List<HistoryRecord> historyRecords;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Khởi tạo danh sách lịch sử tác động mẫu
            historyRecords = new List<HistoryRecord>
            {
                new HistoryRecord { Id = 1, UserName = "Nguyen A", Action = "Thêm dữ liệu", Description = "Thêm người dùng", Time = DateTime.Now.AddMinutes(-5), IpAddress = "192.168.1.1" },
                new HistoryRecord { Id = 2, UserName = "Tran B", Action = "Cập nhật dữ liệu", Description = "Cập nhật thông tin", Time = DateTime.Now.AddMinutes(-10), IpAddress = "192.168.1.2" },
                new HistoryRecord { Id = 3, UserName = "Le C", Action = "Xóa dữ liệu", Description = "Xóa người dùng", Time = DateTime.Now.AddMinutes(-15), IpAddress = "192.168.1.3" }
            };

            // Hiển thị danh sách dữ liệu lên DataGrid
            HistoryDataGrid.ItemsSource = historyRecords;
            UpdateRecordCount();
        }

        // Cập nhật số lượng bản ghi hiển thị
        private void UpdateRecordCount()
        {
            RecordCountTextBlock.Text = $"Tổng số bản ghi: {historyRecords.Count}";
        }

        // Tìm kiếm trong danh sách lịch sử tác động
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text.ToLower();
            var filteredRecords = historyRecords.Where(r =>
                r.UserName.ToLower().Contains(searchQuery) ||
                r.Action.ToLower().Contains(searchQuery) ||
                r.Description.ToLower().Contains(searchQuery)).ToList();

            HistoryDataGrid.ItemsSource = filteredRecords;
            UpdateRecordCount();
        }

        // Lọc dữ liệu theo thời gian và hành động
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            string selectedAction = (ActionFilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            var filteredRecords = historyRecords.AsEnumerable();

            if (startDate.HasValue)
            {
                filteredRecords = filteredRecords.Where(r => r.Time >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                filteredRecords = filteredRecords.Where(r => r.Time <= endDate.Value);
            }

            if (selectedAction != "Tất cả")
            {
                filteredRecords = filteredRecords.Where(r => r.Action == selectedAction);
            }

            HistoryDataGrid.ItemsSource = filteredRecords.ToList();
            UpdateRecordCount();
        }

        // Xóa tất cả bản ghi
        private void DeleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            historyRecords.Clear();
            HistoryDataGrid.ItemsSource = historyRecords;
            UpdateRecordCount();
        }

        // Xuất báo cáo (ví dụ xuất ra console)
        private void ExportReportButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            // Cái này có thể làm để xuất ra file Excel, CSV, hoặc PDF tùy yêu cầu.
        }
    }

    // Lớp mô tả dữ liệu lịch sử tác động
    public class HistoryRecord
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public string IpAddress { get; set; }
    }
}

