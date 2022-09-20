using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using prova.Models.Entities;

namespace prova.Models.Services.Infrastructure
{
    public partial class MyCourseDbContext : DbContext
    {

        public MyCourseDbContext(DbContextOptions<MyCourseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses"); //superfluo se la tabella si chiama come la proprietà che espone il DbSet
                entity.HasKey(Course => Course.Id);
                //entity.HasKey(Course => new {Course.Id, Course.Author}); nel caso di più chiavi primarie
                
                //Mapping pr owned Types
                entity.OwnsOne(Course => Course.CurrentPrice, builder => {
                    builder.Property(money => money.Currency).HasConversion<string>().HasColumnName("CurrentPrice_Currency"); //è superfluo perchè le colonne seguono già la convenzione dei nomi
                    builder.Property(money => money.Amount).HasColumnName("CurrentPrice_Amount"); //è superfluo perchè le colonne seguono già la convenzione dei nomi
                });

                //Mapping pr owned Types
                entity.OwnsOne(Course => Course.FullPrice, builder => {
                    builder.Property(money => money.Currency).HasConversion<string>().HasColumnName("FullPrice_Currency"); //è superfluo perchè le colonne seguono già la convenzione dei nomi
                    builder.Property(money => money.Amount).HasColumnName("FullPrice_Amount"); //è superfluo perchè le colonne seguono già la convenzione dei nomi
                });

                //Mapping per le relazioni tra le tabelle
                entity.HasMany(course => course.Lessons)
                .WithOne(lesson => lesson.Course)
                .HasForeignKey(lesson => lesson.CourseId); //superflua se il nome della proprietà ha il nome dell'identità pricipale con il suffisso Id: CourseId appunto
                
                #region Mapping generato automaticamente dal tool di reverse engineering
                /*
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");

                entity.Property(e => e.CurrentPriceAmount)
                    .IsRequired()
                    .HasColumnName("CurrentPrice_Amount")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CurrentPriceCurrency)
                    .IsRequired()
                    .HasColumnName("CurrentPrice_Currency")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                entity.Property(e => e.Email).HasColumnType("TEXT (100)");

                entity.Property(e => e.FullPriceAmount)
                    .IsRequired()
                    .HasColumnName("FullPrice_Amount")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FullPriceCurrency)
                    .IsRequired()
                    .HasColumnName("FullPrice_Currency")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.ImagePath).HasColumnType("TEXT (100)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");
                    */
                    #endregion
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasOne(lesson => lesson.Course)
                .WithMany(Course => Course.Lessons);
                #region Mapping generato automaticamente dal tool di reverse engineering
                /*
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasColumnType("TEXT (8)")
                    .HasDefaultValueSql("'00:00:00'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.CourseId);
                    */
                    #endregion
            });
        }
    }
}
