using Microsoft.EntityFrameworkCore;
using QuanLyGiong_ThucAnChanNuoi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using OfficeOpenXml;
using System.IO;
using Microsoft.Win32;
using QuanLyGiong_ThucAnChanNuoi.Helps;
namespace QuanLyGiong_ThucAnChanNuoi.ViewModel
{
   internal class QuanLyLichSuViewModel : INotifyPropertyChanged
    {

        private int _newId;



        private string _newTextSearch;

        private Models.LichSuTruyCap _rowSelectExportem;
     


        public ObservableCollection<Models.LichSuTruyCap> LichSuTruyCaps { get; set; } = new ObservableCollection<Models.LichSuTruyCap>();


        public string NewTextSearch
        {
            get => _newTextSearch;
            set
            {
                _newTextSearch = value;
                OnPropertyChanged(nameof(NewTextSearch)); // Notify UI to update binding
            }
        }

        public Models.LichSuTruyCap RowSelectExportem
        {
            get => _rowSelectExportem;
            set
            {
                _rowSelectExportem = value;
                OnPropertyChanged(nameof(RowSelectExportem));
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

        public ICommand AddItemCommand { get; }
        public ICommand ExportItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand RowSelectedCommand { get; set; }



        public QuanLyLichSuViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            ExportItemCommand = new RelayCommand(ExportItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<Models.LichSuTruyCap>(OnRowSelected);
        

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
                    
                    var rowData = GetLichSuTruyCaps();
                    //Gan gia tri cho table list
                    LichSuTruyCaps = new ObservableCollection<Models.LichSuTruyCap>(rowData);

                


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private void OnRowSelected(Models.LichSuTruyCap selectExportem)
        {
            if (selectExportem != null)
            {

                ////Thực hiện hành động với SelectExportem
                //NewId = selectExportem.Id;
                //NewNote = selectExportem.LyDoCam;
                //NewFullName = selectExportem.TenHoaChat;


            }

        }


        private List<Models.LichSuTruyCap> GetLichSuTruyCaps()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var data = db.LichSuTruyCaps.AsTracking().Include(c => c.NguoiDung).ToList();
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

                var LichSuTruyCaps = GetLichSuTruyCaps()
                  .Where(x =>
                       (x.NguoiDung?.TenDn?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Mota?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.ThoiGian?.ToString().ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.IpAddress?.ToString().ToLower().Contains(textSearch.ToLower()) ?? false)

                      || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                   )
                  .ToList();


                LoadTableList(LichSuTruyCaps);


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
      

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}");
            }
        }
        private async void ExportItem()
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xuất báo cáo không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                    {
                      var lstc = db.LichSuTruyCaps.ToList();

                        ExcelExport excelExport = new ExcelExport();
                        excelExport.ExcelExportT(lstc);

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xuất báo cáo : {ex.Message}");
                }

            }
        }
        private async void DeleteItem()
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ lịch sử truy cập không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                    {

            
                            var lstc = db.LichSuTruyCaps.ToList();

                            db.RemoveRange(lstc);
                            db.SaveChanges();

                            // Reset list
                            var data = GetLichSuTruyCaps();
                            LoadTableList(data);

                            MessageBox.Show("Đã xóa toàn bộ lịch sử!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xoa: {ex.Message}");
                }
              
            }
         
        }

        private void LoadTableList(List<Models.LichSuTruyCap> data)
        {
            LichSuTruyCaps.Clear();
            foreach (var item in data)
            {
                LichSuTruyCaps.Add(item);
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
