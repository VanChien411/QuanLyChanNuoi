﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{
  
    [Table("lich_su_truy_cap")]
    public partial class LichSuTruyCap
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NguoiDungID")]
        public int? NguoiDungId { get; set; }
  
        [Column(TypeName = "datetime")]
        public DateTime? ThoiGian { get; set; }

        [Column("mota")]
        [StringLength(50)]
        public string Mota { get; set; }

        [Column("IP_address")]
        [StringLength(40)]
        public string IpAddress { get; set; }

        [ForeignKey("NguoiDungId")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}

