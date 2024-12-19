using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using QuanLyGiong_ThucAnChanNuoi.ViewModel;

namespace QuanLyGiong_ThucAnChanNuoi
{
    public partial class NguyenLieuDuocPhepSuDung : Window
    {
      
        public NguyenLieuDuocPhepSuDung()
        {
            InitializeComponent();
            DataContext = new NuyenLieuDuocSuDungViewModel();
 
        }

    
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrWhiteSpace(SearchTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
        }
    }

    // Model dữ liệu
  
}
