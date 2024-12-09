using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGiong_ThucAnChanNuoi.ViewModel
{
    public class QuanLyHCXaViewModel : INotifyPropertyChanged
    {
        private int _idHuyen = 2;
        private int _idXa = 3;

        private string _newDistrictName;
        private int _newTrucThuoc;
        private string _newMaBuuDien;
        private string _newTinh;
        private string _newTextSearch;

        private string _textComboBox;
        private DonViHc _selectedItem;
        private DonViHc _districtSelected;

        public ObservableCollection<DonViHc> DonViHcs { get; set; } = new ObservableCollection<DonViHc>();
        public ObservableCollection<DonViHc> Xas { get; set; } = new ObservableCollection<DonViHc>();
        public ObservableCollection<DonViHc> Huyens { get; set; } = new ObservableCollection<DonViHc>();


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
                if (_selectedItem?.Ten != value) { 
                    SelectedItem = null;
                }
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
                if (_districtSelected?.Ten != value)
                {
                    DistrictSelected = null;
                }
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


        public QuanLyHCXaViewModel()
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
                    //CapHcId == 1 la thanh pho cho select
                    var huyens = db.DonViHcs.AsNoTracking().Where(x => x.CapHcId == _idHuyen).ToList();
                    Huyens = new ObservableCollection<DonViHc>(huyens);

                    var xas = GetDonViHcs();
                    Xas = new ObservableCollection<DonViHc>(xas);

                    //Gan gia tri cho table list
                    DonViHcs = new ObservableCollection<DonViHc>(xas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private List<DonViHc> GetDonViHcs()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var donViHcs = db.DonViHcs.AsNoTracking().Include(c => c.TrucThuocNavigation).Where(x => x.CapHcId == _idXa).ToList();
                    return donViHcs;
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

                var textSearch = NewTextSearch.ToLower();
                // Sử dụng && để kiểm tra x.MaBuuDien != null trước khi gọi ToLower().Contains().

                var donViHcs = GetDonViHcs()
                    .Where(x => x.Ten.ToLower().Contains(textSearch)
                        || (x.MaBuuDien != null && x.MaBuuDien.ToLower().Contains(textSearch))
                        || (x.TrucThuocNavigation != null && x.TrucThuocNavigation.Ten.ToLower().Contains(textSearch)))
                    .ToList();

                LoadTableList(donViHcs);


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
                    int? idTrucThuoc = null;
                    if (DistrictSelected != null)
                    {
                        MessageBox.Show($"Huyện tồn tại [ sửa hoặc xóa]");
                        return;
                    }
                    if (SelectedItem != null)
                    {
                        idTrucThuoc = SelectedItem.Id;
                    }
                    else if (string.IsNullOrEmpty(_textComboBox) != true)
                    {
                        var newTrucThuoc = new DonViHc
                        {
                            Ten = _textComboBox,
                            CapHcId = _idHuyen
                        };

                        // Thêm vào DbContext
                        await db.AddAsync(newTrucThuoc);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        await db.SaveChangesAsync();

                        // Lấy ID của thực thể mới
                        idTrucThuoc = newTrucThuoc.Id;
                    }
                    if (!string.IsNullOrEmpty(NewDistrictName))
                    {
                        var donViHc = new DonViHc { Ten = NewDistrictName, TrucThuoc = idTrucThuoc, CapHcId = _idXa, MaBuuDien = NewMaBuuDien };
                        await db.AddAsync(donViHc);
                        await db.SaveChangesAsync();

                        // Reset list
                        var donViHcs = GetDonViHcs();

                        LoadTableList(donViHcs);
                        LoadSelect();

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

                    if (DistrictSelected != null)
                    {

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
                                CapHcId = _idHuyen
                            };

                            // Thêm vào DbContext
                            await db.AddAsync(newTrucThuoc);

                            // Lưu thay đổi vào cơ sở dữ liệu
                            await db.SaveChangesAsync();

                            // Lấy ID của thực thể mới
                            idTrucThuoc = newTrucThuoc.Id;
                        }

                        // Sửa thông tin huyện
                        var donViHc = await db.DonViHcs.FirstOrDefaultAsync(x => x.Id == DistrictSelected.Id);

                        if (donViHc != null)
                        {

                            donViHc.TrucThuoc = idTrucThuoc;
                            donViHc.MaBuuDien = NewMaBuuDien;

                            // Lưu thay đổi vào cơ sở dữ liệu
                            await db.SaveChangesAsync();

                            // Reset list
                            var donViHcs = GetDonViHcs();
                            LoadTableList(donViHcs);
                            LoadSelect();

                            NewDistrictName = string.Empty;
                            Text = string.Empty;
                            NewMaBuuDien = string.Empty;

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
                        if (donViHc != null)
                        {
                            //Phần này ko dùng async
                            db.Remove(donViHc);
                            db.SaveChanges();

                            // Reset list
                            var donViHcs = GetDonViHcs();
                            LoadTableList(donViHcs);
                            LoadSelect();

                            NewDistrictName = string.Empty;
                            Text = string.Empty;
                            NewMaBuuDien = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}");
            }
        }

        private void LoadTableList(List<DonViHc> donViHcs)
        {
            DonViHcs.Clear();
            foreach (var item in donViHcs)
            {
                DonViHcs.Add(item);
            }
        }
        private void LoadSelect() {
            LoadXaselect();
            LoadHuyenSelect();
        }
        private void LoadXaselect()
        {
            var donViHcs = GetDonViHcs();
            Xas.Clear();
            foreach (var item in donViHcs)
            {
                Xas.Add(item);
            }
        }
        private void LoadHuyenSelect()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var donViHcs = db.DonViHcs.AsNoTracking().Include(c => c.TrucThuocNavigation).Where(x => x.CapHcId == _idHuyen).ToList();
                    Huyens.Clear();
                    foreach (var item in donViHcs)
                    {
                        Huyens.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
