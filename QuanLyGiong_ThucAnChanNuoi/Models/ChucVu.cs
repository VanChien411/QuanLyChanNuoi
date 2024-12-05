using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("ChucVu")]
    public partial class ChucVu
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string TenChucVu { get; set; }

        [Column("mota")]
        [StringLength(100)]
        public string Mota { get; set; }

        [InverseProperty("ChucVu")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
    }
}

