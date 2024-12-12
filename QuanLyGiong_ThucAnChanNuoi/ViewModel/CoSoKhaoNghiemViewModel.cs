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
    internal class CoSoKhaoNghiemViewModel : INotifyPropertyChanged
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
            public string Name => StatusValue ? "Đang nghiên cứu" : "Đã nghiên cứu xong";

            public Status(bool statusValue = false) // Giá trị mặc định là false
            {
                StatusValue = statusValue;
            }
        }

        private int _newId;
        private string _newFullName;
        private string _newAddress;
        private string _newOfficeName;
        private string _coSoType = "Khảo nghiệm";

        private string _newTextSearch;

        private CoSoVatNuoi _rowSelectedItem;
        private ToChucCaNhan _toChucCaNhanSelectedItem;
        private GiongVatNuoiType _giongVatNuoiType;
        private GiongVatNuoi _giongVatNuoi;

        private Status _statusSelectedItem;



        public ObservableCollection<CoSoVatNuoi> CoSoVatNuois { get; set; } = new ObservableCollection<CoSoVatNuoi>();
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

        public CoSoVatNuoi RowSelectedItem
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
        public string NewOfficeName
        {
            get => _newOfficeName;
            set
            {
                _newOfficeName = value;
                OnPropertyChanged(nameof(NewOfficeName));
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

        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand RowSelectedCommand { get; set; }
        public ICommand GiongVatNuoiTypeSelectedCommand { get; set; }
        public ICommand GiongVatNuoiSelectedCommand { get; set; }

        public ICommand ToChucCaNhanSelectedCommand { get; set; }



        public CoSoKhaoNghiemViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<CoSoVatNuoi>(OnRowSelected);
            GiongVatNuoiTypeSelectedCommand = new RelayCommandT<GiongVatNuoiType>(OnGiongVatNuoiTypeSelected);
            GiongVatNuoiSelectedCommand = new RelayCommandT<GiongVatNuoi>(OnGiongVatNuoiSelected);
            ToChucCaNhanSelectedCommand = new RelayCommandT<ToChucCaNhan>(OnToChuCaNhanSelected);

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

                    var coSoVatNuois = GetCoSoVatNuois();
                    //Gan gia tri cho table list
                    CoSoVatNuois = new ObservableCollection<CoSoVatNuoi>(coSoVatNuois);

                    var giongVatNuoiTypes = new GiongVatNuoiType().GetList();
                    GiongVatNuoiTypes = new ObservableCollection<GiongVatNuoiType>(giongVatNuoiTypes);

                    var giongVatNuois = db.GiongVatNuois.ToList();
                    GiongVatNuois = new ObservableCollection<GiongVatNuoi>(giongVatNuois);
                    var toChucCaNhan = db.ToChucCaNhans.ToList();
                    ToChucCaNhans = new ObservableCollection<ToChucCaNhan>(toChucCaNhan);

                    var trangThais = new List<Status> { new Status(true), new Status(false) };
                    TrangThais = new ObservableCollection<Status>(trangThais);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private void OnRowSelected(CoSoVatNuoi selectedItem)
        {
            if (selectedItem != null)
            {

                // Thực hiện hành động với SelectedItem
                //NewFullName = selectedItem.Ten;
                //NewAddress = selectedItem.DiaChi;
                //NewPhone = selectedItem.SoDienThoai;
                //NewType = selectedItem.LoaiHinh;

                NewId = selectedItem.Id;
                NewOfficeName = selectedItem.TenCoSo;
                NewAddress = selectedItem.DiaChi;

                ToChucCaNhanSelectedItem = ToChucCaNhans.FirstOrDefault(c => c.Id == selectedItem.ToChucCaNhanId);
                GiongVatNuoiSelectedItem = GiongVatNuois.FirstOrDefault(c => c.Id == selectedItem.GiongVatNuoiId);
                StatusSelectedItem = TrangThais.FirstOrDefault(c => c.StatusValue == selectedItem.Trangthai);

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

        private List<CoSoVatNuoi> GetCoSoVatNuois()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var coSoVatNuois = db.CoSoVatNuois.AsNoTracking().Include(c => c.ToChucCaNhan).Include(c => c.GiongVatNuoi).Where(x => x.LoaiCoSo.Equals(_coSoType)).ToList();
                    return coSoVatNuois;
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

                var coSoVatNuois = GetCoSoVatNuois()
                  .Where(x =>
                      (x.ToChucCaNhan?.Ten?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.GiongVatNuoi?.Loai?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.TenCoSo?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                      || (x.DiaChi?.ToLower().Contains(textSearch.ToLower()) ?? false)
                   )
                  .ToList();


                LoadTableList(coSoVatNuois);


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
                
                    if (!string.IsNullOrEmpty(NewOfficeName) && ToChucCaNhanSelectedItem?.Id != null && GiongVatNuoiSelectedItem?.Id != null)
                    {
                        var coSoKhaoNghiem = new CoSoVatNuoi {TenCoSo = NewOfficeName, DiaChi = NewAddress, GiongVatNuoiId = GiongVatNuoiSelectedItem.Id, ToChucCaNhanId =  ToChucCaNhanSelectedItem.Id, LoaiCoSo = _coSoType, Trangthai = true };
                        db.CoSoVatNuois.Add(coSoKhaoNghiem);
                        db.SaveChanges();


                        var CoSoVatNuois = GetCoSoVatNuois();
                        LoadTableList(CoSoVatNuois);
                    }
                    else {
                        MessageBox.Show($"Nhập đầy đủ Tên cơ sở, tổ chức cá nhân và giống vật nuôi");
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
                   
                
                    if (RowSelectedItem != null && !string.IsNullOrEmpty(NewOfficeName) && ToChucCaNhanSelectedItem?.Id != null && GiongVatNuoiSelectedItem?.Id != null)
                    {
                        var coSoVatNuoi = await db.CoSoVatNuois.FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                        coSoVatNuoi.TenCoSo = NewOfficeName;
                        coSoVatNuoi.ToChucCaNhanId = ToChucCaNhanSelectedItem.Id;
                        coSoVatNuoi.DiaChi = NewAddress;
                        coSoVatNuoi.GiongVatNuoiId = GiongVatNuoiSelectedItem.Id;
                        coSoVatNuoi.Trangthai = _statusSelectedItem.StatusValue;

                        db.SaveChanges();

                        var coSoVatNuois = GetCoSoVatNuois();
                        LoadTableList(coSoVatNuois);

                        RowSelectedItem = CoSoVatNuois.FirstOrDefault(x => x.Id == coSoVatNuoi.Id);
                    }
                    else
                    {
                        MessageBox.Show($"Nhập đầy đủ Tên cơ sở, tổ chức cá nhân và giống vật nuôi");
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
                        var coSoVatNuoi = await db.CoSoVatNuois.FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                        db.Remove(coSoVatNuoi);

                        db.SaveChanges();


                        var coSoVatNuois = GetCoSoVatNuois();
                        LoadTableList(coSoVatNuois);

                        NewId = 0;
                        NewOfficeName = "";
                        NewAddress = "";

                        ToChucCaNhanSelectedItem = null;
                        GiongVatNuoiSelectedItem = null;
                        StatusSelectedItem = null;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi : {ex.Message}");
            }
        }

        private void LoadTableList(List<CoSoVatNuoi> coSoVatNuois)
        {
            CoSoVatNuois.Clear();
            foreach (var item in coSoVatNuois)
            {
                CoSoVatNuois.Add(item);
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
