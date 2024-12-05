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

        public Nhom Nhom { get; set; }
        public PhanQuyen PhanQuyen { get; set; }
    }
}
