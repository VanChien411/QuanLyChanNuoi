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
    internal class SoHuuGiongVNViewModel : INotifyPropertyChanged
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
            public string Name => StatusValue ? "Hoạt động" : "Khóa";

            public Status(bool statusValue = false) // Giá trị mặc định là false
            {
                StatusValue = statusValue;
            }
        }

        private int _newId;
        private string _newFullName;
        private string _newAddress;
        private string _newOfficeName;


        private string _newTextSearch;

        private CoSoVatNuoi _rowSelectedItem;
        private ToChucCaNhan _toChucCaNhanSelectedItem;
        private GiongVatNuoiType _giongVatNuoiType;
        private Status _statusSelectedItem;



        public ObservableCollection<CoSoVatNuoi> CoSoVatNuois { get; set; } = new ObservableCollection<CoSoVatNuoi>();
        public ObservableCollection<ToChucCaNhan> ToChucCaNhans { get; set; } = new ObservableCollection<ToChucCaNhan>();

        public ObservableCollection<GiongVatNuoiType> GiongVatNuoiTypes { get; set; } = new ObservableCollection<GiongVatNuoiType>();
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
        public ICommand ToChucCaNhanSelectedCommand { get; set; }



        public SoHuuGiongVNViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<CoSoVatNuoi>(OnRowSelected);
            GiongVatNuoiTypeSelectedCommand = new RelayCommandT<GiongVatNuoiType>(OnGiongVatNuoiTypeSelected);
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
               
                //RowSelectedItem = selectedItem;
                // Thực hiện hành động với SelectedItem
                //NewFullName = selectedItem.Ten;
                //NewAddress = selectedItem.DiaChi;
                //NewPhone = selectedItem.SoDienThoai;
                //NewType = selectedItem.LoaiHinh;


            }

        }
        private void OnGiongVatNuoiTypeSelected(GiongVatNuoiType selectedItem)
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
                   
                    var coSoVatNuois = db.CoSoVatNuois.AsNoTracking().Include(c => c.ToChucCaNhan).Include(c => c.GiongVatNuoi).ToList();
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

                var CoSoVatNuois = GetCoSoVatNuois()
                  .Where(x =>
                      (x.ToChucCaNhan?.Ten?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.GiongVatNuoi?.Loai?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.TenCoSo?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                      || (x.DiaChi?.ToLower().Contains(textSearch.ToLower()) ?? false)
                   )
                  .ToList();


                LoadTableList(CoSoVatNuois);


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
                    //if (!string.IsNullOrEmpty(NewFullName))
                    //{
                    //    var CoSoVatNuoi = new CoSoVatNuoi { Ten = NewFullName, DiaChi = NewAddress, Email = NewEmail, SoDienThoai = NewPhone, LoaiHinh = NewType, LoaiHoatDong = NewActiveType };
                    //    db.CoSoVatNuois.Add(CoSoVatNuoi);
                    //    db.SaveChanges();

                    //    var CoSoVatNuois = GetCoSoVatNuois();
                    //    LoadTableList(CoSoVatNuois);
                    //}

                    //if(!)
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
                    //if (RowSelectedItem != null)
                    //{
                    //    var CoSoVatNuoi = await db.CoSoVatNuois.FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                    //    CoSoVatNuoi.Ten = NewFullName;
                    //    CoSoVatNuoi.DiaChi = NewAddress;
                    //    CoSoVatNuoi.SoDienThoai = NewPhone;
                    //    CoSoVatNuoi.LoaiHinh = NewType;
                    //    CoSoVatNuoi.LoaiHoatDong = RowSelectedItem.LoaiHoatDong;

                    //    await db.SaveChangesAsync();

                    //    var CoSoVatNuois = GetCoSoVatNuois();
                    //    LoadTableList(CoSoVatNuois);
                    //    RowSelectedItem = CoSoVatNuois.FirstOrDefault(x => x.Id == CoSoVatNuoi.Id);
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
                    //    var CoSoVatNuoi = await db.CoSoVatNuois.FirstOrDefaultAsync(x => x.Id == RowSelectedItem.Id);
                    //    db.Remove(CoSoVatNuoi);

                    //    db.SaveChanges();

                    //    var CoSoVatNuois = GetCoSoVatNuois();
                    //    LoadTableList(CoSoVatNuois);

                    //    NewFullName = "";
                    //    NewEmail = "";
                    //    NewAddress = "";
                    //    NewPhone = "";
                    //    NewType = "";

                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi : {ex.Message}");
            }
        }

        private void LoadTableList(List<CoSoVatNuoi> CoSoVatNuois)
        {
            CoSoVatNuois.Clear();
            foreach (var item in CoSoVatNuois)
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
