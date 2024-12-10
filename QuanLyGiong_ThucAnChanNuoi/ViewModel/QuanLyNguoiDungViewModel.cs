using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGiong_ThucAnChanNuoi.ViewModel
{
    class QuanLyNguoiDungViewModel : INotifyPropertyChanged
    {
        public class Status
        {
            public bool StatusValue { get; set; }
            public string Name => StatusValue ? "Mở" : "Khóa";

            public Status(bool statusValue = false) // Giá trị mặc định là false
            {
                StatusValue = statusValue;
            }
        }
        private string _newDistrictName;
        private int _newId;
        private string _newFullName;
        private string _newEmail;
        private string _newTextSearch;

        //private string _textComboBox;
        //private NguoiDung _selectedItem;
        //private NguoiDung _districtSelected;

        public ObservableCollection<NguoiDung> NguoiDungs { get; set; } = new ObservableCollection<NguoiDung>();
        public ObservableCollection<ChucVu> ChucVus { get; set; } = new ObservableCollection<ChucVu>();
        public ObservableCollection<Status> TrangThais { get; set; } = new ObservableCollection<Status>();

        public class NguoiDungInterface : NguoiDung
        {
          
            public string TrangThaiHienThi => TrangThai.HasValue && TrangThai.Value ? "Mở" : "Khóa";
        }
        public string NewTextSearch
        {
            get => _newTextSearch;
            set
            {
                _newTextSearch = value;
                OnPropertyChanged(nameof(NewTextSearch)); // Notify UI to update binding
            }
        }
        private NguoiDung _rowSelectedItem;
        public NguoiDung RowSelectedItem
        {
            get => _rowSelectedItem;
            set
            {
                _rowSelectedItem = value;
                OnPropertyChanged(nameof(RowSelectedItem));
            }
        }
        private ChucVu _chucVuSelectedItem;
        public ChucVu ChucVuSelectedItem
        {
            get => _chucVuSelectedItem;
            set
            {
                _chucVuSelectedItem = value;
                OnPropertyChanged(nameof(ChucVuSelectedItem));
            }
        }
        private Status _statusSelectedItem;
        public Status StatusSelectedItem
        {
            get => _statusSelectedItem;
            set
            {
                _statusSelectedItem = value;
                OnPropertyChanged(nameof(StatusSelectedItem));
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
        public string NewEmail
        {
            get => _newEmail;
            set
            {
                _newEmail = value;
                OnPropertyChanged(nameof(NewEmail));
            }
        }
        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand DistrictSelectionChangedCommand { get; }
        public ICommand SelectionChangedCommand { get; }
        public ICommand RowSelectedCommand { get; set; }
        public ICommand ChucVuSelectedCommand { get; set; }
        public ICommand StatusSelectedCommand { get; set; }


        public QuanLyNguoiDungViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<NguoiDung>(OnRowSelected);
            ChucVuSelectedCommand = new RelayCommandT<ChucVu>(OnChucVuSelected);
            StatusSelectedCommand = new RelayCommandT<Status>(OnStatusSelected);

            //DistrictSelectionChangedCommand = new RelayCommandT<object>(OnDistrictSelectionChanged);
            //SelectionChangedCommand = new RelayCommandT<object>(OnSelectionChanged);
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
                    var nguoiDungs = GetNguoiDungs();
                    //Gan gia tri cho table list
                    NguoiDungs = new ObservableCollection<NguoiDung>(nguoiDungs);

                    var chucVus = db.ChucVus.ToList();
                    ChucVus = new ObservableCollection<ChucVu>(chucVus);

                    var trangThais = new List<Status> { new Status(true)  , new Status(false) };
                    TrangThais = new ObservableCollection<Status> (trangThais);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private void OnRowSelected(NguoiDung selectedItem)
        {
            RowSelectedItem = selectedItem;
            // Thực hiện hành động với SelectedItem
        }
        private void OnChucVuSelected(ChucVu selectedItem)
        {
            ChucVuSelectedItem = selectedItem;
            // Thực hiện hành động với SelectedItem
        }
       
        private void OnStatusSelected(Status selectedItem)
        {
            StatusSelectedItem = selectedItem;
            // Thực hiện hành động với SelectedItem
    
        }
        private List<NguoiDung> GetNguoiDungs()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var nguoiDungs = db.NguoiDungs.AsNoTracking().Include(c => c.ChucVu).ToList();
                    return nguoiDungs;
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

                var textSearch = NewTextSearch.ToLower();
                // Sử dụng && để kiểm tra x.MaBuuDien != null trước khi gọi ToLower().Contains().

                var nguoiDungs = GetNguoiDungs()
                    .Where(x => x.HoTen.ToLower().Contains(textSearch)
                        || ( x.Email.ToLower().Contains(textSearch))
                        || (x.Id.ToString().ToLower().Contains(textSearch))
                        || (x.TrangThaiHienThi.ToLower().Contains(textSearch))
                        || (x.ChucVu != null && x.ChucVu.TenChucVu.ToLower().Contains(textSearch)))
                    .ToList();

                LoadTableList(nguoiDungs);


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
                //using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                //{
                   
                //    int? idTrucThuoc = null;
                //    if (DistrictSelected != null)
                //    {
                //        MessageBox.Show($"Huyện tồn tại [ sửa hoặc xóa]");
                //        return;
                //    }
                //    if (SelectedItem != null)
                //    {
                //        idTrucThuoc = SelectedItem.Id;
                //    }
                //    else if (string.IsNullOrEmpty(_textComboBox) != true)
                //    {
                //        var newTrucThuoc = new NguoiDung
                //        {
                //            Ten = _textComboBox,
                //            CapHcId = _idTinh
                //        };

                //        // Thêm vào DbContext
                //        await db.AddAsync(newTrucThuoc);

                //        // Lưu thay đổi vào cơ sở dữ liệu
                //        await db.SaveChangesAsync();

                //        // Lấy ID của thực thể mới
                //        idTrucThuoc = newTrucThuoc.Id;
                //    }
                //    if (!string.IsNullOrEmpty(NewDistrictName))
                //    {
                //        var NguoiDung = new NguoiDung { Ten = NewDistrictName, TrucThuoc = idTrucThuoc, CapHcId = _idHuyen, MaBuuDien = NewMaBuuDien };
                //        await db.AddAsync(NguoiDung);
                //        await db.SaveChangesAsync();

                //        // Reset list
                //        var NguoiDungs = GetNguoiDungs();

                //        LoadTableList(NguoiDungs);
                //        LoadSelect();

                //        NewDistrictName = string.Empty;
                //        Text = string.Empty;
                //        NewMaBuuDien = string.Empty;

                //    }

                //}
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
                //using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                //{

                //    if (DistrictSelected != null)
                //    {

                //        //Kiểm tra tỉnh có chưa
                //        int? idTrucThuoc = null;
                //        if (SelectedItem != null)
                //        {
                //            idTrucThuoc = SelectedItem.Id;
                //        }
                //        else if (string.IsNullOrEmpty(_textComboBox) != true)
                //        {
                //            var newTrucThuoc = new NguoiDung
                //            {
                //                Ten = _textComboBox,
                //                CapHcId = _idTinh
                //            };

                //            // Thêm vào DbContext
                //            await db.AddAsync(newTrucThuoc);

                //            // Lưu thay đổi vào cơ sở dữ liệu
                //            await db.SaveChangesAsync();

                //            // Lấy ID của thực thể mới
                //            idTrucThuoc = newTrucThuoc.Id;
                //        }

                //        // Sửa thông tin huyện
                //        var NguoiDung = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Id == DistrictSelected.Id);

                //        if (NguoiDung != null)
                //        {

                //            NguoiDung.TrucThuoc = idTrucThuoc;
                //            NguoiDung.MaBuuDien = NewMaBuuDien;

                //            // Lưu thay đổi vào cơ sở dữ liệu
                //            await db.SaveChangesAsync();

                //            // Reset list
                //            var NguoiDungs = GetNguoiDungs();
                //            LoadTableList(NguoiDungs);
                //            LoadSelect();

                //            NewDistrictName = string.Empty;
                //            Text = string.Empty;
                //            NewMaBuuDien = string.Empty;

                //        }

                //    }
                //}
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
                //using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                //{
                //    if (DistrictSelected != null)
                //    {
                //        var NguoiDung = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Id == DistrictSelected.Id);
                //        if (NguoiDung != null)
                //        {
                //            //Phần này ko dùng async
                //            db.Remove(NguoiDung);
                //            db.SaveChanges();

                //            // Reset list
                //            var NguoiDungs = GetNguoiDungs();
                //            LoadTableList(NguoiDungs);
                //            LoadSelect();

                //            NewDistrictName = string.Empty;
                //            Text = string.Empty;
                //            NewMaBuuDien = string.Empty;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}");
            }
        }

        private void LoadTableList(List<NguoiDung> nguoiDungs)
        {
            NguoiDungs.Clear();
            foreach (var item in nguoiDungs)
            {
                NguoiDungs.Add(item);
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
