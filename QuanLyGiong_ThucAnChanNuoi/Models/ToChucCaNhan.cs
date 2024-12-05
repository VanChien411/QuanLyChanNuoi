using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace QuanLyGiong_ThucAnChanNuoi.Models
{

    [Table("ToChucCaNhan")]
    public partial class ToChucCaNhan
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string LoaiHinh { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiHoatDong { get; set; }

        [InverseProperty("ToChucCaNhan")]
        public virtual ICollection<CoSoThucAn> CoSoThucAns { get; set; } = new List<CoSoThucAn>();

        [InverseProperty("ToChucCaNhan")]
        public virtual ICollection<CoSoVatNuoi> CoSoVatNuois { get; set; } = new List<CoSoVatNuoi>();

        [InverseProperty("ToChucCaNhan")]
        public virtual ICollection<ToChucNguonGen> ToChucNguonGens { get; set; } = new List<ToChucNguonGen>();
    }
}
