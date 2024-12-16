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
     internal class SanXuatThucAnViewModel : INotifyPropertyChanged
    {


        private int _newId;
        private string _newFullName;
        private string _newArea;
        private DateTime _newStartDate;
        //private string _newGenSource;
        private string _foodType = "Thương mại";
        private string _factoryType = "Sản xuất";

        private string _newTextSearch;

        private CoSoThucAn _rowSelectedItem;
        private CoSoThucAn _CoSoThucAnSelectedItem;
        private ToChucCaNhan _toChucCaNhanSelectedItem;

        private ThucAnChanNuoi _ThucAnChanNuoi;



        public ObservableCollection<CoSoThucAn> CoSoThucAns { get; set; } = new ObservableCollection<CoSoThucAn>();
        public ObservableCollection<CoSoThucAn> CoSoThucAnSelect { get; set; } = new ObservableCollection<CoSoThucAn>();
        public ObservableCollection<ToChucCaNhan> ToChucCaNhans { get; set; } = new ObservableCollection<ToChucCaNhan>();


        public ObservableCollection<ThucAnChanNuoi> ThucAnChanNuois { get; set; } = new ObservableCollection<ThucAnChanNuoi>();






        public string NewTextSearch
        {
            get => _newTextSearch;
            set
            {
                _newTextSearch = value;
                OnPropertyChanged(nameof(NewTextSearch)); // Notify UI to update binding
            }
        }

        public CoSoThucAn RowSelectedItem
        {
            get => _rowSelectedItem;
            set
            {
                _rowSelectedItem = value;
                OnPropertyChanged(nameof(RowSelectedItem));
            }
        }
        public CoSoThucAn CoSoThucAnSelectedItem
        {
            get => _CoSoThucAnSelectedItem;
            set
            {
                _CoSoThucAnSelectedItem = value;
                OnPropertyChanged(nameof(CoSoThucAnSelectedItem));
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

        public ThucAnChanNuoi ThucAnChanNuoiselectedItem
        {
            get => _ThucAnChanNuoi;
            set
            {
                _ThucAnChanNuoi = value;
               
                OnPropertyChanged(nameof(ThucAnChanNuoiselectedItem));
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
        public ICommand RowSelectedCommand { get; set; }
        public ICommand ThucAnChanNuoiTypeSelectedCommand { get; set; }
        public ICommand ThucAnChanNuoiselectedCommand { get; set; }
        public ICommand CoSoThucAnSelectedCommand { get; set; }

        public ICommand ToChucCaNhanSelectedCommand { get; set; }



        public SanXuatThucAnViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<CoSoThucAn>(OnRowSelected);
            ThucAnChanNuoiselectedCommand = new RelayCommandT<ThucAnChanNuoi>(OnThucAnChanNuoiselected);
            ToChucCaNhanSelectedCommand = new RelayCommandT<ToChucCaNhan>(OnToChuCaNhanSelected);
            CoSoThucAnSelectedCommand = new RelayCommandT<CoSoThucAn>(OnCoSoThucAnSelected);

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
               
                    var rowData = GetCoSoThucAns();
                    //Gan gia tri cho table list
                    CoSoThucAns = new ObservableCollection<CoSoThucAn>(rowData);

                    var coSoThucAns = db.CoSoThucAns.ToList();
                    CoSoThucAnSelect = new ObservableCollection<CoSoThucAn>(coSoThucAns);

                    var thucAnChanNuois = db.ThucAnChanNuois.ToList();
                    ThucAnChanNuois = new ObservableCollection<ThucAnChanNuoi>(thucAnChanNuois);

                    var toChucCaNhan = db.ToChucCaNhans.ToList();
                    ToChucCaNhans = new ObservableCollection<ToChucCaNhan>(toChucCaNhan);



                    NewStartDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private void OnRowSelected(CoSoThucAn selectedItem)
        {
            if (selectedItem != null)
            {

                //Thực hiện hành động với SelectedItem
                NewId = selectedItem.Id;
                NewArea = selectedItem.DiaChi;
                NewStartDate = selectedItem.NgayCapNhat ?? DateTime.Now; // Hoặc một giá trị mặc định khác


                ToChucCaNhanSelectedItem = ToChucCaNhans.FirstOrDefault(x => x.Id == selectedItem.ToChucCaNhanId);
                ThucAnChanNuoiselectedItem = ThucAnChanNuois.FirstOrDefault(x => x.Id == selectedItem.ThucAnChanNuoiId);
                CoSoThucAnSelectedItem = CoSoThucAnSelect.FirstOrDefault(x => x.Id == selectedItem.Id);

            }

        }

        private void OnThucAnChanNuoiselected(ThucAnChanNuoi selectedItem)
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
        private void OnCoSoThucAnSelected(CoSoThucAn selectedItem)
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

                var CoSoThucAns = GetCoSoThucAns()
                  .Where(x =>
                      (x.TenCoSo?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.DiaChi?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.NgayCapNhat?.ToString().ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                      || (x.ThucAnChanNuoi?.TenThucAn?.ToLower().Contains(textSearch.ToLower()) ?? false)
                   )
                  .ToList();


                LoadTableList(CoSoThucAns);


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
                    if(CoSoThucAnSelectedItem?.Id == null || ThucAnChanNuoiselectedItem?.Id == null || ToChucCaNhanSelectedItem?.Id == null)
                    {
                        MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
                        return;
                    }
                    var csta = new CoSoThucAn { TenCoSo = CoSoThucAnSelectedItem.TenCoSo, DiaChi = NewArea, LoaiCoSo = _factoryType, ToChucCaNhanId = ToChucCaNhanSelectedItem.Id, ThucAnChanNuoiId = ThucAnChanNuoiselectedItem.Id };
                    db.CoSoThucAns.Add(csta);
                    db.SaveChanges();


                    //Reset list
                    var data = GetCoSoThucAns();
                    LoadTableList(data);

                    RowSelectedItem = null;
                    CoSoThucAnSelectedItem = null;
                    ThucAnChanNuoiselectedItem = null;
                    ToChucCaNhanSelectedItem = null;

                    
                    NewArea = "";
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
                    if (CoSoThucAnSelectedItem?.Id == null || ThucAnChanNuoiselectedItem?.Id == null || ToChucCaNhanSelectedItem?.Id == null)
                    {
                        MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
                        return;
                    }
                    if (RowSelectedItem?.Id != null) {
                        var csta = db.CoSoThucAns.FirstOrDefault(x => x.Id == RowSelectedItem.Id);
                        if (csta != null) {
                            csta.TenCoSo = CoSoThucAnSelectedItem.TenCoSo;
                            csta.DiaChi = NewArea;
                            csta.ToChucCaNhanId = ToChucCaNhanSelectedItem.Id;
                            csta.ThucAnChanNuoiId = ThucAnChanNuoiselectedItem.Id;
                            csta.NgayCapNhat = NewStartDate;


                            db.SaveChanges();


                            //Reset list
                            var data = GetCoSoThucAns();
                            LoadTableList(data);

                            RowSelectedItem = CoSoThucAns.FirstOrDefault(x => x.Id == csta.Id);
                        }
                    }  else
                    {

                        MessageBox.Show("Chọn dữ liệu muốn sửa trong bảng");
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
                        var gvnBt = db.CoSoThucAns.Where(x => x.Id == RowSelectedItem.Id).FirstOrDefault();
                        db.Remove(gvnBt);
                        db.SaveChanges();

                        // Reset list
                        var data = GetCoSoThucAns();
                        LoadTableList(data);


                        RowSelectedItem = null;
                        CoSoThucAnSelectedItem = null;
                        ThucAnChanNuoiselectedItem = null;
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
