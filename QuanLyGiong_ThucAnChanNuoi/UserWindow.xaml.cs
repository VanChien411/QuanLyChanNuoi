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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }
       

        // Tra cứu dữ liệu - Giống vật nuôi
       private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
{
    PlaceholderText.Visibility = string.IsNullOrWhiteSpace(SearchTextBox.Text)
        ? Visibility.Visible
        : Visibility.Collapsed;
}
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy từ khóa từ TextBox
            string keyword = SearchTextBox.Text;

            // Kiểm tra từ khóa và thực hiện hành động
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show($"Từ khóa tìm kiếm: {keyword}");
                // Thêm logic tìm kiếm tại đây
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa để tìm kiếm.");
            }
        }



        // Tra cứu dữ liệu - Nguồn gen giống vật nuôi

        private void SearchGeneticResources_Click(object sender, RoutedEventArgs e)
        {
            // Lấy từ khóa từ TextBox
            string keyword = SearchTextBox.Text;
            // Kiểm tra từ khóa và thực hiện hành động
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show($"Từ khóa tìm kiếm: {keyword}");
                // Thêm logic tìm kiếm tại đây
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa để tìm kiếm.");
            }
        }





        // Tra cứu dữ liệu - Thức ăn chăn nuôi
        private void SearchFeedProducers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tìm kiếm cơ sở sản xuất và mua bán thức ăn chăn nuôi thương mại.");
            // Thực hiện chức năng tìm kiếm cơ sở sản xuất thức ăn chăn nuôi tại đây
        }

    }
}
