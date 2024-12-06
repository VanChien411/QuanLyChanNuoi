using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;


namespace QuanLyGiong_ThucAnChanNuoi.ViewModel
{
    public  class QuanLyHCHuyenViewModel: INotifyPropertyChanged
    {
        private int _idTinh = 1;
        private int _idHuyen = 2;

        private string _newTen;
        private int _newTrucThuoc;
        private string _newMaBuuDien;
        private string _newTinh;

        private string _textComboBox;
        private DonViHc _selectedItem;
        public ObservableCollection<DonViHc> DonViHcs { get; set; } = new ObservableCollection<DonViHc>();
        public ObservableCollection<DonViHc> Tinhs { get; set; } = new ObservableCollection<DonViHc>();

   
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
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public string NewTen
        {
            get => _newTen;
            set
            {
                _newTen = value;
                OnPropertyChanged(nameof(NewTen));
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
        public ICommand SelectionChangedCommand { get; }
        public QuanLyHCHuyenViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            SelectionChangedCommand = new RelayCommandT<object>(OnSelectionChanged);
        }
        private void Initialize()
        {
            try
            {
                // EnsureDatabaseCreated();
                LoadAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi khởi tạo: {ex.Message}");
            }
        }

        private  void EnsureDatabaseCreated()
        {
            using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
            {
                if (! db.Database.CanConnect())
                {
                    throw new InvalidOperationException("Không thể kết nối cơ sở dữ liệu.");
                }
                 db.Database.EnsureCreated();
            }
        }
        private void LoadAsync()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {
                    // Dùng AsNoTracking nếu chỉ đọc
                    var donViHcs = db.DonViHcs.AsNoTracking().ToList();
                    DonViHcs = new ObservableCollection<DonViHc>(donViHcs);

                    //CapHcId == 1 la thanh pho
                    var tinhs =  db.DonViHcs.AsNoTracking().Where(x => x.CapHcId == _idTinh).ToList();
                    Tinhs = new ObservableCollection<DonViHc>(tinhs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private void AddItem()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {
                    int idTrucThuoc = -1;
                    if(SelectedItem != null)
                    {
                        idTrucThuoc = SelectedItem.Id;
                    }
                    else if ( string.IsNullOrEmpty(_textComboBox) != true)
                    {
                        var newTrucThuoc = new DonViHc { Ten = _textComboBox, CapHcId = _idTinh };
                        db.AddAsync(newTrucThuoc);
                        db.SaveChangesAsync();

                        idTrucThuoc = newTrucThuoc.Id;
                    }
                    if(idTrucThuoc != -1 && !string.IsNullOrEmpty(NewTen))
                    {
                        var donViHc = new DonViHc { Ten = NewTen, TrucThuoc = idTrucThuoc, CapHcId = _idHuyen, MaBuuDien = NewMaBuuDien };
                        db.AddAsync(donViHc);
                        db.SaveChangesAsync();

                        DonViHcs.Add(donViHc);
                        NewTen = string.Empty;
                        Text = string.Empty;
                        NewMaBuuDien = string.Empty;
               
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm: {ex.Message}");
            }
        }
        // Hàm xử lý sự kiện khi selection thay đổi
        private void OnSelectionChanged(object selectedItem)
        {
            // Xử lý logic khi người dùng thay đổi lựa chọn
            if (selectedItem != null)
            {
                // Ví dụ, in ra giá trị của item được chọn
                Console.WriteLine($"Item selected: {selectedItem}");
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
