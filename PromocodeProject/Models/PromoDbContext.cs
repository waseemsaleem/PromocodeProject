using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PromoCodeProject.Models
{
    public partial class PromoDbContext : DbContext
    {
        public PromoDbContext()
        {
        }
        public PromoDbContext(DbContextOptions<PromoDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public virtual DbSet<PromoCodes> PromoCodes { get; set; }
        public virtual DbSet<ClientSecrets> ClientSecrets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromoCodes>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.PromoCode).HasMaxLength(200);
            });
            modelBuilder.Entity<ClientSecrets>(entity =>
            {
                entity.Property(e => e.AppKey)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.AppValue)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });
        }
    }
}
