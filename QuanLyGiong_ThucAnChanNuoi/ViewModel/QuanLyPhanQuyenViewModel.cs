using ControlzEx.Standard;
using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace QuanLyGiong_ThucAnChanNuoi.ViewModel
{
    public class QuanLyPhanQuyenViewModel : INotifyPropertyChanged
    {
        public class PhanQuyenDTO
        {
            public int? index {  get; set; }
            public NguoiDung NguoiDung { get; set; }
            public Nhom Nhom { get; set; }
            public PhanQuyen Quyen { get; set; }
            public PhanQuyenDTO()
            {

            }
            // Phương thức này sẽ trả về danh sách PhanQuyenDTO từ một danh sách NguoiDungs
            public static List<PhanQuyenDTO> PhanQuyenDTOList(List<NguoiDung> nguoiDungs)
            {
                List<PhanQuyenDTO> result = new List<PhanQuyenDTO>();

                foreach (var item in nguoiDungs)
                {
                    // Kiểm tra và lấy quyền của người dùng
                    foreach (var phanQuyenItem in item.PhanQuyenNguoiDungs)
                    {
                        var temp = new PhanQuyenDTO
                        {
                            NguoiDung = item,
                            Quyen = phanQuyenItem.PhanQuyen
                        };
                        result.Add(temp);
                    }

                    // Lấy thông tin về nhóm của người dùng nếu có
                    foreach (var thanhVienItem in item.ThanhVienNhoms)
                    {
                        var temp = new PhanQuyenDTO
                        {
                            NguoiDung = item,
                            Nhom = thanhVienItem.Nhom
                        };
                        result.Add(temp);
                    }
                }

                // Sắp xếp theo Id của người dùng
                result = result.OrderBy(x => x.NguoiDung.Id).ToList();

                // Gán index cho từng phần tử trong danh sách
                for (int i = 1; i <= result.Count; i++)
                {
                    result[i - 1].index = i;
                }

                return result;
            }

        }
        public class Status
        {
            public bool StatusValue { get; set; }
            public string Name => StatusValue ? "Mở" : "Khóa";

            public Status(bool statusValue = false) // Giá trị mặc định là false
            {
                StatusValue = statusValue;
            }
        }

        private int _newId;
        private string _newFullName;
        private string _newEmail;
        private string _newTextSearch;

        private PhanQuyenDTO _rowSelectedItem;
        private ChucVu _chucVuSelectedItem;
        private Status _statusSelectedItem;


        public ObservableCollection<PhanQuyenDTO> PhanQuyens { get; set; } = new ObservableCollection<PhanQuyenDTO>();
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

        public PhanQuyenDTO RowSelectedItem
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


        public QuanLyPhanQuyenViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<PhanQuyenDTO>(OnRowSelected);
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
                    var phanQuyens = GetPhanQuyens();
                    //Gan gia tri cho table list
                    PhanQuyens = new ObservableCollection<PhanQuyenDTO>(phanQuyens);
                
                    var chucVus = db.ChucVus.ToList();
                    ChucVus = new ObservableCollection<ChucVu>(chucVus);

                    var trangThais = new List<Status> { new Status(true), new Status(false) };
                    TrangThais = new ObservableCollection<Status>(trangThais);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private void OnRowSelected(PhanQuyenDTO selectedItem)
        {
            if (selectedItem != null)
            {
                RowSelectedItem = selectedItem;
                // Thực hiện hành động với SelectedItem
                //NewId = selectedItem.index ?? 0;
                //NewFullName = selectedItem.HoTen;
                //NewEmail = selectedItem.Email;

                //ChucVuSelectedItem = ChucVus.FirstOrDefault(c => c.Id == selectedItem?.ChucVu?.Id);
                //StatusSelectedItem = TrangThais.FirstOrDefault(c => c.StatusValue == selectedItem?.TrangThai);
            }

        }
        private void OnChucVuSelected(ChucVu selectedItem)
        {

        }

        private void OnStatusSelected(Status selectedItem)
        {


        }
        private List<PhanQuyenDTO> GetPhanQuyens()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var result = db.NguoiDungs
                        .Include(c => c.PhanQuyenNguoiDungs)
                            .ThenInclude(c => c.PhanQuyen)
                        .Include(c => c.ThanhVienNhoms)
                            .ThenInclude(c => c.Nhom)
                        .ToList();
                    return PhanQuyenDTO.PhanQuyenDTOList(result);
                   
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

                var PhanQuyens = GetPhanQuyens()
                    .Where(x =>
                        (x.NguoiDung?.TenDn?.ToLower().Contains(textSearch.ToLower()) ?? false)
                        || (x.index?.ToString().ToLower().Contains(textSearch.ToLower()) ?? false)
                        || (x.NguoiDung?.Email?.ToLower().Contains(textSearch.ToLower()) ?? false)
                        || (x.NguoiDung?.HoTen?.ToLower().Contains(textSearch.ToLower()) ?? false)
                        || (x.Nhom?.Ten?.ToLower().Contains(textSearch.ToLower()) ?? false)
                        || (x.Quyen?.TenQuyen?.ToLower().Contains(textSearch.ToLower()) ?? false))
                    .ToList();


                LoadTableList(PhanQuyens);


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
                    //if (RowSelectedItem != null)
                    //{
                    //    var PhanQuyen = await db.PhanQuyens.FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                    //    if (PhanQuyen != null)
                    //    {
                    //        PhanQuyen.HoTen = NewFullName;
                    //        PhanQuyen.ChucVuId = ChucVuSelectedItem?.Id;
                    //        PhanQuyen.TrangThai = StatusSelectedItem?.StatusValue;
                    //        PhanQuyen.Email = NewEmail;
                    //        await db.SaveChangesAsync();

                    //        var PhanQuyens = GetPhanQuyens();
                    //        LoadTableList(PhanQuyens);
                    //        RowSelectedItem = PhanQuyens.FirstOrDefault(x => x.Id == PhanQuyen.Id);


                    //    }
                    //}
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
                    //if (RowSelectedItem != null)
                    //{
                    //    // Xóa phân quyền liên quan
                    //    //var phanQuyens = await db.PhanQuyenPhanQuyens
                    //    //    .Where(x => x.PhanQuyenId == RowSelectedItem.Id)
                    //    //    .ToListAsync();

                    //    //if (phanQuyens.Any())
                    //    //{
                    //    //    db.PhanQuyenPhanQuyens.RemoveRange(phanQuyens);
                    //    //}

                    //    // Xóa người dùng
                    //    var PhanQuyen = await db.PhanQuyens.Include(c => c.PhanQuyenPhanQuyens).FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                    //    if (PhanQuyen != null)
                    //    {
                    //        // Xóa phân quyền liên quan

                    //        if (PhanQuyen.PhanQuyenPhanQuyens.Any())
                    //        {
                    //            db.PhanQuyenPhanQuyens.RemoveRange(PhanQuyen.PhanQuyenPhanQuyens);
                    //        }

                    //        db.PhanQuyens.Remove(PhanQuyen);
                    //    }

                    //    // Lưu thay đổi
                    //    await db.SaveChangesAsync();



                    //    var PhanQuyens = GetPhanQuyens();
                    //    LoadTableList(PhanQuyens);

                    //    NewId = 0;
                    //    NewFullName = "";
                    //    NewEmail = "";

                    //    ChucVuSelectedItem = null;
                    //    StatusSelectedItem = null;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi : {ex.Message}");
            }
        }

        private void LoadTableList(List<PhanQuyenDTO> data)
        {
            PhanQuyens.Clear();
            foreach (var item in data)
            {
                PhanQuyens.Add(item);
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
