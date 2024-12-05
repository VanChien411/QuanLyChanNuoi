using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace QuanLyGiong_ThucAnChanNuoi.Models
{

    [Table("Nhom")]
    public partial class Nhom
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        [Column("mota")]
        [StringLength(50)]
        public string Mota { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? NgayTao { get; set; }

        [InverseProperty("Nhom")]
        public virtual ICollection<ThanhVienNhom> ThanhVienNhoms { get; set; } = new List<ThanhVienNhom>();


        // Mối quan hệ nhiều-nhiều qua bảng trung gian
        public virtual ICollection<PhanQuyenNhom> PhanQuyenNhoms { get; set; }
    }
}
