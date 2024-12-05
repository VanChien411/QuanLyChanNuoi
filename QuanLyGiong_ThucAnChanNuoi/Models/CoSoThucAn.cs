using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("CoSoThucAn")]
    public partial class CoSoThucAn
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenCoSo { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiCoSo { get; set; }

        [Column("ToChucCaNhanID")]
        public int ToChucCaNhanId { get; set; }

        [Column("ThucAnChanNuoiID")]
        public int ThucAnChanNuoiId { get; set; }

        [InverseProperty("CoSoThucAn")]
        public virtual ICollection<CoSoHoaChatCam> CoSoHoaChatCams { get; set; } = new List<CoSoHoaChatCam>();

        [InverseProperty("CoSoThucAn")]
        public virtual ICollection<CoSoNguyenLieuChoPhep> CoSoNguyenLieuChoPheps { get; set; } = new List<CoSoNguyenLieuChoPhep>();

        [InverseProperty("CoSoThucAn")]
        public virtual ICollection<GiayChungNhan> GiayChungNhans { get; set; } = new List<GiayChungNhan>();

        [ForeignKey("ThucAnChanNuoiId")]
        [InverseProperty("CoSoThucAns")]
        public virtual ThucAnChanNuoi ThucAnChanNuoi { get; set; }

        [ForeignKey("ToChucCaNhanId")]
        [InverseProperty("CoSoThucAns")]
        public virtual ToChucCaNhan ToChucCaNhan { get; set; }
    }
}

