using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KenTaShop.Data;

public partial class ClothesShopManagementContext : DbContext
{
    public ClothesShopManagementContext()
    {
    }

    public ClothesShopManagementContext(DbContextOptions<ClothesShopManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Billinfor> Billinfors { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<Goodsinfor> Goodsinfors { get; set; }

    public virtual DbSet<Goodstype> Goodstypes { get; set; }

    public virtual DbSet<ImportGood> ImportGoods { get; set; }

    public virtual DbSet<ImportGoodsinfor> ImportGoodsinfors { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userstype> Userstypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

        => optionsBuilder.UseSqlServer("Data Source=DucTrong;Initial Catalog=ClothesShopManagement;Integrated Security=True;Trust Server Certificate=True");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.IdBill);

            entity.ToTable("Bill");

            entity.Property(e => e.BillDate).HasColumnType("date");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_Bill_Status");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Bill_Users");
        });

        modelBuilder.Entity<Billinfor>(entity =>
        {
            entity.HasKey(e => e.IdBillInfor);

            entity.ToTable("Billinfor");

            entity.HasOne(d => d.IdGoodsNavigation).WithMany(p => p.Billinfors)
                .HasForeignKey(d => d.IdGoods)
                .HasConstraintName("FK_Billinfor_Goods");

            entity.HasOne(d => d.IdbillNavigation).WithMany(p => p.Billinfors)
                .HasForeignKey(d => d.Idbill)
                .HasConstraintName("FK_Billinfor_Bill");
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.IdGoods);

            entity.Property(e => e.IdGoods);
            entity.Property(e => e.GoodsName).HasMaxLength(50);

            entity.HasOne(d => d.IdGoodstypeNavigation).WithMany(p => p.Goods)
                .HasForeignKey(d => d.IdGoodstype)
                .HasConstraintName("FK_Goods_Goodstype");
        });

        modelBuilder.Entity<Goodsinfor>(entity =>
        {
            entity.HasKey(e => e.IdGoodsInfor);

            entity.ToTable("Goodsinfor");

            entity.Property(e => e.IdGoodsInfor);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.GoodsDetail).HasMaxLength(100);
            entity.Property(e => e.Size).HasMaxLength(50);

            entity.HasOne(d => d.IdGoodsNavigation).WithMany(p => p.Goodsinfors)
                .HasForeignKey(d => d.IdGoods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Goodsinfor_Goods");
        });

        modelBuilder.Entity<Goodstype>(entity =>
        {
            entity.HasKey(e => e.IdGoodstype);

            entity.ToTable("Goodstype");

            entity.Property(e => e.IdGoodstype);
            entity.Property(e => e.GoodstypeDetail).HasMaxLength(100);
        });

        modelBuilder.Entity<ImportGood>(entity =>
        {
            entity.HasKey(e => e.IdImport);

            entity.Property(e => e.IdImport);
            entity.Property(e => e.CreatedDate).HasColumnType("date");

            entity.HasOne(d => d.IdGoodstypeNavigation).WithMany(p => p.ImportGoods)
                .HasForeignKey(d => d.IdGoodstype)
                .HasConstraintName("FK_ImportGoods_Goodstype");

            entity.HasOne(d => d.IdImportGoodsinforNavigation).WithMany(p => p.ImportGoods)
                .HasForeignKey(d => d.IdImportGoodsinfor)
                .HasConstraintName("FK_ImportGoods_ImportGoodsinfor");

            entity.HasOne(d => d.IdstatusNavigation).WithMany(p => p.ImportGoods)
                .HasForeignKey(d => d.Idstatus)
                .HasConstraintName("FK_ImportGoods_Status");
        });

        modelBuilder.Entity<ImportGoodsinfor>(entity =>
        {
            entity.HasKey(e => e.IdImportGoodsinfor);

            entity.ToTable("ImportGoodsinfor");

            entity.Property(e => e.IdImportGoodsinfor);
            entity.Property(e => e.Vat).HasColumnName("VAT");

            entity.HasOne(d => d.IdGoodsNavigation).WithMany(p => p.ImportGoodsinfors)
                .HasForeignKey(d => d.IdGoods)
                .HasConstraintName("FK_ImportGoodsinfor_Goods");

            entity.HasOne(d => d.IdProducerNavigation).WithMany(p => p.ImportGoodsinfors)
                .HasForeignKey(d => d.IdProducer)
                .HasConstraintName("FK_ImportGoodsinfor_Producer");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.IdPicture);

            entity.ToTable("Picture");

            entity.Property(e => e.IdPicture);
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("URL");

            entity.HasOne(d => d.IdGoodsNavigation).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.IdGoods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Picture_Goods");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.IdProducer);

            entity.ToTable("Producer");

            entity.Property(e => e.IdProducer);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Producername)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.IdRating);
            entity.ToTable("Rating");

            entity.Property(e => e.Comment).HasMaxLength(200);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Rating1)
                .HasMaxLength(50)
                .HasColumnName("Rating");

            entity.HasOne(d => d.IdBillNavigation).WithMany()
                .HasForeignKey(d => d.IdBill)
                .HasConstraintName("FK_Rating_Bill");

            entity.HasOne(d => d.IdGoodsNavigation).WithMany()
                .HasForeignKey(d => d.IdGoods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rating_Goods");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus);

            entity.ToTable("Status");

            entity.Property(e => e.IdStatus);
            entity.Property(e => e.Statusdetail).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Pass).HasMaxLength(200);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.IdUsertypeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdUsertype)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Userstype");
        });

        modelBuilder.Entity<Userstype>(entity =>
        {
            entity.HasKey(e => e.IdUsertype).HasName("PK_Usertype");

            entity.ToTable("Userstype");

            entity.Property(e => e.UserDetail).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
