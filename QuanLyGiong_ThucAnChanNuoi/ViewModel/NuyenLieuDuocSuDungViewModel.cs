﻿using Microsoft.EntityFrameworkCore;
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
  internal class NuyenLieuDuocSuDungViewModel : INotifyPropertyChanged
    {

        private int _newId;
        private string _newFullName;
        private string _NewNote;


        private string _newTextSearch;

        private Models.NguyenLieuChoPhep _rowSelectedItem;
        private ToChucCaNhan _toChucCaNhanSelectedItem;

        private NguonGen _NguonGen;



        public ObservableCollection<Models.NguyenLieuChoPhep> NguyenLieuChoPheps { get; set; } = new ObservableCollection<Models.NguyenLieuChoPhep>();
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

        public Models.NguyenLieuChoPhep RowSelectedItem
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

        public string NewNote
        {
            get => _NewNote;
            set
            {
                _NewNote = value;
                OnPropertyChanged(nameof(NewNote));
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



        public NuyenLieuDuocSuDungViewModel()
        {
            Initialize();
            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            SearchCommand = new RelayCommand(Search);
            RowSelectedCommand = new RelayCommandT<Models.NguyenLieuChoPhep>(OnRowSelected);
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
               
                    var rowData = GetNguyenLieuChoPheps();
                    //Gan gia tri cho table list
                    NguyenLieuChoPheps = new ObservableCollection<Models.NguyenLieuChoPhep>(rowData);

                    var nguonGens = db.NguonGens.ToList();
                    NguonGens = new ObservableCollection<NguonGen>(nguonGens);
                    var toChucCaNhan = db.ToChucCaNhans.ToList();
                    ToChucCaNhans = new ObservableCollection<ToChucCaNhan>(toChucCaNhan);



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }
        private void OnRowSelected(Models.NguyenLieuChoPhep selectedItem)
        {
            if (selectedItem != null)
            {

                ////Thực hiện hành động với SelectedItem
                NewId = selectedItem.Id;
                NewNote = selectedItem.MoTa;
                NewFullName = selectedItem.TenNguyenLieu;


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

        private List<Models.NguyenLieuChoPhep> GetNguyenLieuChoPheps()
        {
            try
            {
                using (var db = new QuanLyGiongVaThucAnChanNuoiContext())
                {

                    var data = db.NguyenLieuChoPheps.AsNoTracking().Include(c => c.CoSoNguyenLieuChoPheps).ToList();
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

                var NguyenLieuChoPheps = GetNguyenLieuChoPheps()
                  .Where(x =>
                       (x.TenNguyenLieu?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.MoTa?.ToLower().Contains(textSearch.ToLower()) ?? false)
                      || (x.Id.ToString().ToLower().Contains(textSearch.ToLower()))
                   )
                  .ToList();


                LoadTableList(NguyenLieuChoPheps);


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
                    if (!string.IsNullOrEmpty(NewFullName))
                    {
                        var hcc = new Models.NguyenLieuChoPhep
                        {
                            TenNguyenLieu = NewFullName,
                            MoTa = NewNote
                        };
                        db.NguyenLieuChoPheps.Add(hcc);
                        db.SaveChanges();

                        //Reset list
                        var data = GetNguyenLieuChoPheps();
                        LoadTableList(data);

                        NewFullName = "";
                        NewNote = "";
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
                    if (RowSelectedItem != null)
                    {
                        if (!string.IsNullOrEmpty(NewFullName))
                        {
                            var hcc = db.NguyenLieuChoPheps.FirstOrDefault(x => x.Id == RowSelectedItem.Id);
                            if (hcc != null)
                            {
                                hcc.TenNguyenLieu = NewFullName;
                                hcc.MoTa = NewNote;

                                db.SaveChanges();

                                //Reset list
                                var data = GetNguyenLieuChoPheps();
                                LoadTableList(data);

                                RowSelectedItem = NguyenLieuChoPheps.FirstOrDefault(x => x.Id == hcc.Id);
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
                        var gvnBt = db.NguyenLieuChoPheps.Where(x => x.Id == RowSelectedItem.Id).FirstOrDefault();
                        db.Remove(gvnBt);
                        db.SaveChanges();

                        // Reset list
                        var data = GetNguyenLieuChoPheps();
                        LoadTableList(data);

                        RowSelectedItem = null;

                        NewFullName = "";
                        NewNote = "";
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xoa: {ex.Message}");
            }
        }

        private void LoadTableList(List<Models.NguyenLieuChoPhep> data)
        {
            NguyenLieuChoPheps.Clear();
            foreach (var item in data)
            {
                NguyenLieuChoPheps.Add(item);
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

