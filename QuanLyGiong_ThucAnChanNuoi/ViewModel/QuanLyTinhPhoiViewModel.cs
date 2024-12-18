using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGiong_ThucAnChanNuoi.ViewModel
{
     internal class QuanLyTinhPhoiViewModel : INotifyPropertyChanged
    {
        private int _newId;
        private string _newFullName;
        private string _newAddress;
        private string _newPhone;
        private string _newEmail;
        private string _newType;
        private string _newActiveType = "Sản xuất tinh, phôi, ấu trùng";
        private string _newTextSearch;

        private ToChucCaNhan _rowSelectedItem;
        private ChucVu _chucVuSelectedItem;



        public ObservableCollection<ToChucCaNhan> ToChucCaNhans { get; set; } = new ObservableCollection<ToChucCaNhan>();



        public string NewTextSearch
        {
            get => _newTextSearch;
            set
            {
                _newTextSearch = value;
                OnPropertyChanged(nameof(NewTextSearch)); // Notify UI to update binding
            }
        }
        public string NewPhone
        {
            get => _newPhone;
            set
            {
                _newPhone = value;
                OnPropertyChanged(nameof(NewPhone)); // Notify UI to update binding
            }
        }

        public ToChucCaNhan RowSelectedItem
        {
            get => _rowSelectedItem;
            set
            {
                _rowSelectedItem = value;
                OnPropertyChanged(nameof(RowSelectedItem));
            }
        }




        public int NewId
        {
            get => _newId;
            set
            {
                _newId = value;
                OnPropertyChanged(nameof(NewId));
            }
        }
        public string NewFullName
        {
            get => _newFullName;
            set
            {
                _newFullName = value;
                OnPropertyChanged(nameof(NewFullName));
            }
        }
        public string NewAddress
        {
            get => _newAddress;
            set
            {
                _newAddress = value;
                OnPropertyChanged(nameof(NewAddress));
            }
        }
        public string NewEmail
        {
            get => _newEmail;
            set
            {
                _newEmail = value;
                OnPropertyChanged(nameof(NewEmail));
            }
        }
        public string NewType
        {
            get => _newType;
            set
            {
                _newType = value;
                OnPropertyChanged(nameof(NewType));
            }
        }
        public string NewActiveType
        {
            get => _newActiveType;
            set
            {
                _newActiveType = value;
                OnPropertyChanged(nameof(NewActiveType));
            }
        }
        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand RowSelectedCommand { get; set; }
        public ICommand ChucVuSelectedCommand { get; set; }


        public QuanLyTinhPhoiViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<ToChucCaNhan>(OnRowSelected);



        }
        private void Initialize()
        {
            try
            {
                LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo: {ex.Message}");
            }
        }


        private void LoadAsync()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var toChucCaNhans = GetToChucCaNhans();
                    //Gan gia tri cho table list
                    ToChucCaNhans = new ObservableCollection<ToChucCaNhan>(toChucCaNhans);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private void OnRowSelected(ToChucCaNhan selectedItem)
        {
            if (selectedItem != null)
            {

                //RowSelectedItem = selectedItem;
                // Thực hiện hành động với SelectedItem
                NewFullName = selectedItem.Ten;
                NewAddress = selectedItem.DiaChi;
                NewPhone = selectedItem.SoDienThoai;
                NewType = selectedItem.LoaiHinh;


            }

        }
        private void OnChucVuSelected(ChucVu selectedItem)
        {

        }

        private List<ToChucCaNhan> GetToChucCaNhans()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var ToChucCaNhans = db.ToChucCaNhans.AsNoTracking().Where(x => x.LoaiHoatDong.Equals(NewActiveType)).ToList();
                    return ToChucCaNhans;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
            return null;
        }
        private async void Search()
        {
            try
            {

                var textSearch = NewTextSearch?.ToLower() ?? "";
                // Sử dụng && để kiểm tra x.MaBuuDien != null trước khi gọi ToLower().Contains().

                var toChucCaNhans = GetToChucCaNhans()
                  .Where(x =>
                      (x.Ten?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Email?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                      || (x.SoDienThoai?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.LoaiHinh?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.LoaiHoatDong?.ToLower().Contains(textSearch.ToLower()) ?? false))
                  .ToList();


                LoadTableList(toChucCaNhans);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}");
            }
        }
        private async void AddItem()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {
                    if (!string.IsNullOrEmpty(NewFullName))
                    {
                        var toChucCaNhan = new ToChucCaNhan { Ten = NewFullName, DiaChi = NewAddress, Email = NewEmail, SoDienThoai = NewPhone, LoaiHinh = NewType, LoaiHoatDong = NewActiveType };
                        db.ToChucCaNhans.Add(toChucCaNhan);
                        db.SaveChanges();

                        var ToChucCaNhans = GetToChucCaNhans();
                        LoadTableList(ToChucCaNhans);

                        NewFullName = "";
                        NewAddress = "";
                        NewPhone = "";
                        NewType = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}");
            }
        }
        private async void EditItem()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {
                    if (RowSelectedItem != null)
                    {
                        var toChucCaNhan = await db.ToChucCaNhans.FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                        toChucCaNhan.Ten = NewFullName;
                        toChucCaNhan.DiaChi = NewAddress;
                        toChucCaNhan.SoDienThoai = NewPhone;
                        toChucCaNhan.LoaiHinh = NewType;
                        toChucCaNhan.LoaiHoatDong = RowSelectedItem.LoaiHoatDong;

                        await db.SaveChangesAsync();

                        var toChucCaNhans = GetToChucCaNhans();
                        LoadTableList(toChucCaNhans);
                        RowSelectedItem = ToChucCaNhans.FirstOrDefault(x => x.Id == toChucCaNhan.Id);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa: {ex.Message}");
            }
        }
        private async void DeleteItem()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {
                    if (RowSelectedItem != null)
                    {
                        var toChucCaNhan = await db.ToChucCaNhans.FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                        db.Remove(toChucCaNhan);

                        db.SaveChanges();

                        var ToChucCaNhans = GetToChucCaNhans();
                        LoadTableList(ToChucCaNhans);

                        NewFullName = "";
                        NewEmail = "";
                        NewAddress = "";
                        NewPhone = "";
                        NewType = "";

                    }
                    else
                    {
                        MessageBox.Show("Chọn dữ liệu muốn xóa trong bảng");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}");
            }
        }
        private void LoadTableList(List<ToChucCaNhan> toChucCaNhans)
        {
            ToChucCaNhans.Clear();
            foreach (var item in toChucCaNhans)
            {
                ToChucCaNhans.Add(item);
            }
        }


        // Hàm xử lý sự kiện khi selection thay đổi




        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
