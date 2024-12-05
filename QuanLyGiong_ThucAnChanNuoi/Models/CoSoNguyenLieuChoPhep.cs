using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace QuanLyGiong_ThucAnChanNuoi.Models
{

    [Table("CoSo_NguyenLieuChoPhep")]
    public partial class CoSoNguyenLieuChoPhep
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CoSoThucAnID")]
        public int CoSoThucAnId { get; set; }

        [Column("NguyenLieuID")]
        public int NguyenLieuId { get; set; }

        public double? SoLuong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("CoSoThucAnId")]
        [InverseProperty("CoSoNguyenLieuChoPheps")]
        public virtual CoSoThucAn CoSoThucAn { get; set; }

        [ForeignKey("NguyenLieuId")]
        [InverseProperty("CoSoNguyenLieuChoPheps")]
        public virtual NguyenLieuChoPhep NguyenLieu { get; set; }
    }
}
