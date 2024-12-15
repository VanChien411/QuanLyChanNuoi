using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("GiongCanBaoTon")]
    public partial class GiongCanBaoTon
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("GiongID")]
        public int GiongId { get; set; }

        [Required]
        [StringLength(50)]
        public string Loai { get; set; }

        [StringLength(200)]
        public string LyDo { get; set; }
        public DateTime? NgayBaoTon { get; set; }
        public bool? TrangThai { get; set; }
        [ForeignKey("GiongId")]
        [InverseProperty("GiongCanBaoTons")]
        public virtual GiongVatNuoi Giong { get; set; }

        public string TrangThaiHienThi => TrangThai.HasValue && TrangThai.Value ? "Đang bảo tồn" : "Hết bảo tồn";
    }
}

