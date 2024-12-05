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
    /// Interaction logic for ManHinhChinh.xaml
    /// </summary>
    public partial class ManHinhChinh : Window
    {
        public ManHinhChinh()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private void BtnSystemManagement_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Quản trị hệ thống được chọn.");
            // Thêm logic xử lý tại đây
        }

        private void BtnAnimalBreeds_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Giống vật nuôi được chọn.");
            // Thêm logic xử lý tại đây
        }
        private void BtnFeeds_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thức ăn chăn nuôi được chọn.");
            // Thêm logic xử lý tại đây
        }
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Đăng xuất thành công.");
            // Thêm logic đăng xuất tại đây
            this.Close();
        }
    }
}
