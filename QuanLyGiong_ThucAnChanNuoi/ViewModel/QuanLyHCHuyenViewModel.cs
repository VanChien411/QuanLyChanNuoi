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
    public  class QuanLyHCHuyenViewModel: INotifyPropertyChanged
    {
        private int _idTinh = 1;
        private int _idHuyen = 2;

        private string _newDistrictName;
        private int _newTrucThuoc;
        private string _newMaBuuDien;
        private string _newTinh;
        private string _newTextSearch;

        private string _textComboBox;
        private DonViHc _selectedItem;
        private DonViHc _districtSelected;

        public ObservableCollection<DonViHc> DonViHcs { get; set; } = new ObservableCollection<DonViHc>();
        public ObservableCollection<DonViHc> Huyens { get; set; } = new ObservableCollection<DonViHc>();
        public ObservableCollection<DonViHc> Tinhs { get; set; } = new ObservableCollection<DonViHc>();


        public string NewTextSearch
        {
            get => _newTextSearch;
            set
            {
                _newTextSearch = value;
                OnPropertyChanged(nameof(NewTextSearch)); // Notify UI to update binding
            }
        }
        public string Text
        {
            get => _textComboBox;
            set
            {
                _textComboBox = value;
                OnPropertyChanged(nameof(Text)); // Notify UI to update binding
            }
        }
        public DonViHc SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (value != null)
                {
                    // Khi có SelectedItem, cập nhật Text để khớp với item
                    Text = (value as DonViHc)?.Ten; // Thay 'YourItemType' bằng kiểu object trong ItemsSource
                }
             
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public DonViHc DistrictSelected
        {
            get => _districtSelected;
            set
            {
                _districtSelected = value;
                if (value != null)
                {
                    // Khi có SelectedItem, cập nhật Text để khớp với item
                    NewDistrictName = (value as DonViHc)?.Ten; // Thay 'YourItemType' bằng kiểu object trong ItemsSource
                }

                OnPropertyChanged(nameof(DistrictSelected));
            }
        }
        public string NewDistrictName
        {
            get => _newDistrictName;
            set
            {
                _newDistrictName = value;
                OnPropertyChanged(nameof(NewDistrictName));
            }
        }

        public int NewTrucThuoc
        {
            get => _newTrucThuoc;
            set
            {
                _newTrucThuoc = value;
                OnPropertyChanged(nameof(NewTrucThuoc));
            }
        }
        public string NewMaBuuDien
        {
            get => _newMaBuuDien;
            set
            {
                _newMaBuuDien = value;
                OnPropertyChanged(nameof(NewMaBuuDien));
            }
        }
        public string NewTinh
        {
            get => _newTinh;
            set
            {
                _newTinh = value;
                OnPropertyChanged(nameof(NewTinh));
            }
        }

        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand DistrictSelectionChangedCommand { get; }
        public ICommand SelectionChangedCommand { get; }


        public QuanLyHCHuyenViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            DistrictSelectionChangedCommand = new RelayCommandT<object>(OnDistrictSelectionChanged);
            SelectionChangedCommand = new RelayCommandT<object>(OnSelectionChanged);
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
                    // Dùng AsNoTracking nếu chỉ đọc
                    var donViHcs = db.DonViHcs.AsNoTracking().Include(c => c.TrucThuocNavigation).ToList();
                    DonViHcs = new ObservableCollection<DonViHc>(donViHcs);

                    //CapHcId == 1 la thanh pho cho select
                    var tinhs =  db.DonViHcs.AsNoTracking().Where(x => x.CapHcId == _idTinh).ToList();
                    Tinhs = new ObservableCollection<DonViHc>(tinhs);

                    var huyens = db.DonViHcs.AsNoTracking().Include(c => c.TrucThuocNavigation).Where(x => x.CapHcId == _idHuyen).ToList();
                    Huyens = new ObservableCollection<DonViHc>(huyens);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private async void Search()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {
                    var textSearch = NewTextSearch.ToLower();
                    var donViHcs = await db.DonViHcs.Include(c => c.TrucThuocNavigation)
                        .Where(x => x.Ten.ToLower().Contains(textSearch)
                        || x.MaBuuDien.ToLower().Contains(textSearch)
                        || x.TrucThuocNavigation.Ten.ToLower().Contains(textSearch))
                            .ToListAsync();

                    LoadTableList(donViHcs);


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
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {
                    int? idTrucThuoc  = null;
                    if(DistrictSelected != null)
                    {
                        MessageBox.Show($"Huyện tồn tại [ sửa hoặc xóa]");
                        return;
                    }
                    if(SelectedItem != null)
                    {
                        idTrucThuoc = SelectedItem.Id;
                    }
                    else if ( string.IsNullOrEmpty(_textComboBox) != true)
                    {
                        var newTrucThuoc = new DonViHc
                        {
                            Ten = _textComboBox,
                            CapHcId = _idTinh
                        };

                        // Thêm vào DbContext
                        await db.AddAsync(newTrucThuoc);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        await db.SaveChangesAsync();

                        // Lấy ID của thực thể mới
                        idTrucThuoc = newTrucThuoc.Id;
                    }
                    if( !string.IsNullOrEmpty(NewDistrictName))
                    {
                        var donViHc = new DonViHc { Ten = NewDistrictName, TrucThuoc = idTrucThuoc , CapHcId = _idHuyen, MaBuuDien = NewMaBuuDien };
                        await db.AddAsync(donViHc);
                        await db.SaveChangesAsync();

                        // Reset list
                        var donViHcs = db.DonViHcs.AsNoTracking().Include(c => c.TrucThuocNavigation).ToList();

                        LoadTableList(donViHcs);

                        NewDistrictName = string.Empty;
                        Text = string.Empty;
                        NewMaBuuDien = string.Empty;
               
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
                  
                    if (DistrictSelected != null) {

                        //Kiểm tra tỉnh có chưa
                        int? idTrucThuoc = null;
                        if (SelectedItem != null)
                        {
                            idTrucThuoc = SelectedItem.Id;
                        }
                        else if (string.IsNullOrEmpty(_textComboBox) != true)
                        {
                            var newTrucThuoc = new DonViHc
                            {
                                Ten = _textComboBox,
                                CapHcId = _idTinh
                            };

                            // Thêm vào DbContext
                            await db.AddAsync(newTrucThuoc);

                            // Lưu thay đổi vào cơ sở dữ liệu
                            await db.SaveChangesAsync();

                            // Lấy ID của thực thể mới
                            idTrucThuoc = newTrucThuoc.Id;
                        }

                        // Sửa thông tin huyện
                        var donViHc = await db.DonViHcs.FirstOrDefaultAsync(x => x.Id == DistrictSelected.Id );

                        if (donViHc != null) {
                    
                            donViHc.TrucThuoc = idTrucThuoc;
                            donViHc.MaBuuDien = NewMaBuuDien;

                            // Lưu thay đổi vào cơ sở dữ liệu
                            await db.SaveChangesAsync();

                            // Reset list
                            var donViHcs = db.DonViHcs.Include(c => c.TrucThuocNavigation).AsNoTracking().ToList();
                            LoadTableList(donViHcs);
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
                    if (DistrictSelected != null)
                    {
                        var donViHc = await db.DonViHcs.FirstOrDefaultAsync(x => x.Id == DistrictSelected.Id);
                        if (donViHc != null) { 
                            //Phần này ko dùng async
                            db.Remove(donViHc);
                            db.SaveChanges();

                            // Reset list
                            var donViHcs = db.DonViHcs.AsNoTracking().Include(c => c.TrucThuocNavigation).ToList();
                            LoadTableList(donViHcs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}");
            }
        }

        private void LoadTableList( List<DonViHc> donViHcs)
        {
            DonViHcs.Clear();
            foreach (var item in donViHcs)
            {
                DonViHcs.Add(item);
            }
        }
        // Hàm xử lý sự kiện khi selection thay đổi
        private void OnSelectionChanged(object selectedItem)
        {
            // Xử lý logic khi người dùng thay đổi lựa chọn
            if (selectedItem != null)
            {
              
            }
        }
        private void OnDistrictSelectionChanged(object selectedItem)
        {
            // Xử lý logic khi người dùng thay đổi lựa chọn
            if (selectedItem != null)
            {
                try
                {
                    SelectedItem = DistrictSelected.TrucThuocNavigation;
                    NewMaBuuDien = DistrictSelected.MaBuuDien;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi khởi tạo: {ex.Message}");
                }
            }
        }
        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (myComboBox.SelectedItem != null)
            //{
            //    // Lấy giá trị của item được chọn
            //    string selectedValue = (myComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            //    MessageBox.Show($"Selected: {selectedValue}");
            //}
            //else
            //{
            //    // Lấy giá trị do người dùng nhập
            //    string enteredValue = myComboBox.Text;
            //    MessageBox.Show($"Entered: {enteredValue}");
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
