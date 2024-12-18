using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using QuanLyGiong_ThucAnChanNuoi.ViewModel;

using System.Collections.ObjectModel;


namespace QuanLyGiong_ThucAnChanNuoi
{
    public partial class SoHuuGiongVatNuoi : Window
    {

        public SoHuuGiongVatNuoi()
        {
            InitializeComponent();
            DataContext = new SoHuuGiongVNViewModel();
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

