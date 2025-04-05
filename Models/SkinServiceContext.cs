using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SkinCareService.Models;

namespace Models;

public partial class SkinServiceContext : DbContext
{
    public SkinServiceContext()
    {
    }

    public SkinServiceContext(DbContextOptions<SkinServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServiceResult> ServiceResults { get; set; }

    public virtual DbSet<SkinService> SkinServices { get; set; }

    public virtual DbSet<StaffAction> StaffActions { get; set; }

    public virtual DbSet<Therapist> Therapists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorkingSchedule> WorkingSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=SkinService;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blogs__54379E30C23EBA3B");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Blogs__AuthorId__4222D4EF");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED02ADD33D");

            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Not Paid");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Bookings__Custom__34C8D9D1");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Bookings__Servic__35BCFE0A");

            entity.HasOne(d => d.Therapist).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TherapistId)
                .HasConstraintName("FK__Bookings__Therap__36B12243");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDD6ACC496B4");

            entity.Property(e => e.Comments).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Feedbacks__Booki__3D5E1FD2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38A33DA744");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Payments__Bookin__45F365D3");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD4805E6C6A555");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReportType).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AA78126CD");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160ECA8D96F").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<ServiceResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__ServiceR__976902083D755620");

            entity.Property(e => e.ResultDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ResultDescription).HasMaxLength(1000);

            entity.HasOne(d => d.Booking).WithMany(p => p.ServiceResults)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__ServiceRe__Booki__5070F446");

            entity.HasOne(d => d.Therapist).WithMany(p => p.ServiceResults)
                .HasForeignKey(d => d.TherapistId)
                .HasConstraintName("FK__ServiceRe__Thera__5165187F");
        });

        modelBuilder.Entity<SkinService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__SkinServ__C51BB00AB1596D79");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ServiceName).HasMaxLength(100);
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<StaffAction>(entity =>
        {
            entity.HasKey(e => e.ActionId).HasName("PK__StaffAct__FFE3F4D941B56B6B");

            entity.Property(e => e.ActionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActionType).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Booking).WithMany(p => p.StaffActions)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__StaffActi__Booki__4AB81AF0");

            entity.HasOne(d => d.Staff).WithMany(p => p.StaffActions)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__StaffActi__Staff__4BAC3F29");
        });

        modelBuilder.Entity<Therapist>(entity =>
        {
            entity.HasKey(e => e.TherapistId).HasName("PK__Therapis__4D62193205ACE5BF");

            entity.Property(e => e.Certifications).HasMaxLength(500);
            entity.Property(e => e.Specialization).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Therapists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Therapist__UserI__2F10007B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C21280A95");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053435FE6ED7").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__286302EC");
        });

        modelBuilder.Entity<WorkingSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__WorkingS__9C8A5B498BC506D3");

            entity.HasOne(d => d.Therapist).WithMany(p => p.WorkingSchedules)
                .HasForeignKey(d => d.TherapistId)
                .HasConstraintName("FK__WorkingSc__Thera__31EC6D26");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
