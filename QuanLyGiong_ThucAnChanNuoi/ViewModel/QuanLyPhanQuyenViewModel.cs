using ControlzEx.Standard;
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
        private NguoiDung _nguoiDungSelectedItem;
        private PhanQuyen _quyenSelectdItem;
        private Nhom _nhomSelectdItem;
        private PhanQuyen _quyenNhomSelectdItem;


        public ObservableCollection<PhanQuyenDTO> PhanQuyens { get; set; } = new ObservableCollection<PhanQuyenDTO>();
        public ObservableCollection<NguoiDung> NguoiDungs { get; set; } = new ObservableCollection<NguoiDung>();
        public ObservableCollection<PhanQuyen> Quyens { get; set; } = new ObservableCollection<PhanQuyen>();
        public ObservableCollection<Nhom> Nhoms { get; set; } = new ObservableCollection<Nhom>();
        public ObservableCollection<PhanQuyen> QuyensOfNhom { get; set; } = new ObservableCollection<PhanQuyen>();



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

        public NguoiDung NguoiDungSelectedItem
        {
            get => _nguoiDungSelectedItem;
            set
            {
                if (_nguoiDungSelectedItem != value) // Chỉ thay đổi khi giá trị khác
                {
                    _nguoiDungSelectedItem = value;
                    OnPropertyChanged(nameof(NguoiDungSelectedItem)); // Thông báo UI
                }
            }
        }

        public PhanQuyen QuyenSelectedItem
        {
            get => _quyenSelectdItem;
            set
            {
                _quyenSelectdItem = value;
             
                OnPropertyChanged(nameof(QuyenSelectedItem));
            }
        }
        public Nhom NhomSelectedItem
        {
            get => _nhomSelectdItem;
            set
            {
                _nhomSelectdItem = value;
             
                OnPropertyChanged(nameof(NhomSelectedItem));
            }
        }
        public PhanQuyen QuyenNhomSelectedItem
        {
            get => _quyenNhomSelectdItem;
            set
            {
                _quyenNhomSelectdItem = value;
                OnPropertyChanged(nameof(QuyenNhomSelectedItem));
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
        public ICommand RowSelectedCommand { get; set; }
        public ICommand NguoiDungSelectedCommand { get; set; }
        public ICommand QuyenSelectedCommand { get; set; }
        public ICommand NhomSelectedCommand { get; set; }
        public ICommand QuyenNhomSelectedCommand { get; set; }


        public QuanLyPhanQuyenViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<PhanQuyenDTO>(OnRowSelected);
            NguoiDungSelectedCommand = new RelayCommandT<NguoiDung>(OnNguoiDungSelected);
            QuyenSelectedCommand = new RelayCommandT<PhanQuyen>(OnQuyenSelected);
            NhomSelectedCommand = new RelayCommandT<Nhom>(OnNhomSelected);
            QuyenNhomSelectedCommand = new RelayCommandT<PhanQuyen>(OnQuyenNhomSelected);


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
                
                    var nguoiDungs = db.NguoiDungs.ToList();
                    NguoiDungs = new ObservableCollection<NguoiDung>(nguoiDungs);

                    var quyens = db.PhanQuyens.ToList();
                    Quyens = new ObservableCollection<PhanQuyen>(quyens);

                    var nhoms = db.Nhoms.ToList();
                    Nhoms = new ObservableCollection<Nhom>(nhoms);

            
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

                // Thực hiện hành động với SelectedItem

                NguoiDungSelectedItem = NguoiDungs.FirstOrDefault(c => c.Id == selectedItem?.NguoiDung?.Id);
                QuyenSelectedItem = Quyens.FirstOrDefault(c => c.MaQuyen == selectedItem?.Quyen?.MaQuyen);

                NhomSelectedItem = Nhoms.FirstOrDefault(c => c.Id ==selectedItem?.Nhom?.Id);

             
            }

        }
        private void OnNguoiDungSelected(NguoiDung selectedItem)
        {
          

        }

        private void OnQuyenSelected(PhanQuyen selectedItem)
        {
           if(selectedItem != null && QuyenSelectedItem != null)
            {
                // Giới hạng chỉ thêm 1 quyền hoặc 1 nhóm mỗi lần
               
                    NhomSelectedItem = null;
                    QuyensOfNhom.Clear();
                
            }

        }
        private void OnNhomSelected(Nhom selectedItem)
        {
            if(selectedItem != null)
            {
               if(QuyenSelectedItem != null && NhomSelectedItem != null)
                    QuyenSelectedItem = null;
              

                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var quyens = db.PhanQuyenNhoms.Include(c => c.PhanQuyen).Where(c => c.NhomID == selectedItem.Id).ToList();
                    if(quyens.Count > 0)
                    {
                        var list = new List<PhanQuyen>();
                        // Tao list phan quyen va them tung quyen thuc nhóm
                        foreach(var item in quyens)
                        {
                            if(item.PhanQuyen != null)
                            {
                                list.Add(item.PhanQuyen);
                            }
                        }

                        QuyensOfNhom.Clear();
                        foreach (var item in list)
                        {
                            QuyensOfNhom.Add(item);

                        }
                        if(list.Count > 0)
                            QuyenNhomSelectedItem  = QuyensOfNhom[0];
                    }
                    

                }
            }
       

        }
        private void OnQuyenNhomSelected(PhanQuyen selectedItem)
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
                    //Kiem tra rowselected có trùng với nguoidungselected 
                    if (RowSelectedItem != null) {
                        if (RowSelectedItem.NguoiDung?.Id == NguoiDungSelectedItem?.Id) {
                            // Cho phep cap nhap
                            // Kiểm tra nếu quyền đã tồn tại trong cơ sở dữ liệu.
                            if (RowSelectedItem?.Quyen != null)
                            {
                                var phanQuyenNguoiDung = db.PhanQuyenNguoiDungs
                                    .Where(x => x.NguoiDungId == RowSelectedItem.NguoiDung.Id
                                                      && (x.MaQuyen == RowSelectedItem.Quyen.MaQuyen))
                                    .FirstOrDefault();

                                if (phanQuyenNguoiDung != null) {

                                    phanQuyenNguoiDung.MaQuyen = QuyenSelectedItem?.MaQuyen;
                                    db.SaveChanges();

                                    //var phanQuyen = new Models.PhanQuyenNguoiDung { NguoiDungId = RowSelectedItem.NguoiDung.Id, MaQuyen = QuyenSelectedItem.MaQuyen };
                                    //db.PhanQuyenNguoiDungs.Add(phanQuyen);
                                    //db.SaveChanges();

                                }

                            }

                            if (RowSelectedItem?.Nhom != null) {

                                var thanhVienNhom = db.ThanhVienNhoms
                                    .Where(x => x.NguoiDungId == RowSelectedItem.NguoiDung.Id
                                    && x.NhomId == RowSelectedItem.Nhom.Id).FirstOrDefault();
                                if(thanhVienNhom != null)
                                {
                                    thanhVienNhom.NhomId = NhomSelectedItem.Id;
                                    thanhVienNhom.NgayThamGia  = DateTime.Now;
                                    db.SaveChanges();

                                    //var tvn = new Models.ThanhVienNhom {NguoiDungId = RowSelectedItem.NguoiDung.Id, NhomId = NhomSelectedItem.Id, NgayThamGia = DateTime.Now };
                                    //db.ThanhVienNhoms.Add(tvn);
                                    //db.SaveChanges();
                                }
                            }
                            
                        }

                        var phanQuyens = GetPhanQuyens();
                        LoadTableList(phanQuyens);

                    }
                    else
                        MessageBox.Show("Chọn dòng dữ liệu muốn cập nhập trong table");
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

                    //    NguoiDungSelectedItem = null;
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
