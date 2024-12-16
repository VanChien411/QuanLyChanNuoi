using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using QuanLyGiong_ThucAnChanNuoi.ViewModel;
namespace QuanLyGiong_ThucAnChanNuoi
{
    /// <summary>
    /// Interaction logic for KhaiThacPhatTrienNguonGen.xaml
    /// </summary>
    public partial class KhaiThacPhatTrienNguonGen : Window
    {// Lớp mô hình dữ liệu
        public class AnimalOwner
        {
            public int Index { get; set; } // ID
            public string BreedName { get; set; } // Tên tổ chức/cá nhân
            public string BreedType { get; set; } // Địa chỉ
            public string ExportBanStatus { get; set; } // Loại giống vật nuôi
            public string ExportBanDate { get; set; } // Trạng thái
        }
        // Danh sách dữ liệu
        private ObservableCollection<AnimalOwner> _animalOwners;

        public KhaiThacPhatTrienNguonGen()
        {
            InitializeComponent();
            DataContext = new KhaiThacGenViewModel();
            //LoadData();
        }

        // Tải dữ liệu mẫu ban đầu
        private void LoadData()
        {
            //_animalOwners = new ObservableCollection<AnimalOwner>
            //{
            //    new AnimalOwner { Index = 1, BreedName = "Công ty ABC", BreedType = "Lợn", ExportBanStatus = "Miền Bắc", ExportBanDate = "01/01/2024" },
            //    new AnimalOwner { Index = 2, BreedName = "Hộ gia đình XYZ", BreedType = "gà", ExportBanStatus = "Miền Trung", ExportBanDate = "" }
            //};

            //DistrictDataGrid.ItemsSource = _animalOwners;
        }

        // Xử lý tìm kiếm
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //string keyword = SearchTextBox.Text.ToLower();
            //var filteredList = _animalOwners.Where(owner =>
            //    owner.BreedName.ToLower().Contains(keyword) ||
            //    owner.BreedType.ToLower().Contains(keyword) ||
            //    owner.ExportBanStatus.ToLower().Contains(keyword) ||
            //    owner.ExportBanDate.ToLower().Contains(keyword)).ToList();

            //DistrictDataGrid.ItemsSource = new ObservableCollection<AnimalOwner>(filteredList);
        }

        // Hiển thị hoặc ẩn placeholder
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(SearchTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        // Thêm mới tổ chức/cá nhân
        private void AddDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            //if (int.TryParse(DistrictNameTextBox.Text, out int id))
            //{
            //    _animalOwners.Add(new AnimalOwner
            //    {
            //        Index = id,
            //        BreedName = ProvinceTextBox.Text,
            //        BreedType = PostalCodeTextBox.Text,
            //        ExportBanStatus = PostalCodeTextBox1.Text,
            //        ExportBanDate = PostalCodeTextBox2.Text
            //    });
            //    MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //    ClearInputFields();
            //}
            //else
            //{
            //    MessageBox.Show("ID phải là số hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        // Sửa thông tin tổ chức/cá nhân
        private void EditDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            //if (DistrictDataGrid.SelectedItem is AnimalOwner selectedOwner)
            //{
            //    selectedOwner.BreedName = ProvinceTextBox.Text;
            //    selectedOwner.BreedType = PostalCodeTextBox.Text;
            //    selectedOwner.ExportBanStatus = PostalCodeTextBox1.Text;
            //    selectedOwner.ExportBanDate = PostalCodeTextBox2.Text;

            //    DistrictDataGrid.Items.Refresh();
            //    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //    ClearInputFields();
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn một mục trong danh sách để sửa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        // Xóa tổ chức/cá nhân
        private void DeleteDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            //if (DistrictDataGrid.SelectedItem is AnimalOwner selectedOwner)
            //{
            //    _animalOwners.Remove(selectedOwner);
            //    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //    ClearInputFields();
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn một mục trong danh sách để xóa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        // Xóa nội dung các trường nhập liệu
        private void ClearInputFields()
        {
            //DistrictNameTextBox.Clear();
            //ProvinceTextBox.Clear();
            //PostalCodeTextBox.Clear();
            //PostalCodeTextBox1.Clear();
            //PostalCodeTextBox2.Clear();
        }
    }


}
