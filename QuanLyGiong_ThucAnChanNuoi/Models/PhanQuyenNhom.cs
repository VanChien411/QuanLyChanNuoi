using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("phan_quyen_nhom")]
    public class PhanQuyenNhom
    {
        public int NhomID { get; set; }
        public string MaQuyen { get; set; }

        // Chỉ rõ khóa ngoại đến NguoiDung
        [ForeignKey(nameof(NhomID))]
        public virtual Nhom Nhom { get; set; }

        // Nếu PhanQuyen là bảng khác
        [ForeignKey(nameof(MaQuyen))]
        public virtual PhanQuyen PhanQuyen { get; set; }
    }
}
