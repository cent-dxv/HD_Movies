using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HD_Movies.Models;

public partial class MoviedbContext : DbContext
{
    public MoviedbContext()
    {
    }

    public MoviedbContext(DbContextOptions<MoviedbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Film> Films { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.FilmId).HasName("PRIMARY");

            entity.ToTable("film");

            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.LastUpdate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("last_update");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.OriginalLanguageId).HasColumnName("original_language_id");
            entity.Property(e => e.Rating)
                .HasDefaultValueSql("'G'")
                .HasColumnType("enum('G','PG','PG-13','R','NC-17')")
                .HasColumnName("rating");
            entity.Property(e => e.ReleaseYear)
                .HasColumnType("year")
                .HasColumnName("release_year");
            entity.Property(e => e.RentalDuration)
                .HasDefaultValueSql("'3'")
                .HasColumnName("rental_duration");
            entity.Property(e => e.RentalRate)
                .HasPrecision(4)
                .HasDefaultValueSql("'4.99'")
                .HasColumnName("rental_rate");
            entity.Property(e => e.ReplacementCost)
                .HasPrecision(5)
                .HasDefaultValueSql("'19.99'")
                .HasColumnName("replacement_cost");
            entity.Property(e => e.SpecialFeatures)
                .HasColumnType("set('Trailers','Commentaries','Deleted Scenes','Behind the Scenes')")
                .HasColumnName("special_features");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
