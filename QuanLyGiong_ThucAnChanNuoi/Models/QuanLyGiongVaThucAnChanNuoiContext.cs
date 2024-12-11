using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace QuanLyGiong_ThucAnChanNuoi.Models
{

    public partial class QuanLyGiongVaThucAnChanNuoiContext : DbContext
    {
        public QuanLyGiongVaThucAnChanNuoiContext()
        {
        }

        public QuanLyGiongVaThucAnChanNuoiContext(DbContextOptions<QuanLyGiongVaThucAnChanNuoiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CapHc> CapHcs { get; set; }

        public virtual DbSet<ChucVu> ChucVus { get; set; }

        public virtual DbSet<CoSoHoaChatCam> CoSoHoaChatCams { get; set; }

        public virtual DbSet<CoSoNguyenLieuChoPhep> CoSoNguyenLieuChoPheps { get; set; }

        public virtual DbSet<CoSoThucAn> CoSoThucAns { get; set; }

        public virtual DbSet<CoSoVatNuoi> CoSoVatNuois { get; set; }

        public virtual DbSet<DonViHc> DonViHcs { get; set; }

        public virtual DbSet<GiayChungNhan> GiayChungNhans { get; set; }

        public virtual DbSet<GiongCanBaoTon> GiongCanBaoTons { get; set; }

        public virtual DbSet<GiongVatNuoi> GiongVatNuois { get; set; }

        public virtual DbSet<HoaChatCam> HoaChatCams { get; set; }

        public virtual DbSet<LichSuTruyCap> LichSuTruyCaps { get; set; }

        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

        public virtual DbSet<NguonGen> NguonGens { get; set; }

        public virtual DbSet<NguyenLieuChoPhep> NguyenLieuChoPheps { get; set; }

        public virtual DbSet<Nhom> Nhoms { get; set; }

        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

        public virtual DbSet<ThanhVienNhom> ThanhVienNhoms { get; set; }

        public virtual DbSet<ThucAnChanNuoi> ThucAnChanNuois { get; set; }

        public virtual DbSet<ToChucCaNhan> ToChucCaNhans { get; set; }

        public virtual DbSet<ToChucNguonGen> ToChucNguonGens { get; set; }
        public virtual DbSet<PhanQuyenNguoiDung> PhanQuyenNguoiDungs { get; set; }
        public virtual DbSet<PhanQuyenNhom> PhanQuyenNhoms { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning 
            => optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DBConnection"]?.ConnectionString,
             sqlServerOptionsAction: sqlOptions =>
             {
                 sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(2),
                    errorNumbersToAdd: null);
             });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CapHc>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Cap_HC__3214EC278B6F0A11");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ChucVu__3214EC27BB75931F");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CoSoHoaChatCam>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__CoSo_Hoa__3214EC27C42CCD2A");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CoSoThucAn).WithMany(p => p.CoSoHoaChatCams)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSo_HoaC__CoSoT__70DDC3D8");

                entity.HasOne(d => d.HoaChatCam).WithMany(p => p.CoSoHoaChatCams)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSo_HoaC__HoaCh__71D1E811");
            });

            modelBuilder.Entity<CoSoNguyenLieuChoPhep>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__CoSo_Ngu__3214EC27FC6117A4");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CoSoThucAn).WithMany(p => p.CoSoNguyenLieuChoPheps)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSo_Nguy__CoSoT__76969D2E");

                entity.HasOne(d => d.NguyenLieu).WithMany(p => p.CoSoNguyenLieuChoPheps)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSo_Nguy__Nguye__778AC167");
            });

            modelBuilder.Entity<CoSoThucAn>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__CoSoThuc__3214EC27CD16556B");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ThucAnChanNuoi).WithMany(p => p.CoSoThucAns)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSoThucA__ThucA__68487DD7");

                entity.HasOne(d => d.ToChucCaNhan).WithMany(p => p.CoSoThucAns)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSoThucA__ToChu__6754599E");
            });

            modelBuilder.Entity<CoSoVatNuoi>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__CoSoVatN__3214EC273E840717");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.GiongVatNuoi).WithMany(p => p.CoSoVatNuois)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSoVatNu__Giong__59FA5E80");

                entity.HasOne(d => d.ToChucCaNhan).WithMany(p => p.CoSoVatNuois)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CoSoVatNu__ToChu__59063A47");
            });

            modelBuilder.Entity<DonViHc>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__DonVi_HC__3214EC27F82C804F");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CapHc).WithMany(p => p.DonViHcs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DonVi_HC__Cap_HC__3A81B327");

                entity.HasOne(d => d.TrucThuocNavigation).WithMany(p => p.InverseTrucThuocNavigation).HasConstraintName("FK__DonVi_HC__TrucTh__398D8EEE");

            
            });

            modelBuilder.Entity<GiayChungNhan>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__GiayChun__3214EC2785E9692F");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.NoiCap).HasDefaultValueSql("(N'Cục chăn nuôi')");

                entity.HasOne(d => d.CoSoThucAn).WithMany(p => p.GiayChungNhans)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GiayChung__CoSoT__6C190EBB");
            });

            modelBuilder.Entity<GiongCanBaoTon>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__GiongCan__3214EC277F25E256");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Giong).WithMany(p => p.GiongCanBaoTons)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GiongCanB__Giong__5CD6CB2B");
            });

            modelBuilder.Entity<GiongVatNuoi>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__GiongVat__3214EC277F83271F");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<HoaChatCam>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__HoaChatC__3214EC273ADD4859");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<LichSuTruyCap>(entity =>
            {
                entity.HasOne(d => d.NguoiDung).WithMany().HasConstraintName("FK__lich_su_t__Nguoi__4222D4EF");
            });
            modelBuilder.Entity<PhanQuyenNhom>(entity =>
            {

                entity.ToTable("phan_quyen_nhom");

                entity.HasKey(e => new { e.NhomID, e.MaQuyen });


                entity.Property(e => e.NhomID)
                    .HasColumnName("NhomID")
                    .IsRequired();

                entity.Property(e => e.MaQuyen)
                    .HasColumnName("ma_quyen")
                    .IsRequired();

                entity.HasOne(e => e.Nhom)
                    .WithMany(d => d.PhanQuyenNhoms)
                    .HasForeignKey(e => e.NhomID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PhanQuyen)
                    .WithMany(p => p.PhanQuyenNhoms)
                    .HasForeignKey(e => e.MaQuyen)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<PhanQuyenNguoiDung>(entity =>
            {
                entity.ToTable("phan_quyen_nguoi_dung");

                entity.HasKey(e => new { e.NguoiDungId, e.MaQuyen });

                entity.Property(e => e.NguoiDungId)
                    .HasColumnName("NguoiDungID")
                    .IsRequired();

                entity.Property(e => e.MaQuyen)
                    .HasColumnName("ma_quyen")
                    .IsRequired();

                entity.HasOne(e => e.NguoiDung)
                    .WithMany(d => d.PhanQuyenNguoiDungs)
                    .HasForeignKey(e => e.NguoiDungId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PhanQuyen)
                    .WithMany(p => p.PhanQuyenNguoiDungs)
                    .HasForeignKey(e => e.MaQuyen)
                    .OnDelete(DeleteBehavior.Cascade);


            });
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__NguoiDun__3214EC276E110FFC");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ChucVu).WithMany(p => p.NguoiDungs).HasConstraintName("FK__NguoiDung__ChucV__3F466844");

                entity.HasOne(d => d.DonViHc).WithMany(p => p.NguoiDungs).HasConstraintName("FK__NguoiDung__DonVi__403A8C7D");

             
            });

            modelBuilder.Entity<NguonGen>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__NguonGen__3214EC273522CF75");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<NguyenLieuChoPhep>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__NguyenLi__3214EC27A332078C");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Nhom>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Nhom__3214EC275D50EEAA");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                //Cấu hình mối quan hệ nhiều-nhiều giữa NguoiDung và PhanQuyen
                entity.HasMany(d => d.PhanQuyenNhoms) // NguoiDung có nhiều PhanQuyen
                 .WithOne()  // Mối quan hệ một chiều ở phía PhanQuyen
                 .HasForeignKey(pq => pq.NhomID) // NguoiDungId trong bảng trung gian
                 .HasConstraintName("FK__PhanQuyenNhom__Id");
              
            });

            modelBuilder.Entity<PhanQuyen>(entity =>
            {
                entity.HasKey(e => e.MaQuyen).HasName("PK__phan_quy__1D4B7ED4EB4AE75E");
              
            });

            modelBuilder.Entity<ThanhVienNhom>(entity =>
            {
                entity.HasKey(e => new { e.NguoiDungId, e.NhomId }).HasName("PK__ThanhVie__8A239FDD5C32A77A");

                entity.HasOne(d => d.NguoiDung).WithMany(p => p.ThanhVienNhoms)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThanhVien__Nguoi__46E78A0C");

                entity.HasOne(d => d.Nhom).WithMany(p => p.ThanhVienNhoms)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThanhVien__NhomI__47DBAE45");
            });

            modelBuilder.Entity<ThucAnChanNuoi>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ThucAnCh__3214EC27DC6CE5DB");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ToChucCaNhan>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ToChucCa__3214EC27302ACB62");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.LoaiHinh).HasDefaultValueSql("(N'Tổ chức')");
            });

            modelBuilder.Entity<ToChucNguonGen>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ToChucNg__3214EC271100F644");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.NguonGen).WithMany(p => p.ToChucNguonGens)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ToChucNgu__Nguon__628FA481");

                entity.HasOne(d => d.ToChucCaNhan).WithMany(p => p.ToChucNguonGens)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ToChucNgu__ToChu__619B8048");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
