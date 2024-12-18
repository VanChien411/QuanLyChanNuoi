using Microsoft.Win32;
using OfficeOpenXml;
using System.Collections.Generic;
using System;
using System.IO;
using System.Windows;

namespace QuanLyGiong_ThucAnChanNuoi.Helps
{
    internal class ExcelExport
    {
        public void ExcelExportT<T>(IEnumerable<T> data)
        {
            // 1. Hiển thị hộp thoại "Lưu file"
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Save an Excel File",
                FileName = "ExportedData.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // 2. Tạo file Excel
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage())
                    {
                        // Thêm worksheet
                        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        // 3. Lấy danh sách thuộc tính của đối tượng T
                        var properties = typeof(T).GetProperties();

                        // 4. Ghi tiêu đề cột (dựa trên tên thuộc tính)
                        for (int i = 0; i < properties.Length; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = properties[i].Name;
                        }

                        // 5. Ghi dữ liệu
                        int row = 2; // Bắt đầu từ dòng thứ 2 (dòng 1 là tiêu đề)
                        foreach (var item in data)
                        {
                            for (int col = 0; col < properties.Length; col++)
                            {
                                var value = properties[col].GetValue(item);
                                worksheet.Cells[row, col + 1].Value = value?.ToString();
                            }
                            row++;
                        }

                        // 6. Lưu file vào vị trí người dùng chọn
                        var filePath = saveFileDialog.FileName;
                        File.WriteAllBytes(filePath, package.GetAsByteArray());

                        // 7. Thông báo thành công
                        MessageBox.Show("File exported and saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
