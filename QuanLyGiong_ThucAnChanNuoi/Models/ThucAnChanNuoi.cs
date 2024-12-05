using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("ThucAnChanNuoi")]
    public partial class ThucAnChanNuoi
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenThucAn { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiThucAn { get; set; }

        [InverseProperty("ThucAnChanNuoi")]
        public virtual ICollection<CoSoThucAn> CoSoThucAns { get; set; } = new List<CoSoThucAn>();
    }
}

