using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AIW3_DewaPermana_SMKN8JEMBER.Models;

public partial class EsemkaRailwaysContext : DbContext
{
    public EsemkaRailwaysContext()
    {
    }

    public EsemkaRailwaysContext(DbContextOptions<EsemkaRailwaysContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=RPLSMKN8JBR\\SQLEXPRESS;Initial Catalog=EsemkaRailways;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915F90D10B9E2D");

            entity.ToTable("Passenger");

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B69979D28CA");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.ArrivalStationId).HasColumnName("ArrivalStationID");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureStationId).HasColumnName("DepartureStationID");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.TrainId).HasColumnName("TrainID");

            entity.HasOne(d => d.ArrivalStation).WithMany(p => p.ScheduleArrivalStations)
                .HasForeignKey(d => d.ArrivalStationId)
                .HasConstraintName("FK__Schedule__Arriva__2C3393D0");

            entity.HasOne(d => d.DepartureStation).WithMany(p => p.ScheduleDepartureStations)
                .HasForeignKey(d => d.DepartureStationId)
                .HasConstraintName("FK__Schedule__Depart__2D27B809");

            entity.HasOne(d => d.Train).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TrainId)
                .HasConstraintName("FK__Schedule__TrainI__2E1BDC42");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Station__E0D8A6DDDBC5CE09");

            entity.ToTable("Station");

            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StationName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC627C0955221");

            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.BookingTime).HasColumnType("datetime");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Passenger).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__Ticket__Passenge__2F10007B");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK__Ticket__Schedule__300424B4");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.HasKey(e => e.TrainId).HasName("PK__Train__8ED2725A5F51EE38");

            entity.ToTable("Train");

            entity.Property(e => e.TrainId).HasColumnName("TrainID");
            entity.Property(e => e.TrainName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
