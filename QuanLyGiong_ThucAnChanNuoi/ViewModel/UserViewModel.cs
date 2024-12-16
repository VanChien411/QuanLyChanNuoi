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
     internal class UserViewModel : INotifyPropertyChanged
    {


        private int _newId;
        private string _newFullName;
        private string _newArea;
        private DateTime _newStartDate;
        //private string _newGenSource;
        private string _foodType = "Thương mại";
        private string _factoryType = "Sản xuất";

        private string _newTextSearch;
        private string _newTextSearch2;
        private string _newTextSearch3;

        private CoSoThucAn _coSoThucAnSelectedItem;
        private ToChucNguonGen _ToChucNguonGensedItem;
        private ToChucCaNhan _toChucCaNhanSelectedItem;

        private CoSoVatNuoi _CoSoVatNuoi;



        public ObservableCollection<CoSoThucAn> CoSoThucAns { get; set; } = new ObservableCollection<CoSoThucAn>();
        public ObservableCollection<ToChucNguonGen> ToChucNguonGens { get; set; } = new ObservableCollection<ToChucNguonGen>();
        public ObservableCollection<ToChucCaNhan> ToChucCaNhans { get; set; } = new ObservableCollection<ToChucCaNhan>();


        public ObservableCollection<CoSoVatNuoi> CoSoVatNuois { get; set; } = new ObservableCollection<CoSoVatNuoi>();






        public string NewTextSearch
        {
            get => _newTextSearch;
            set
            {
                _newTextSearch = value;
                OnPropertyChanged(nameof(NewTextSearch)); // Notify UI to update binding
            }
        }
        public string NewTextSearch2
        {
            get => _newTextSearch2;
            set
            {
                _newTextSearch2 = value;
                OnPropertyChanged(nameof(NewTextSearch2)); // Notify UI to update binding
            }
        }
        public string NewTextSearch3
        {
            get => _newTextSearch3;
            set
            {
                _newTextSearch3 = value;
                OnPropertyChanged(nameof(NewTextSearch3)); // Notify UI to update binding
            }
        }
        public CoSoThucAn coSoThucAnSelectedItem
        {
            get => _coSoThucAnSelectedItem;
            set
            {
                _coSoThucAnSelectedItem = value;
                OnPropertyChanged(nameof(coSoThucAnSelectedItem));
            }
        }
        public ToChucNguonGen ToChucNguonGensedItem
        {
            get => _ToChucNguonGensedItem;
            set
            {
                _ToChucNguonGensedItem = value;
                OnPropertyChanged(nameof(ToChucNguonGensedItem));
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

        public CoSoVatNuoi CoSoVatNuoiselectedItem
        {
            get => _CoSoVatNuoi;
            set
            {
                _CoSoVatNuoi = value;

                OnPropertyChanged(nameof(CoSoVatNuoiselectedItem));
            }
        }



        //public string GenSource
        //{
        //    get => _newGenSource;
        //    set
        //    {
        //        _newGenSource = value;
        //        OnPropertyChanged(nameof(GenSource));
        //    }
        //}
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
        public string NewArea
        {
            get => _newArea;
            set
            {
                _newArea = value;
                OnPropertyChanged(nameof(NewArea));
            }
        }

        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand SearchCommand2 { get; }
        public ICommand SearchCommand3 { get; }
        public ICommand coSoThucAnSelectedCommand { get; set; }
        public ICommand ThucAnChanNuoiTypeSelectedCommand { get; set; }
        public ICommand CoSoVatNuoiselectedCommand { get; set; }
        public ICommand ToChucNguonGensedCommand { get; set; }

        public ICommand ToChucCaNhanSelectedCommand { get; set; }



        public UserViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);

            SearchCommand = new RelayCommand(Search);
            SearchCommand2 = new RelayCommand(Search2);
            SearchCommand3 = new RelayCommand(Search3);

            coSoThucAnSelectedCommand = new RelayCommandT<CoSoThucAn>(OncoSoThucAnSelected);
            CoSoVatNuoiselectedCommand = new RelayCommandT<ThucAnChanNuoi>(OnCoSoVatNuoiselected);
            ToChucCaNhanSelectedCommand = new RelayCommandT<ToChucCaNhan>(OnToChuCaNhanSelected);
            ToChucNguonGensedCommand = new RelayCommandT<CoSoThucAn>(OnToChucNguonGensed);

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
                    
   
                    var coSoThucAns = db.CoSoThucAns.Include(x => x.ToChucCaNhan).Include(x => x.ThucAnChanNuoi).ToList();
                    CoSoThucAns = new ObservableCollection<CoSoThucAn>(coSoThucAns);

                    var coSoVatNuois = db.CoSoVatNuois.Include(c => c.GiongVatNuoi).Include(c => c.ToChucCaNhan).ToList();
                    CoSoVatNuois = new ObservableCollection<CoSoVatNuoi>(coSoVatNuois);

                    var toChucCaNhan = db.ToChucCaNhans.ToList();
                    ToChucCaNhans = new ObservableCollection<ToChucCaNhan>(toChucCaNhan);

                    var toChucNguonGen = db.ToChucNguonGens.Include(c => c.ToChucCaNhan).Include(c => c.NguonGen).ToList();
                    ToChucNguonGens = new ObservableCollection<ToChucNguonGen>(toChucNguonGen);

                    NewStartDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private void OncoSoThucAnSelected(CoSoThucAn selectedItem)
        {
            if (selectedItem != null)
            {

                //Thực hiện hành động với SelectedItem
                NewId = selectedItem.Id;
                NewArea = selectedItem.DiaChi;
                NewStartDate = selectedItem.NgayCapNhat.Value;

                ToChucCaNhanSelectedItem = ToChucCaNhans.FirstOrDefault(x => x.Id == selectedItem.ToChucCaNhanId);
                CoSoVatNuoiselectedItem = CoSoVatNuois.FirstOrDefault(x => x.Id == selectedItem.ThucAnChanNuoiId);
                ToChucNguonGensedItem = ToChucNguonGens.FirstOrDefault(x => x.Id == selectedItem.Id);

            }

        }

        private void OnCoSoVatNuoiselected(ThucAnChanNuoi selectedItem)
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
        private void OnToChucNguonGensed(CoSoThucAn selectedItem)
        {
            if (selectedItem != null)
            {


            }

        }

        private void OnChucVuSelected(ChucVu selectedItem)
        {

        }

        private List<CoSoThucAn> GetCoSoThucAns()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var data = db.CoSoThucAns
                         .AsNoTracking()
                         .Include(c => c.ThucAnChanNuoi) // Tải toàn bộ mối quan hệ
                         .Include(c => c.ToChucCaNhan)
                         .Where(x => x.LoaiCoSo.Equals(_factoryType)
                                  && x.ThucAnChanNuoi.LoaiThucAn.Equals(_foodType)) // Lọc dữ liệu
                         .ToList();

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
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var data = db.CoSoVatNuois
                         .AsNoTracking()
                         .Include(c => c.ToChucCaNhan) // Tải toàn bộ mối quan hệ
                         .Include(c => c.GiongVatNuoi)
                         .ToList();

                    data = data.Where(x =>
                              (x.ToChucCaNhan?.Ten?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.GiongVatNuoi?.Loai?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.TenCoSo?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                              || (x.DiaChi?.ToLower().Contains(textSearch.ToLower()) ?? false)
                           )
                         .ToList();


                    CoSoVatNuois.Clear();
                    foreach (var item in data)
                    {
                        CoSoVatNuois.Add(item);
                    }

                }
               


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}");
            }
        }
        private async void Search2()
        {
            try
            {

                var textSearch = NewTextSearch2?.ToLower() ?? "";
                // Sử dụng && để kiểm tra x.MaBuuDien != null trước khi gọi ToLower().Contains().
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var data = db.ToChucNguonGens
                         .AsNoTracking()
                         .Include(c => c.ToChucCaNhan) // Tải toàn bộ mối quan hệ
                         .Include(c => c.NguonGen)
                         .ToList();

                    data = data.Where(x =>
                              (x.ToChucCaNhan?.Ten?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.NguonGen?.TenNguonGen?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.HoatDong?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.ToChucCaNhan?.Ten?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                              || (x.KhuVuc?.ToLower().Contains(textSearch.ToLower()) ?? false)
                           )
                         .ToList();


                    ToChucNguonGens.Clear();
                    foreach (var item in data)
                    {
                        ToChucNguonGens.Add(item);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}");
            }
        }
        private async void Search3()
        {
            try
            {

                var textSearch = NewTextSearch3?.ToLower() ?? "";
                // Sử dụng && để kiểm tra x.MaBuuDien != null trước khi gọi ToLower().Contains().
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var data = db.CoSoThucAns
                         .AsNoTracking()
                         .Include(c => c.ToChucCaNhan) // Tải toàn bộ mối quan hệ
                         .Include(c => c.ThucAnChanNuoi)
                         .ToList();

                    data = data.Where(x =>
                              (x.ToChucCaNhan?.Ten?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.ThucAnChanNuoi?.TenThucAn?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.ThucAnChanNuoi.LoaiThucAn?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.ThucAnChanNuoi?.MoTa?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                              || (x.LoaiCoSo?.ToLower().Contains(textSearch.ToLower()) ?? false)
                              || (x.TenCoSo?.ToLower().Contains(textSearch.ToLower()) ?? false)
                           )
                         .ToList();


                    CoSoThucAns.Clear();
                    foreach (var item in data)
                    {
                        CoSoThucAns.Add(item);
                    }

                }

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
                //    if (ToChucNguonGensedItem?.Id == null || CoSoVatNuoiselectedItem?.Id == null || ToChucCaNhanSelectedItem?.Id == null)
                //    {
                //        MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
                //        return;
                //    }
                //    var csta = new CoSoThucAn { TenCoSo = ToChucNguonGensedItem.TenCoSo, DiaChi = NewArea, LoaiCoSo = _factoryType, ToChucCaNhanId = ToChucCaNhanSelectedItem.Id, ThucAnChanNuoiId = CoSoVatNuoiselectedItem.Id };
                //    db.CoSoThucAns.Add(csta);
                //    db.SaveChanges();


                //    //Reset list
                //    var data = GetCoSoThucAns();
                //    LoadTableList(data);

                //    coSoThucAnSelectedItem = null;
                //    ToChucNguonGensedItem = null;
                //    CoSoVatNuoiselectedItem = null;
                //    ToChucCaNhanSelectedItem = null;


                //    NewArea = "";
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
                //    if (ToChucNguonGensedItem?.Id == null || CoSoVatNuoiselectedItem?.Id == null || ToChucCaNhanSelectedItem?.Id == null)
                //    {
                //        MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
                //        return;
                //    }
                //    if (coSoThucAnSelectedItem?.Id != null)
                //    {
                //        var csta = db.CoSoThucAns.FirstOrDefault(x => x.Id == coSoThucAnSelectedItem.Id);
                //        if (csta != null)
                //        {
                //            csta.TenCoSo = ToChucNguonGensedItem.TenCoSo;
                //            csta.DiaChi = NewArea;
                //            csta.ToChucCaNhanId = ToChucCaNhanSelectedItem.Id;
                //            csta.ThucAnChanNuoiId = CoSoVatNuoiselectedItem.Id;
                //            csta.NgayCapNhat = NewStartDate;


                //            db.SaveChanges();


                //            //Reset list
                //            var data = GetCoSoThucAns();
                //            LoadTableList(data);

                //            coSoThucAnSelectedItem = CoSoThucAns.FirstOrDefault(x => x.Id == csta.Id);
                //        }
                //    }
                //    else
                //    {

                //        MessageBox.Show("Chọn dữ liệu muốn sửa trong bảng");
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
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    if (coSoThucAnSelectedItem != null)
                    {
                        var gvnBt = db.CoSoThucAns.Where(x => x.Id == coSoThucAnSelectedItem.Id).FirstOrDefault();
                        db.Remove(gvnBt);
                        db.SaveChanges();

                        // Reset list
                        var data = GetCoSoThucAns();
                        LoadTableList(data);


                        coSoThucAnSelectedItem = null;
                        ToChucNguonGensedItem = null;
                        CoSoVatNuoiselectedItem = null;
                        ToChucCaNhanSelectedItem = null;


                        NewArea = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xoa: {ex.Message}");
            }
        }

        private void LoadTableList(List<CoSoThucAn> data)
        {
            CoSoThucAns.Clear();
            foreach (var item in data)
            {
                CoSoThucAns.Add(item);
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
