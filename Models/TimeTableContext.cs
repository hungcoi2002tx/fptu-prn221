using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment.Models
{
    public partial class TimeTableContext : DbContext
    {
        public TimeTableContext()
        {
        }

        public TimeTableContext(DbContextOptions<TimeTableContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Timetable> Timetables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=DESKTOP-VM67PNO\\NGUYENMINHHUNG;database=TimeTable;user=sa;password=123; TrustServerCertificate = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasIndex(e => e.Code, "UC_Classes_Code")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasIndex(e => e.Code, "UC_Rooms_Code")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.HasIndex(e => e.Code, "UC_Slots_Code")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasIndex(e => e.Code, "UC_Subjects_Code")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasIndex(e => e.Code, "UC_Teachers_Code")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Timetable>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ClassCode)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.RoomCode)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.SlotCode)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherCode)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClassCodeNavigation)
                    .WithMany(p => p.Timetables)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.ClassCode)
                    .HasConstraintName("FK__Timetable__Class__5CD6CB2B");

                entity.HasOne(d => d.RoomCodeNavigation)
                    .WithMany(p => p.Timetables)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.RoomCode)
                    .HasConstraintName("FK__Timetable__RoomC__5FB337D6");

                entity.HasOne(d => d.SlotCodeNavigation)
                    .WithMany(p => p.Timetables)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SlotCode)
                    .HasConstraintName("FK__Timetable__SlotC__60A75C0F");

                entity.HasOne(d => d.SubjectCodeNavigation)
                    .WithMany(p => p.Timetables)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SubjectCode)
                    .HasConstraintName("FK__Timetable__Subje__5EBF139D");

                entity.HasOne(d => d.TeacherCodeNavigation)
                    .WithMany(p => p.Timetables)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.TeacherCode)
                    .HasConstraintName("FK__Timetable__Teach__5DCAEF64");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
