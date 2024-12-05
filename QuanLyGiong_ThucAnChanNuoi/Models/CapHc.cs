
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models {
    [Table("Cap_HC")]
    public partial class CapHc
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        public int Cap { get; set; }

        [InverseProperty("CapHc")]
        public virtual ICollection<DonViHc> DonViHcs { get; set; } = new List<DonViHc>();
    }
}

