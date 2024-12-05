using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{

    [Table("CoSo_HoaChatCam")]
    public partial class CoSoHoaChatCam
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("CoSoThucAnID")]
        public int CoSoThucAnId { get; set; }

        [Column("HoaChatCamID")]
        public int HoaChatCamId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayPhatHien { get; set; }

        [StringLength(200)]
        public string LyDoSuDung { get; set; }

        [ForeignKey("CoSoThucAnId")]
        [InverseProperty("CoSoHoaChatCams")]
        public virtual CoSoThucAn CoSoThucAn { get; set; }

        [ForeignKey("HoaChatCamId")]
        [InverseProperty("CoSoHoaChatCams")]
        public virtual HoaChatCam HoaChatCam { get; set; }
    }
}
