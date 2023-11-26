using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace mandiri_project.Entities
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnnualReview> AnnualReviews { get; set; } = null!;
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<BusinessAreaMaster> BusinessAreaMasters { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-E88QR8QI\\SQLSERVER2017;Database=Mandiri;User Id=sa;Password=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnnualReview>(entity =>
            {
                entity.ToTable("AnnualReview");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmployeeNo).HasMaxLength(15);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ReviewDate).HasColumnType("date");

                entity.HasOne(d => d.EmployeeNoNavigation)
                    .WithMany(p => p.AnnualReviews)
                    .HasForeignKey(d => d.EmployeeNo)
                    .HasConstraintName("FK_Employee_No");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_App_User");

                entity.ToTable("ApplicationUser");

                entity.Property(e => e.UserId).HasMaxLength(100);

                entity.Property(e => e.BusinessAreaCode).HasMaxLength(15);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Password).HasColumnType("text");

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.Role).HasMaxLength(15);

                entity.Property(e => e.Token).HasColumnType("text");

                entity.Property(e => e.TokenExpireDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BusinessAreaMaster>(entity =>
            {
                entity.HasKey(e => e.AreaCode)
                    .HasName("PK_Business_Area_Master");

                entity.ToTable("BusinessAreaMaster");

                entity.Property(e => e.AreaCode).HasMaxLength(15);

                entity.Property(e => e.AreaName).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeNo);

                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeNo).HasMaxLength(15);

                entity.Property(e => e.BusinessAreaCode).HasMaxLength(15);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TerminationDate).HasColumnType("date");

                entity.HasOne(d => d.BusinessAreaCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BusinessAreaCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_Code_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
