using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("NguonGen")]
    public partial class NguonGen
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenNguonGen { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        [InverseProperty("NguonGen")]
        public virtual ICollection<ToChucNguonGen> ToChucNguonGens { get; set; } = new List<ToChucNguonGen>();
    }
}

