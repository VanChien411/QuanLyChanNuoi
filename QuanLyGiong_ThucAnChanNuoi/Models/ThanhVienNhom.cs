using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
   
[Table("ThanhVienNhom")]
public partial class ThanhVienNhom
{
    [Key]
    [Column("NguoiDungID")]
    public int NguoiDungId { get; set; }

    [Key]
    [Column("NhomID")]
    public int NhomId { get; set; }

    public bool? LaTruongNhom { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayThamGia { get; set; }

    [ForeignKey("NguoiDungId")]
    [InverseProperty("ThanhVienNhoms")]
    public virtual NguoiDung NguoiDung { get; set; }

    [ForeignKey("NhomId")]
    [InverseProperty("ThanhVienNhoms")]
    public virtual Nhom Nhom { get; set; }
}
}

