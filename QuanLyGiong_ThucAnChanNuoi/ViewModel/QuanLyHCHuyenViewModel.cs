using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

using System.Windows.Input;


namespace QuanLyGiong_ThucAnChanNuoi.ViewModel
{
    public  class QuanLyHCHuyenViewModel: INotifyPropertyChanged
    {
        private string _newTen;
        private int _newTrucThuoc;
        private string _newMaBuuDien;

        public ObservableCollection<DonViHc> DonViHcs { get; set; } = new ObservableCollection<DonViHc>();


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
        public ICommand AddProductCommand { get; }

        public QuanLyHCHuyenViewModel()
        {
            Initialize();
            AddProductCommand = new RelayCommand(AddProduct);
        }
        private  void Initialize()
        {
            try
            {
                // EnsureDatabaseCreated();
                 Load();
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
        private  void Load()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {
                    var donViHcs =  db.DonViHcs.ToList();
                    DonViHcs = new ObservableCollection<DonViHc>(donViHcs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private  void AddProduct()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {
                   //var donViHc = new DonViHc { Ten = NewTen, }
                   // db.Products.Add(product);
                   //  db.SaveChanges();

                   // Products.Add(product);

                   // // Clear inputs
                   // NewProductName = string.Empty;
                   // NewProductPrice = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm: {ex.Message}");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
