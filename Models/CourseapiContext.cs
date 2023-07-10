using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Models;

public partial class CourseapiContext : DbContext
{
    public CourseapiContext()
    {
    }

    public CourseapiContext(DbContextOptions<CourseapiContext> options) : base(options)
    { }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         //
    //     }
    // }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder
    //         .UseCollation("utf8mb4_0900_ai_ci")
    //         .HasCharSet("utf8mb4");

    //     modelBuilder.Entity<Course>(entity =>
    //     {
    //         entity.HasKey(e => e.Id).HasName("PRIMARY");

    //         entity.ToTable("course", tb => tb.HasComment("The various courses followed by students in an institution."));

    //         // entity.HasIndex(e => e.IdStudent, "id_student");

    //         entity.Property(e => e.Id)
    //             .ValueGeneratedNever()
    //             .HasColumnName("id");
    //         entity.Property(e => e.Credit).HasColumnName("credit");
    //         entity.Property(e => e.Description)
    //             .HasColumnType("text")
    //             .HasColumnName("description");
    //         // entity.Property(e => e.IdStudent).HasColumnName("id_student");
    //         entity.Property(e => e.Name)
    //             .HasMaxLength(50)
    //             .HasDefaultValueSql("''")
    //             .HasColumnName("name");
    //     });

    //     modelBuilder.Entity<Student>(entity =>
    //     {
    //         entity.HasKey(e => e.StudentId).HasName("PRIMARY");

    //         entity.ToTable("student", tb => tb.HasComment("Student basic informations with associated course."));

    //         entity.Property(e => e.StudentId)
    //             .ValueGeneratedNever()
    //             .HasColumnName("student_id");
    //         entity.Property(e => e.Age).HasColumnName("age");
    //         entity.Property(e => e.Name)
    //             .HasMaxLength(50)
    //             .HasDefaultValueSql("''")
    //             .HasColumnName("name");
    //     });

    //     OnModelCreatingPartial(modelBuilder);
    // }

    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
