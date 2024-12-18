using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using QuanLyGiong_ThucAnChanNuoi.ViewModel;

namespace QuanLyGiong_ThucAnChanNuoi
{
    /// <summary>
    /// Interaction logic for MuaBanConGiong.xaml
    /// </summary>
    public partial class MuaBanConGiong : Window
    {
        public MuaBanConGiong()
        {
            InitializeComponent();
            DataContext = new MuaBanConGiongViewModel();
        }

    
        // Hiển thị hoặc ẩn placeholder
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(SearchTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

     
    }


}
