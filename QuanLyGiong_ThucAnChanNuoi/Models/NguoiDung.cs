using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string HoTen { get; set; }

        [Required]
        [Column("TenDN")]
        [StringLength(40)]
        public string TenDn { get; set; }

        [StringLength(30)]
        public string MatKhau { get; set; }

        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("trang_thai")]
        public bool? TrangThai { get; set; }

        [Column("ChucVu_ID")]
        public int? ChucVuId { get; set; }

        [Column("DonVi_HC_ID")]
        public int? DonViHcId { get; set; }

        [ForeignKey("ChucVuId")]
        [InverseProperty("NguoiDungs")]
        public virtual ChucVu ChucVu { get; set; }

        [ForeignKey("DonViHcId")]
        [InverseProperty("NguoiDungs")]
        public virtual DonViHc DonViHc { get; set; }

        [InverseProperty("NguoiDung")]
        public virtual ICollection<ThanhVienNhom> ThanhVienNhoms { get; set; } = new List<ThanhVienNhom>();
        [InverseProperty("NguoiDung")]
        public virtual ICollection<LichSuTruyCap> LichSuTruyCaps { get; set; } = new List<LichSuTruyCap>();

        // Mối quan hệ nhiều-nhiều qua bảng trung gian
        public virtual ICollection<PhanQuyenNguoiDung> PhanQuyenNguoiDungs { get; set; }
    }
}

