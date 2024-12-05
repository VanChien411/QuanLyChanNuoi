using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("NguyenLieuChoPhep")]
    public partial class NguyenLieuChoPhep
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenNguyenLieu { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        [InverseProperty("NguyenLieu")]
        public virtual ICollection<CoSoNguyenLieuChoPhep> CoSoNguyenLieuChoPheps { get; set; } = new List<CoSoNguyenLieuChoPhep>();
    }
}

