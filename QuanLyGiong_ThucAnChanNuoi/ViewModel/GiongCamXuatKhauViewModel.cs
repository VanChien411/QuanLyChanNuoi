using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace QuanLyGiong_ThucAnChanNuoi.ViewModel
{
    internal class GiongCamXuatKhauViewModel : INotifyPropertyChanged
    {


        public class GiongVatNuoiType
        {
            public string Name { get; set; }

            public List<GiongVatNuoiType> GetList()
            {
                return new List<GiongVatNuoiType>
                    {
                        new GiongVatNuoiType { Name = "Bò" },
                        new GiongVatNuoiType { Name = "Lợn" },
                        new GiongVatNuoiType { Name = "Gà" },
                        new GiongVatNuoiType { Name = "Vịt" },
                        new GiongVatNuoiType { Name = "Dê" }
                    };
            }
        }

        public class Status
        {
            public bool StatusValue { get; set; }
            public string Name => StatusValue ? "Đang bảo tồn" : "Hết bảo tồn";

            public Status(bool statusValue = false) // Giá trị mặc định là false
            {
                StatusValue = statusValue;
            }
        }

        private int _newId;
        private string _newFullName;
        private string _newKind;
        private DateTime _newStartDate;
        private string _newAnimalName;
        private string _type = "Cấm xuất khẩu";


        private string _newTextSearch;

        private GiongCanBaoTon _rowSelectedItem;
        private ToChucCaNhan _toChucCaNhanSelectedItem;
        private GiongVatNuoiType _giongVatNuoiType;
        private GiongVatNuoi _giongVatNuoi;

        private Status _statusSelectedItem;



        public ObservableCollection<GiongCanBaoTon> GiongCanBaoTons { get; set; } = new ObservableCollection<GiongCanBaoTon>();
        public ObservableCollection<ToChucCaNhan> ToChucCaNhans { get; set; } = new ObservableCollection<ToChucCaNhan>();

        public ObservableCollection<GiongVatNuoiType> GiongVatNuoiTypes { get; set; } = new ObservableCollection<GiongVatNuoiType>();
        public ObservableCollection<GiongVatNuoi> GiongVatNuois { get; set; } = new ObservableCollection<GiongVatNuoi>();

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

        public GiongCanBaoTon RowSelectedItem
        {
            get => _rowSelectedItem;
            set
            {
                _rowSelectedItem = value;
                OnPropertyChanged(nameof(RowSelectedItem));
            }
        }
        public ToChucCaNhan ToChucCaNhanSelectedItem
        {
            get => _toChucCaNhanSelectedItem;
            set
            {
                _toChucCaNhanSelectedItem = value;
                OnPropertyChanged(nameof(ToChucCaNhanSelectedItem));
            }
        }
        public GiongVatNuoiType GiongVatNuoiTypeSelectedItem
        {
            get => _giongVatNuoiType;
            set
            {
                _giongVatNuoiType = value;

                OnPropertyChanged(nameof(GiongVatNuoiTypeSelectedItem));
            }
        }
        public GiongVatNuoi GiongVatNuoiSelectedItem
        {
            get => _giongVatNuoi;
            set
            {
                _giongVatNuoi = value;
                if (value != null)
                {
                    // Khi có SelectedItem, cập nhật Text để khớp với item
                    AnimalName = (value as GiongVatNuoi)?.TenGiong; // Thay 'YourItemType' bằng kiểu object trong ItemsSource
                }
                OnPropertyChanged(nameof(GiongVatNuoiSelectedItem));
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

        public string AnimalName
        {
            get => _newAnimalName;
            set
            {
                _newAnimalName = value;
                OnPropertyChanged(nameof(AnimalName));
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
        public DateTime NewStartDate
        {
            get => _newStartDate;
            set
            {
                _newStartDate = value;
                OnPropertyChanged(nameof(NewStartDate));
            }
        }
        public string NewKind
        {
            get => _newKind;
            set
            {
                _newKind = value;
                OnPropertyChanged(nameof(NewKind));
            }
        }

        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand RowSelectedCommand { get; set; }
        public ICommand GiongVatNuoiTypeSelectedCommand { get; set; }
        public ICommand GiongVatNuoiSelectedCommand { get; set; }

        public ICommand ToChucCaNhanSelectedCommand { get; set; }



        public GiongCamXuatKhauViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<GiongCanBaoTon>(OnRowSelected);
            //GiongVatNuoiTypeSelectedCommand = new RelayCommandT<GiongVatNuoiType>(OnGiongVatNuoiTypeSelected);
            GiongVatNuoiSelectedCommand = new RelayCommandT<GiongVatNuoi>(OnGiongVatNuoiSelected);
            //ToChucCaNhanSelectedCommand = new RelayCommandT<ToChucCaNhan>(OnToChuCaNhanSelected);

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

                    var rowData = GetGiongCanBaoTons();
                    //Gan gia tri cho table list
                    GiongCanBaoTons = new ObservableCollection<GiongCanBaoTon>(rowData);

                    //var giongVatNuoiTypes = new GiongVatNuoiType().GetList();
                    //GiongVatNuoiTypes = new ObservableCollection<GiongVatNuoiType>(giongVatNuoiTypes);

                    var giongVatNuois = db.GiongVatNuois.ToList();
                    GiongVatNuois = new ObservableCollection<GiongVatNuoi>(giongVatNuois);
                    //var toChucCaNhan = db.ToChucCaNhans.ToList();
                    //ToChucCaNhans = new ObservableCollection<ToChucCaNhan>(toChucCaNhan);

                    var trangThais = new List<Status> { new Status(true), new Status(false) };
                    TrangThais = new ObservableCollection<Status>(trangThais);

                    NewStartDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private void OnRowSelected(GiongCanBaoTon selectedItem)
        {
            if (selectedItem != null)
            {

                //Thực hiện hành động với SelectedItem
                NewId = selectedItem.Id;
                NewKind = selectedItem.Giong?.Loai;
                NewStartDate = selectedItem.NgayBaoTon.Value;


                GiongVatNuoiSelectedItem = GiongVatNuois.FirstOrDefault(x => x.Id == NewId);
                StatusSelectedItem = TrangThais.FirstOrDefault(c => c.StatusValue == selectedItem.TrangThai);

            }

        }
        private void OnGiongVatNuoiTypeSelected(GiongVatNuoiType selectedItem)
        {
            if (selectedItem != null)
            {


            }

        }
        private void OnGiongVatNuoiSelected(GiongVatNuoi selectedItem)
        {
            if (selectedItem != null)
            {


            }

        }
        private void OnToChuCaNhanSelected(ToChucCaNhan selectedItem)
        {
            if (selectedItem != null)
            {


            }

        }

        private void OnChucVuSelected(ChucVu selectedItem)
        {

        }

        private List<GiongCanBaoTon> GetGiongCanBaoTons()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var data = db.GiongCanBaoTons.AsNoTracking().Include(c => c.Giong).Where(x => x.Loai.Equals(_type)).ToList();
                    return data;
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

                var GiongCanBaoTons = GetGiongCanBaoTons()
                  .Where(x =>
                      (x.Giong?.TenGiong?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Giong?.Loai?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.NgayBaoTon?.ToString().ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                      || (x.TrangThaiHienThi?.ToLower().Contains(textSearch.ToLower()) ?? false)
                   )
                  .ToList();


                LoadTableList(GiongCanBaoTons);


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


                    if (string.IsNullOrEmpty(AnimalName) || string.IsNullOrEmpty(NewKind) || string.IsNullOrEmpty(NewStartDate.ToString()) || StatusSelectedItem?.StatusValue == null)
                    {
                        MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
                        return;
                    }

                    int? GiongId = null;
                    if (GiongVatNuoiSelectedItem != null)
                    {
                        GiongId = GiongVatNuoiSelectedItem.Id;

                        var check = db.GiongCanBaoTons.FirstOrDefault(x => x.Loai == _type && x.GiongId == GiongId);
                        if (check != null)
                        {
                            MessageBox.Show("Dữ liệu có sãn hãy [ sửa hoặc xóa]");
                            return;
                        }
                    }
                    else if (!string.IsNullOrEmpty(AnimalName) && !string.IsNullOrEmpty(NewKind))
                    {
                        var gvn = new GiongVatNuoi { TenGiong = AnimalName, Loai = NewKind };
                        db.GiongVatNuois.Add(gvn);
                        db.SaveChanges();

                        GiongId = gvn.Id;

                    }
                    if (GiongId != null)
                    {
                        var gvnBt = new GiongCanBaoTon { GiongId = GiongId.Value, Loai = _type, NgayBaoTon = NewStartDate, TrangThai = StatusSelectedItem.StatusValue };
                        db.GiongCanBaoTons.Add(gvnBt);
                        db.SaveChanges();

                        // Reset list
                        var data = GetGiongCanBaoTons();
                        LoadTableList(data);

                        RowSelectedItem = null;
                        GiongVatNuoiSelectedItem = null;
                        NewKind = "";
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

                    if (string.IsNullOrEmpty(AnimalName) || string.IsNullOrEmpty(NewKind) || string.IsNullOrEmpty(NewStartDate.ToString()) || StatusSelectedItem?.StatusValue == null)
                    {
                        MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
                        return;
                    }

                    if (RowSelectedItem != null)
                    {
                        int? GiongId = null;
                        if (GiongVatNuoiSelectedItem != null)
                        {
                            GiongId = GiongVatNuoiSelectedItem.Id;

                        }
                        else if (!string.IsNullOrEmpty(AnimalName) && !string.IsNullOrEmpty(NewKind))
                        {
                            var gvn = new GiongVatNuoi { TenGiong = AnimalName, Loai = NewKind };
                            db.GiongVatNuois.Add(gvn);
                            db.SaveChanges();

                            GiongId = gvn.Id;

                        }
                        if (GiongId != null)
                        {
                            var gvnBt = db.GiongCanBaoTons.Where(x => x.Id == RowSelectedItem.Id).FirstOrDefault();
                            gvnBt.GiongId = GiongId.Value;
                            gvnBt.Loai = _type;
                            gvnBt.TrangThai = StatusSelectedItem?.StatusValue;
                            gvnBt.NgayBaoTon = NewStartDate;

                            var gvn = db.GiongVatNuois.FirstOrDefault(x => x.Id == GiongId);
                            gvn.TenGiong = AnimalName;
                            gvn.Loai = NewKind;

                            db.SaveChanges();

                            // Reset list
                            var data = GetGiongCanBaoTons();
                            LoadTableList(data);
                            RowSelectedItem = GiongCanBaoTons.FirstOrDefault(x => x.Id == gvnBt.Id);
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

                    if (RowSelectedItem != null)
                    {
                        var gvnBt = db.GiongCanBaoTons.Where(x => x.Id == RowSelectedItem.Id).FirstOrDefault();
                        db.Remove(gvnBt);
                        db.SaveChanges();

                        // Reset list
                        var data = GetGiongCanBaoTons();
                        LoadTableList(data);

                        RowSelectedItem = null;
                        GiongVatNuoiSelectedItem = null;
                        NewKind = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xoa: {ex.Message}");
            }
        }

        private void LoadTableList(List<GiongCanBaoTon> data)
        {
            GiongCanBaoTons.Clear();
            foreach (var item in data)
            {
                GiongCanBaoTons.Add(item);
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
