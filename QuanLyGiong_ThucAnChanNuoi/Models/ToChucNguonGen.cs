using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{

    [Table("ToChucNguonGen")]
    public partial class ToChucNguonGen
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("ToChucCaNhanID")]
        public int ToChucCaNhanId { get; set; }

        [Column("NguonGenID")]
        public int NguonGenId { get; set; }

        [StringLength(40)]
        public string KhuVuc { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? NgayThuThap { get; set; }

        [Required]
        [StringLength(50)]
        public string HoatDong { get; set; }

        [ForeignKey("NguonGenId")]
        [InverseProperty("ToChucNguonGens")]
        public virtual NguonGen NguonGen { get; set; }

        [ForeignKey("ToChucCaNhanId")]
        [InverseProperty("ToChucNguonGens")]
        public virtual ToChucCaNhan ToChucCaNhan { get; set; }
    }
}
