using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shinima.Models;

namespace Shinima.DB;

public partial class DbProductContext : DbContext
{
    public DbProductContext()
    {
    }

    public DbProductContext(DbContextOptions<DbProductContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<HistoryUpdateStatus> HistoryUpdateStatuses { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Сomment> Сomments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-TS2627KA;database=dbProduct;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Lastname).HasMaxLength(50);
        });

        modelBuilder.Entity<HistoryUpdateStatus>(entity =>
        {
            entity.ToTable("HistoryUpdateStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdTask).HasColumnName("ID_Task");
            entity.Property(e => e.NewStatusId).HasColumnName("NewStatusID");
            entity.Property(e => e.OldStatusId).HasColumnName("OldStatusID");
            entity.Property(e => e.TimeUpdate).HasColumnType("datetime");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.HistoryUpdateStatuses)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistoryUpdateStatus_Task");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.FinishScheduleDate).HasColumnType("date");
            entity.Property(e => e.IdCreatorEmployee).HasColumnName("ID_CreatorEmployee");
            entity.Property(e => e.IdResponsibleEmployeeId).HasColumnName("ID_ResponsibleEmployeeId");
            entity.Property(e => e.ShortTitle).HasMaxLength(50);
            entity.Property(e => e.StartScheduleDate).HasColumnType("date");

            entity.HasOne(d => d.IdCreatorEmployeeNavigation).WithMany(p => p.ProjectIdCreatorEmployeeNavigations)
                .HasForeignKey(d => d.IdCreatorEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Project_Employee");

            entity.HasOne(d => d.IdResponsibleEmployee).WithMany(p => p.ProjectIdResponsibleEmployees)
                .HasForeignKey(d => d.IdResponsibleEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Project_Employee1");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ColorNex)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("Task");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("date");
            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.FinishActualTime).HasColumnType("datetime");
            entity.Property(e => e.IdExecutiveEmployee).HasColumnName("ID_ExecutiveEmployee");
            entity.Property(e => e.IdPreviousTask).HasColumnName("ID_PreviousTask");
            entity.Property(e => e.IdProject).HasColumnName("ID_Project");
            entity.Property(e => e.IdStatus).HasColumnName("ID_Status");
            entity.Property(e => e.ShortTitle).HasMaxLength(50);
            entity.Property(e => e.StartActualTime).HasColumnType("datetime");
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdExecutiveEmployeeNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdExecutiveEmployee)
                .HasConstraintName("FK_Task_Employee");

            entity.HasOne(d => d.IdPreviousTaskNavigation).WithMany(p => p.InverseIdPreviousTaskNavigation)
                .HasForeignKey(d => d.IdPreviousTask)
                .HasConstraintName("FK_Task_Task");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Project");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_Task_Status");
        });

        modelBuilder.Entity<Сomment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
            entity.Property(e => e.IdTask).HasColumnName("ID_Task");
            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Сomments)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Сomments_Employee");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.Сomments)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Сomments_Task");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
