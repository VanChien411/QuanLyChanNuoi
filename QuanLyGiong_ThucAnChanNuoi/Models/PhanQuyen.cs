using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{

    [Table("phan_quyen")]
    public partial class PhanQuyen
    {
        [Key]
        [StringLength(20)]
        public string MaQuyen { get; set; }

        [Column("ten_quyen")]
        [StringLength(30)]
        public string TenQuyen { get; set; }

        [Column("mota")]
        [StringLength(50)]
        public string Mota { get; set; }
        // Mối quan hệ nhiều-nhiều qua bảng trung gian
        public virtual ICollection<PhanQuyenNguoiDung> PhanQuyenNguoiDungs { get; set; }
        // Mối quan hệ nhiều-nhiều qua bảng trung gian
        public virtual ICollection<PhanQuyenNhom> PhanQuyenNhoms { get; set; }

    }
}
