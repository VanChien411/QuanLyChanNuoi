using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("CoSoVatNuoi")]
    public partial class CoSoVatNuoi
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenCoSo { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [Column("trangthai")]
        public bool? Trangthai { get; set; }

        [Column("ToChucCaNhanID")]
        public int ToChucCaNhanId { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiCoSo { get; set; }

        [Column("GiongVatNuoiID")]
        public int GiongVatNuoiId { get; set; }

        [ForeignKey("GiongVatNuoiId")]
        [InverseProperty("CoSoVatNuois")]
        public virtual GiongVatNuoi GiongVatNuoi { get; set; }

        [ForeignKey("ToChucCaNhanId")]
        [InverseProperty("CoSoVatNuois")]
        public virtual ToChucCaNhan ToChucCaNhan { get; set; }
    }
}

