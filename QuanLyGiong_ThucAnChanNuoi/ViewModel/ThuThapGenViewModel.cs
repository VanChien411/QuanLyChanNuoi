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

    internal class ThuThapGenViewModel : INotifyPropertyChanged
    {

      
        private int _newId;
        private string _newFullName;
        private string _newArea;
        private DateTime _newStartDate;
        private string _newGenSource;
        private string _active = "Thu thập";


        private string _newTextSearch;

        private ToChucNguonGen _rowSelectedItem;
        private ToChucCaNhan _toChucCaNhanSelectedItem;
    
        private NguonGen _NguonGen;



        public ObservableCollection<ToChucNguonGen> ToChucNguonGens { get; set; } = new ObservableCollection<ToChucNguonGen>();
        public ObservableCollection<ToChucCaNhan> ToChucCaNhans { get; set; } = new ObservableCollection<ToChucCaNhan>();

    
        public ObservableCollection<NguonGen> NguonGens { get; set; } = new ObservableCollection<NguonGen>();

      




        public string NewTextSearch
        {
            get => _newTextSearch;
            set
            {
                _newTextSearch = value;
                OnPropertyChanged(nameof(NewTextSearch)); // Notify UI to update binding
            }
        }

        public ToChucNguonGen RowSelectedItem
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
    
        public NguonGen NguonGenselectedItem
        {
            get => _NguonGen;
            set
            {
                _NguonGen = value;
                if (value != null)
                {
                    // Khi có SelectedItem, cập nhật Text để khớp với item
                    GenSource = (value as NguonGen)?.TenNguonGen; // Thay 'YourItemType' bằng kiểu object trong ItemsSource
                }
                OnPropertyChanged(nameof(NguonGenselectedItem));
            }
        }

      

        public string GenSource
        {
            get => _newGenSource;
            set
            {
                _newGenSource = value;
                OnPropertyChanged(nameof(GenSource));
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
        public ICommand NguonGenTypeSelectedCommand { get; set; }
        public ICommand NguonGenselectedCommand { get; set; }

        public ICommand ToChucCaNhanSelectedCommand { get; set; }



        public ThuThapGenViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<ToChucNguonGen>(OnRowSelected);
            NguonGenselectedCommand = new RelayCommandT<NguonGen>(OnNguonGenselected);
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

                    var rowData = GetToChucNguonGens();
                    //Gan gia tri cho table list
                    ToChucNguonGens = new ObservableCollection<ToChucNguonGen>(rowData);

                    var nguonGens = db.NguonGens.ToList();
                    NguonGens = new ObservableCollection<NguonGen>(nguonGens);
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
        private void OnRowSelected(ToChucNguonGen selectedItem)
        {
            if (selectedItem != null)
            {

                ////Thực hiện hành động với SelectedItem
                NewId = selectedItem.Id;
                NewArea = selectedItem.KhuVuc;
                NewStartDate = selectedItem.NgayThuThap ?? DateTime.Now;

                ToChucCaNhanSelectedItem = ToChucCaNhans.FirstOrDefault(x => x.Id == selectedItem.ToChucCaNhanId);
                NguonGenselectedItem = NguonGens.FirstOrDefault(x => x.Id == selectedItem.NguonGenId);


            }

        }
     
        private void OnNguonGenselected(NguonGen selectedItem)
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

        private List<ToChucNguonGen> GetToChucNguonGens()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var data = db.ToChucNguonGens.AsNoTracking().Include(c => c.NguonGen).Include(c => c.ToChucCaNhan).Where(x => x.HoatDong.Equals(_active)).ToList();
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

                var ToChucNguonGens = GetToChucNguonGens()
                  .Where(x =>
                      (x.ToChucCaNhan?.Ten?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.NguonGen?.TenNguonGen?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.NgayThuThap?.ToString().ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                      || (x.KhuVuc?.ToLower().Contains(textSearch.ToLower()) ?? false)
                   )
                  .ToList();


                LoadTableList(ToChucNguonGens);


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
                    if (ToChucCaNhanSelectedItem?.Id == null || string.IsNullOrEmpty(GenSource) )  {
                        MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
                        return;
                    }
                    int? idGen = null;
                    if(NguonGenselectedItem?.Id != null)
                    {
                        idGen = NguonGenselectedItem.Id;


                        var check = db.ToChucNguonGens.FirstOrDefault(x => x.HoatDong == _active && x.NguonGenId == idGen && x.ToChucCaNhanId == ToChucCaNhanSelectedItem.Id);
                        if (check != null)
                        {
                            MessageBox.Show("Dữ liệu có sãn hãy [ sửa hoặc xóa]");
                            return;
                        }
                    }
                    else
                    {
                        var ng = new NguonGen { TenNguonGen = GenSource };
                        db.NguonGens.Add(ng);
                        db.SaveChanges();

                        idGen = ng.Id;
                    }

                    if (idGen != null) { 
                        var tcng = new ToChucNguonGen { ToChucCaNhanId = ToChucCaNhanSelectedItem.Id, NguonGenId = idGen.Value,KhuVuc = NewArea, NgayThuThap = NewStartDate, HoatDong = _active };
                        db.ToChucNguonGens.Add(tcng);
                        db.SaveChanges();

                        //Reset list
                                var data = GetToChucNguonGens();
                        LoadTableList(data);

                        RowSelectedItem = null;
                        NguonGenselectedItem = null;
                        ToChucCaNhanSelectedItem = null;

                        NewArea = "";

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
                    if (ToChucCaNhanSelectedItem?.Id == null || string.IsNullOrEmpty(GenSource) )
                    {
                        MessageBox.Show("Hãy nhập đầy đủ dữ liệu");
                        return;
                    }
                    if (RowSelectedCommand != null)
                    {
                        int? idGen = null;
                        if (NguonGenselectedItem?.Id != null)
                        {
                            idGen = NguonGenselectedItem.Id;

                        }
                        else
                        {
                            var ng = new NguonGen { TenNguonGen = GenSource };
                            db.NguonGens.Add(ng);
                            db.SaveChanges();

                            idGen = ng.Id;
                        }

                        if (idGen != null)
                        {
                            var tcng = db.ToChucNguonGens.Where(x => x.Id == RowSelectedItem.Id).FirstOrDefault();
                            if (tcng != null)
                            {
                                tcng.ToChucCaNhanId = ToChucCaNhanSelectedItem.Id;
                                tcng.NguonGenId = idGen.Value;
                                tcng.KhuVuc = NewArea;
                                tcng.NgayThuThap = NewStartDate;

                                db.SaveChanges();


                                //Reset list
                                var data = GetToChucNguonGens();
                                LoadTableList(data);

                                RowSelectedItem = ToChucNguonGens.FirstOrDefault(x=> x.Id == tcng.Id);
                            }

                        }
                        


                    }
                    else
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
                        var gvnBt = db.ToChucNguonGens.Where(x => x.Id == RowSelectedItem.Id).FirstOrDefault();
                        db.Remove(gvnBt);
                        db.SaveChanges();

                        // Reset list
                        var data = GetToChucNguonGens();
                        LoadTableList(data);

                        RowSelectedItem = null;
                        NguonGenselectedItem = null;
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

        private void LoadTableList(List<ToChucNguonGen> data)
        {
            ToChucNguonGens.Clear();
            foreach (var item in data)
            {
                ToChucNguonGens.Add(item);
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
