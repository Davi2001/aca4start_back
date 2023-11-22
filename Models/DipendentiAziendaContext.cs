using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class DipendentiAziendaContext : DbContext
{
    public DipendentiAziendaContext()
    {
    }

    public DipendentiAziendaContext(DbContextOptions<DipendentiAziendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attivitum> Attivita { get; set; }

    public virtual DbSet<Dipendenti> Dipendentis { get; set; }

    public virtual DbSet<Logger> Loggers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=DipendentiAzienda;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attivitum>(entity =>
        {
            entity.Property(e => e.Data).HasColumnType("datetime");
            entity.Property(e => e.Matricola)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.TipoAttivita)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.MatricolaNavigation).WithMany(p => p.Attivita)
                .HasForeignKey(d => d.Matricola)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Dipendenti>(entity =>
        {
            entity.HasKey(e => e.Matricola);

            entity.ToTable("Dipendenti");

            entity.Property(e => e.Matricola)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.Cap)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CAP");
            entity.Property(e => e.Citta)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Indirizzo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nominativo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Provincia)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Reparto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ruolo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Logger>(entity =>
        {
            entity.ToTable("Logger");

            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateTimeLog).HasColumnType("datetime");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InnerExcept)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MethodName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
