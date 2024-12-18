
using System.Windows;
using System.Windows.Controls;
using QuanLyGiong_ThucAnChanNuoi.ViewModel;


namespace QuanLyGiong_ThucAnChanNuoi
{
    public partial class QuanLyLichSuTruyCap : Window
    {
      
        public QuanLyLichSuTruyCap()
        {
            InitializeComponent();
            DataContext = new QuanLyLichSuViewModel();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrWhiteSpace(SearchTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
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

