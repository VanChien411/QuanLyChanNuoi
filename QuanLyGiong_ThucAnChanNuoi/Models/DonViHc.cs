
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("DonVi_HC")]
    public partial class DonViHc
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }
        public string MaBuuDien { get; set; }
        public int Cap { get; set; }

        public int? TrucThuoc { get; set; }

        [Column("Cap_HC_ID")]
        public int CapHcId { get; set; }

        [ForeignKey("CapHcId")]
        [InverseProperty("DonViHcs")]
        public virtual CapHc CapHc { get; set; }

        [InverseProperty("TrucThuocNavigation")]
        public virtual ICollection<DonViHc> InverseTrucThuocNavigation { get; set; } = new List<DonViHc>();

        [InverseProperty("DonViHc")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();

        [ForeignKey("TrucThuoc")]
        [InverseProperty("InverseTrucThuocNavigation")]
        public virtual DonViHc TrucThuocNavigation { get; set; }

    }
}

