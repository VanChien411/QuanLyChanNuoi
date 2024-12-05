using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
    [Table("HoaChatCam")]
    public partial class HoaChatCam
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenHoaChat { get; set; }

        [StringLength(200)]
        public string LyDoCam { get; set; }

        [InverseProperty("HoaChatCam")]
        public virtual ICollection<CoSoHoaChatCam> CoSoHoaChatCams { get; set; } = new List<CoSoHoaChatCam>();
    }
}

