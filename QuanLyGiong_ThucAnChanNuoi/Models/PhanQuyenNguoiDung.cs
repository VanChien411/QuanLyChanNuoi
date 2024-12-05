using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("phan_quyen_nguoi_dung")]
    public partial class PhanQuyenNguoiDung
    {
        public int NguoiDungId { get; set; }
        public string MaQuyen { get; set; }

        public NguoiDung NguoiDung { get; set; }
        public PhanQuyen PhanQuyen { get; set; }

    }
}
