using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("GiongVatNuoi")]
    public partial class GiongVatNuoi
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenGiong { get; set; }

        [StringLength(100)]
        public string MoTa { get; set; }

        [Required]
        [StringLength(40)]
        public string Loai { get; set; }

        [InverseProperty("GiongVatNuoi")]
        public virtual ICollection<CoSoVatNuoi> CoSoVatNuois { get; set; } = new List<CoSoVatNuoi>();

        [InverseProperty("Giong")]
        public virtual ICollection<GiongCanBaoTon> GiongCanBaoTons { get; set; } = new List<GiongCanBaoTon>();
    }
}

