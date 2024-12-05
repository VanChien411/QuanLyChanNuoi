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
    /// Interaction logic for SpecialistWindow.xaml
    /// </summary>
    public partial class SpecialistWindow : Window
    {
        public SpecialistWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QuanLyToChucCaNhanSanXuatGVN quanLyToChucCaNhanSanXuatGVN = new QuanLyToChucCaNhanSanXuatGVN();
            quanLyToChucCaNhanSanXuatGVN.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            QuanLySanXuatTinhPhoi quanLySanXuatTinhPhoi = new QuanLySanXuatTinhPhoi();
            quanLySanXuatTinhPhoi.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SoHuuGiongVatNuoi soHuuGiongVatNuoi = new SoHuuGiongVatNuoi();
            soHuuGiongVatNuoi.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CoSoKhaoNghiem coSoKhaoNghiem = new CoSoKhaoNghiem();
            coSoKhaoNghiem.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            GiongVatNuoiCanBaoTon giongVatNuoiCanBaoTon = new GiongVatNuoiCanBaoTon();
            giongVatNuoiCanBaoTon.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ThuThapNguonGen thuThapNguonGen = new ThuThapNguonGen();
            thuThapNguonGen.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            BaoTonNguonGen baoTonNguonGen = new BaoTonNguonGen();
            baoTonNguonGen.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            KhaiThacPhatTrienNguonGen khaiThacPhatTrienNguonGen = new KhaiThacPhatTrienNguonGen();
            khaiThacPhatTrienNguonGen.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            SanXuatThucAn sanXuatThucAn = new SanXuatThucAn(); 
            sanXuatThucAn.Show();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
           CoSoBanThucAn coSoBanThucAn = new CoSoBanThucAn();
           coSoBanThucAn.Show();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            HoaChatCam hoaChatCam = new HoaChatCam();
            hoaChatCam.Show();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            NguyenLieuDuocPhepSuDung nguyenLieuDuocPhepSuDung = new NguyenLieuDuocPhepSuDung();
            nguyenLieuDuocPhepSuDung.Show();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            MuaBanConGiong muaBanConGiong = new MuaBanConGiong();
            muaBanConGiong.Show();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            GiongVatNuoiCamXuatKhau giongVatNuoiCamXuatKhau = new GiongVatNuoiCamXuatKhau();
            giongVatNuoiCamXuatKhau.Show();
        }
    }
}
