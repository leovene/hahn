using Hahn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ArtObjectEntity> ArtObjects { get; set; }
        public DbSet<ConstituentEntity> Constituents { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtObjectEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ObjectId) 
                      .IsRequired();

                entity.Property(e => e.DepartmentId)
                      .IsRequired();

                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(e => e.Department)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(e => e.ArtistDisplayName)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(e => e.Culture)
                      .HasMaxLength(1000);

                entity.Property(e => e.Medium)
                      .HasMaxLength(1000);

                entity.Property(e => e.Dimensions)
                      .HasMaxLength(1000);

                entity.Property(e => e.ObjectURL)
                      .HasMaxLength(1000);

                entity.HasOne<DepartmentEntity>()
                      .WithMany()
                      .HasPrincipalKey(d => d.DepartmentId) 
                      .HasForeignKey(e => e.DepartmentId) 
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Constituents)
                      .WithOne(c => c.ArtObject)
                      .HasForeignKey(c => c.ArtObjectId);

                entity.HasMany(e => e.Tags)
                      .WithOne(t => t.ArtObject)
                      .HasForeignKey(t => t.ArtObjectId);

                entity.HasMany(e => e.Images)
                      .WithOne(i => i.ArtObject)
                      .HasForeignKey(i => i.ArtObjectId);
            });

            modelBuilder.Entity<ConstituentEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ConstituentId)
                      .IsRequired();

                entity.Property(e => e.Name)
                      .HasMaxLength(1000);

                entity.Property(e => e.Role)
                      .HasMaxLength(1000);

                entity.Property(e => e.Gender)
                      .HasMaxLength(1000);

                entity.Property(e => e.WikidataURL)
                      .HasMaxLength(1000);

                entity.Property(e => e.ULANURL)
                      .HasMaxLength(1000);
            });

            modelBuilder.Entity<DepartmentEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.DepartmentId)
                      .IsRequired();

                entity.Property(e => e.DisplayName)
                      .IsRequired()
                      .HasMaxLength(1000);
            });

            modelBuilder.Entity<ImageEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Url)
                      .HasMaxLength(1000);
            });

            modelBuilder.Entity<TagEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Term)
                      .HasMaxLength(1000);

                entity.Property(e => e.WikidataURL)
                      .HasMaxLength(1000);

                entity.Property(e => e.AATURL)
                      .HasMaxLength(1000);
            });
        }
    }
}
