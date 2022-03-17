using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TimetableMVCApp.Models
{
    public partial class SchoolDBContext : DbContext
    {
        public SchoolDBContext()
        {
        }

        public SchoolDBContext(DbContextOptions<SchoolDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Day> Days { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<Time> Times { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Time)
                    .WithMany(p => p.Days)
                    .HasForeignKey(d => d.TimeId)
                    .HasConstraintName("FK_Days_Times");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasMany(d => d.Days)
                    .WithMany(p => p.Modules)
                    .UsingEntity<Dictionary<string, object>>(
                        "ModuleDay",
                        l => l.HasOne<Day>().WithMany().HasForeignKey("DayId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ModuleDays_Days"),
                        r => r.HasOne<Module>().WithMany().HasForeignKey("ModuleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ModuleDays_Modules"),
                        j =>
                        {
                            j.HasKey("ModuleId", "DayId");

                            j.ToTable("ModuleDays");
                        });
            });

            modelBuilder.Entity<Time>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
