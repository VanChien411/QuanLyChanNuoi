using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("phan_quyen_nguoi_dung")]
    public partial class PhanQuyenNguoiDung
    {
        [Column("NguoiDungID")]
        public int NguoiDungId { get; set; }
        [Column("ma_quyen")]
        public string MaQuyen { get; set; }

        // Chỉ rõ khóa ngoại đến NguoiDung
        [ForeignKey(nameof(NguoiDungId))]
        public virtual NguoiDung NguoiDung { get; set; }

        // Nếu PhanQuyen là bảng khác
        [ForeignKey(nameof(MaQuyen))]
        public virtual PhanQuyen PhanQuyen { get; set; }

    }
}
