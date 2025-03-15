using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MarineWeatherX.Models;

public partial class WpfProjectDatabaseContext : DbContext
{
    public WpfProjectDatabaseContext()
    {
    }

    public WpfProjectDatabaseContext(DbContextOptions<WpfProjectDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GangnamguPopulation> GangnamguPopulations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=WpfProjectDatabase;Username=postgres;Password=Sereno1324!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<GangnamguPopulation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gangnamgu_population_pkey");

            entity.ToTable("gangnamgu_population");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdministrativeAgency)
                .HasColumnType("character varying")
                .HasColumnName("administrative_agency");
            entity.Property(e => e.FemalePopulation).HasColumnName("female_population");
            entity.Property(e => e.MalePopulation).HasColumnName("male_population");
            entity.Property(e => e.NumberOfHouseholds).HasColumnName("number_of_households");
            entity.Property(e => e.NumberOfPeoplePerHousehold).HasColumnName("number_of_people_per_household");
            entity.Property(e => e.SexRatio).HasColumnName("sex_ratio");
            entity.Property(e => e.TotalPopulation).HasColumnName("total_population");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
