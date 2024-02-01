using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Kosmodrom.Models;

namespace Kosmodrom.Models.Contexts
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminLogIn> AdminLogIns { get; set; } = null!;
        public virtual DbSet<BannedCompany> BannedCompanies { get; set; } = null!;
        public virtual DbSet<BannedPassenger> BannedPassengers { get; set; } = null!;
        public virtual DbSet<CompanyLogIn> CompanyLogIns { get; set; } = null!;
        public virtual DbSet<Destination> Destinations { get; set; } = null!;
        public virtual DbSet<HangarReservation> HangarReservations { get; set; } = null!;
        public virtual DbSet<LandingPadsReservation> LandingPadsReservations { get; set; } = null!;
        public virtual DbSet<Malfunction> Malfunctions { get; set; } = null!;
        public virtual DbSet<PassengerArrival> PassengerArrivals { get; set; } = null!;
        public virtual DbSet<PassengerDeparture> PassengerDepartures { get; set; } = null!;
        public virtual DbSet<PassengerLogIn> PassengerLogIns { get; set; } = null!;
        public virtual DbSet<Pilot> Pilots { get; set; } = null!;
        public virtual DbSet<PrivateArrival> PrivateArrivals { get; set; } = null!;
        public virtual DbSet<PrivateDeparture> PrivateDepartures { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<SpaceportSupport> SpaceportSupports { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=C117-01\\SQLEXPRESS;Database=SpaceportManagement;TrustServerCertificate=True;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminLogIn>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<BannedCompany>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.BannedCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BannedCom__Compa__571DF1D5");
            });

            modelBuilder.Entity<BannedPassenger>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.BannedPassengers)
                    .HasForeignKey(d => d.PassengerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BannedPas__Passe__534D60F1");
            });

            modelBuilder.Entity<CompanyLogIn>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.Property(e => e.Delay).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<HangarReservation>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HangarReservations)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__HangarRes__Compa__6477ECF3");
            });

            modelBuilder.Entity<LandingPadsReservation>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.LandingPadsReservations)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__LandingPa__Compa__68487DD7");
            });

            modelBuilder.Entity<Malfunction>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.Malfunctions)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK__Malfuncti__Desti__0D7A0286");
            });

            modelBuilder.Entity<PassengerArrival>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.PassengerArrivals)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK__Passenger__Desti__797309D9");

                entity.HasOne(d => d.HangarReservation)
                    .WithMany(p => p.PassengerArrivals)
                    .HasForeignKey(d => d.HangarReservationId)
                    .HasConstraintName("FK__Passenger__Hanga__7C4F7684");

                entity.HasOne(d => d.LandingPadReservation)
                    .WithMany(p => p.PassengerArrivals)
                    .HasForeignKey(d => d.LandingPadReservationId)
                    .HasConstraintName("FK__Passenger__Landi__7B5B524B");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.PassengerArrivals)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK__Passenger__Vehic__7A672E12");
            });

            modelBuilder.Entity<PassengerDeparture>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.PassengerDepartures)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK__Passenger__Desti__00200768");

                entity.HasOne(d => d.HangarReservation)
                    .WithMany(p => p.PassengerDepartures)
                    .HasForeignKey(d => d.HangarReservationId)
                    .HasConstraintName("FK__Passenger__Hanga__02FC7413");

                entity.HasOne(d => d.LandingPadReservation)
                    .WithMany(p => p.PassengerDepartures)
                    .HasForeignKey(d => d.LandingPadReservationId)
                    .HasConstraintName("FK__Passenger__Landi__02084FDA");

                entity.HasOne(d => d.Pilot1)
                    .WithMany(p => p.PassengerDeparturePilot1s)
                    .HasForeignKey(d => d.Pilot1Id)
                    .HasConstraintName("FK__Passenger__Pilot__03F0984C");

                entity.HasOne(d => d.Pilot2)
                    .WithMany(p => p.PassengerDeparturePilot2s)
                    .HasForeignKey(d => d.Pilot2Id)
                    .HasConstraintName("FK__Passenger__Pilot__04E4BC85");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.PassengerDepartures)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK__Passenger__Vehic__01142BA1");
            });

            modelBuilder.Entity<PassengerLogIn>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<PrivateArrival>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PrivateArrivals)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__PrivateAr__Compa__6EF57B66");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.PrivateArrivals)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK__PrivateAr__Desti__6C190EBB");

                entity.HasOne(d => d.HangarReservation)
                    .WithMany(p => p.PrivateArrivals)
                    .HasForeignKey(d => d.HangarReservationId)
                    .HasConstraintName("FK__PrivateAr__Hanga__6E01572D");

                entity.HasOne(d => d.LandingPadReservation)
                    .WithMany(p => p.PrivateArrivals)
                    .HasForeignKey(d => d.LandingPadReservationId)
                    .HasConstraintName("FK__PrivateAr__Landi__6D0D32F4");
            });

            modelBuilder.Entity<PrivateDeparture>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PrivateDepartures)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__PrivateDe__Compa__75A278F5");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.PrivateDepartures)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK__PrivateDe__Desti__72C60C4A");

                entity.HasOne(d => d.HangarReservation)
                    .WithMany(p => p.PrivateDepartures)
                    .HasForeignKey(d => d.HangarReservationId)
                    .HasConstraintName("FK__PrivateDe__Hanga__74AE54BC");

                entity.HasOne(d => d.LandingPadReservation)
                    .WithMany(p => p.PrivateDepartures)
                    .HasForeignKey(d => d.LandingPadReservationId)
                    .HasConstraintName("FK__PrivateDe__Landi__73BA3083");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.PassengerDepartures)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.PassengerDeparturesId)
                    .HasConstraintName("FK__Reservati__Passe__09A971A2");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK__Reservati__Passe__08B54D69");
            });

            modelBuilder.Entity<SpaceportSupport>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
