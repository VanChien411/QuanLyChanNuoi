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

using System.Collections.ObjectModel;


namespace QuanLyGiong_ThucAnChanNuoi
{
    public partial class SoHuuGiongVatNuoi : Window
    {
        // Lớp mô hình dữ liệu
        public class AnimalOwner
        {
            public int Index { get; set; } // ID
            public string Name { get; set; } // Tên tổ chức/cá nhân
            public string Address { get; set; } // Địa chỉ
            public string AnimalType { get; set; } // Loại giống vật nuôi
            public string Status { get; set; } // Trạng thái
        }
        // Danh sách dữ liệu
        private ObservableCollection<AnimalOwner> _animalOwners;

        public SoHuuGiongVatNuoi()
        {
            InitializeComponent();
            LoadData();
        }

        // Tải dữ liệu mẫu ban đầu
        private void LoadData()
        {
            _animalOwners = new ObservableCollection<AnimalOwner>
            {
                new AnimalOwner { Index = 1, Name = "Công ty ABC", Address = "Hà Nội", AnimalType = "Bò", Status = "Hoạt động" },
                new AnimalOwner { Index = 2, Name = "Hộ gia đình XYZ", Address = "Đà Nẵng", AnimalType = "Heo", Status = "Ngừng hoạt động" }
            };

            DistrictDataGrid.ItemsSource = _animalOwners;
        }

        // Xử lý tìm kiếm
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();
            var filteredList = _animalOwners.Where(owner =>
                owner.Name.ToLower().Contains(keyword) ||
                owner.Address.ToLower().Contains(keyword) ||
                owner.AnimalType.ToLower().Contains(keyword) ||
                owner.Status.ToLower().Contains(keyword)).ToList();

            DistrictDataGrid.ItemsSource = new ObservableCollection<AnimalOwner>(filteredList);
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
            if (int.TryParse(DistrictNameTextBox.Text, out int id))
            {
                _animalOwners.Add(new AnimalOwner
                {
                    Index = id,
                    Name = ProvinceTextBox.Text,
                    Address = PostalCodeTextBox.Text,
                    AnimalType = PostalCodeTextBox1.Text,
                    Status = PostalCodeTextBox2.Text
                });
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("ID phải là số hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sửa thông tin tổ chức/cá nhân
        private void EditDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            if (DistrictDataGrid.SelectedItem is AnimalOwner selectedOwner)
            {
                selectedOwner.Name = ProvinceTextBox.Text;
                selectedOwner.Address = PostalCodeTextBox.Text;
                selectedOwner.AnimalType = PostalCodeTextBox1.Text;
                selectedOwner.Status = PostalCodeTextBox2.Text;

                DistrictDataGrid.Items.Refresh();
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục trong danh sách để sửa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xóa tổ chức/cá nhân
        private void DeleteDistrictButton_Click(object sender, RoutedEventArgs e)
        {
            if (DistrictDataGrid.SelectedItem is AnimalOwner selectedOwner)
            {
                _animalOwners.Remove(selectedOwner);
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục trong danh sách để xóa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xóa nội dung các trường nhập liệu
        private void ClearInputFields()
        {
            DistrictNameTextBox.Clear();
            ProvinceTextBox.Clear();
            PostalCodeTextBox.Clear();
            PostalCodeTextBox1.Clear();
            PostalCodeTextBox2.Clear();
        }
    }

  
}

