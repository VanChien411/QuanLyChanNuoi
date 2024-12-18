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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QuanLyDonViHanhChinhCapHuyen quanLyDonViHanhChinhCapHuyen = new QuanLyDonViHanhChinhCapHuyen();
            quanLyDonViHanhChinhCapHuyen.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SanXuatConGiong sanXuatConGiong = new SanXuatConGiong();
            sanXuatConGiong.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GiongVatNuoiCanBaoTon giongVatNuoiCanBaoTon = new GiongVatNuoiCanBaoTon ();
            giongVatNuoiCanBaoTon.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SanXuatTinhPhoiAutrung sanXuatTinhPhoi = new SanXuatTinhPhoiAutrung();
            sanXuatTinhPhoi.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            QuanLyDonViHanhChinhCapXa quanLyDonViHanhChinhCapXa = new QuanLyDonViHanhChinhCapXa ();
            quanLyDonViHanhChinhCapXa.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            QuanLyNguoiDung quanLyNguoiDung = new QuanLyNguoiDung ();
            quanLyNguoiDung.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            PhanQuyenNguoiDung phanQuyenNguoiDung = new PhanQuyenNguoiDung();
            phanQuyenNguoiDung.Show ();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            QuanLyLichSuTruyCap quanLyLichSuTruyCap = new QuanLyLichSuTruyCap ();
            quanLyLichSuTruyCap.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            QuanLyLichSuTacDongHeThong quanLyLichSuTacDongHeThong = new QuanLyLichSuTacDongHeThong();
            quanLyLichSuTacDongHeThong.Show();  
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow ();
            mainWindow.Show ();
            this.Close ();
        }
    }
}
