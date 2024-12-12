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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QuanLyGiong_ThucAnChanNuoi.ViewModel
{
    class QuanLyNguoiDungViewModel : INotifyPropertyChanged
    {
        public class Status
        {
            public bool StatusValue { get; set; }
            public string Name => StatusValue ? "Hoạt động" : "Khóa";

            public Status(bool statusValue = false) // Giá trị mặc định là false
            {
                StatusValue = statusValue;
            }
        }
      
        private int _newId;
        private string _newFullName;
        private string _newEmail;
        private string _newTextSearch;

        private NguoiDung _rowSelectedItem;
        private ChucVu _chucVuSelectedItem;
        private Status _statusSelectedItem;


        public ObservableCollection<NguoiDung> NguoiDungs { get; set; } = new ObservableCollection<NguoiDung>();
        public ObservableCollection<ChucVu> ChucVus { get; set; } = new ObservableCollection<ChucVu>();
        public ObservableCollection<Status> TrangThais { get; set; } = new ObservableCollection<Status>();

        public string NewTextSearch
        {
            get => _newTextSearch;
            set
            {
                _newTextSearch = value;
                OnPropertyChanged(nameof(NewTextSearch)); // Notify UI to update binding
            }
        }
   
        public NguoiDung RowSelectedItem
        {
            get => _rowSelectedItem;
            set
            {
                _rowSelectedItem = value;
                OnPropertyChanged(nameof(RowSelectedItem));
            }
        }
 
        public ChucVu ChucVuSelectedItem
        {
            get => _chucVuSelectedItem;
            set
            {
                if (_chucVuSelectedItem != value) // Chỉ thay đổi khi giá trị khác
                {
                    _chucVuSelectedItem = value;
                    OnPropertyChanged(nameof(ChucVuSelectedItem)); // Thông báo UI
                }
            }
        }
   
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
            if(selectedItem != null)
            {
                RowSelectedItem = selectedItem;
                // Thực hiện hành động với SelectedItem
                NewId = selectedItem.Id;
                NewFullName = selectedItem.HoTen;
                NewEmail = selectedItem.Email;
     
                ChucVuSelectedItem = ChucVus.FirstOrDefault(c => c.Id == selectedItem?.ChucVu?.Id);
                StatusSelectedItem = TrangThais.FirstOrDefault(c => c.StatusValue == selectedItem?.TrangThai);
            }
          
        }
        private void OnChucVuSelected(ChucVu selectedItem)
        {
           
        }
       
        private void OnStatusSelected(Status selectedItem)
        {
            

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

                var textSearch = NewTextSearch?.ToLower()?? "";
                // Sử dụng && để kiểm tra x.MaBuuDien != null trước khi gọi ToLower().Contains().

                var nguoiDungs = GetNguoiDungs()
                  .Where(x =>
                      (x.HoTen?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Email?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()) )
                      || (x.TrangThaiHienThi?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.ChucVu?.TenChucVu?.ToLower().Contains(textSearch.ToLower()) ?? false))
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
                    if(RowSelectedItem != null)
                    {
                        var nguoiDung = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                        if(nguoiDung != null)
                        {
                            nguoiDung.HoTen = NewFullName;
                            nguoiDung.ChucVuId = ChucVuSelectedItem?.Id;
                            nguoiDung.TrangThai = StatusSelectedItem?.StatusValue;
                            nguoiDung.Email = NewEmail;
                            await db.SaveChangesAsync();

                            var nguoiDungs = GetNguoiDungs();
                            LoadTableList(nguoiDungs);
                            RowSelectedItem = NguoiDungs.FirstOrDefault(x => x.Id == nguoiDung.Id);

                         
                        }
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
                    if(RowSelectedItem != null)
                    {
                        // Xóa phân quyền liên quan
                        //var phanQuyens = await db.PhanQuyenNguoiDungs
                        //    .Where(x => x.NguoiDungId == RowSelectedItem.Id)
                        //    .ToListAsync();

                        //if (phanQuyens.Any())
                        //{
                        //    db.PhanQuyenNguoiDungs.RemoveRange(phanQuyens);
                        //}

                        // Xóa người dùng
                        var nguoiDung = await db.NguoiDungs.Include(c => c.PhanQuyenNguoiDungs).FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                        if (nguoiDung != null)
                        {
                            // Xóa phân quyền liên quan
                        
                            if (nguoiDung.PhanQuyenNguoiDungs.Any())
                            {
                                db.PhanQuyenNguoiDungs.RemoveRange(nguoiDung.PhanQuyenNguoiDungs);
                            }

                            db.NguoiDungs.Remove(nguoiDung);
                        }

                        // Lưu thay đổi
                        await db.SaveChangesAsync();

                  

                        var nguoiDungs = GetNguoiDungs();
                        LoadTableList(nguoiDungs);

                        NewId = 0;
                        NewFullName = "";
                        NewEmail = "";

                        ChucVuSelectedItem = null;
                        StatusSelectedItem = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi : {ex.Message}");
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
