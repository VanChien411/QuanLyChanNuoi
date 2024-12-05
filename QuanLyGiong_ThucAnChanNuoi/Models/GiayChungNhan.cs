using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models {
    [Table("GiayChungNhan")]
    public partial class GiayChungNhan
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CoSoThucAnID")]
        public int CoSoThucAnId { get; set; }

        [Required]
        [StringLength(50)]
        public string SoGiayChungNhan { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayCap { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayHetHan { get; set; }

        [StringLength(50)]
        public string NoiCap { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        [ForeignKey("CoSoThucAnId")]
        [InverseProperty("GiayChungNhans")]
        public virtual CoSoThucAn CoSoThucAn { get; set; }
    }
}

