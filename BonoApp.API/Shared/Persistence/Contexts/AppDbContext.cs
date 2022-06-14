using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BonoApp.API.Shared.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User.Domain.Models.User> Users { get; set; }
        
        public DbSet<Bond> Bonds { get; set; }

        private readonly IConfiguration _configuration;
        
        public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User.Domain.Models.User>().ToTable("Users");
            builder.Entity<User.Domain.Models.User>().HasKey(p => p.Id);
            builder.Entity<User.Domain.Models.User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User.Domain.Models.User>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<User.Domain.Models.User>().Property(p => p.LastName).IsRequired().HasMaxLength(30);
            builder.Entity<User.Domain.Models.User>().Property(p => p.Email).IsRequired().HasMaxLength(60);
            builder.Entity<User.Domain.Models.User>().Property(p => p.Password).IsRequired().HasMaxLength(30);

            builder.Entity<User.Domain.Models.User>()
                .HasMany(p => p.Bonds)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
            
            builder.Entity<Bond>().ToTable("Bonds");
            builder.Entity<Bond>().HasKey(p => p.Id);
            builder.Entity<Bond>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Bond>().Property(p => p.NominalValue).IsRequired();
            builder.Entity<Bond>().Property(p => p.CommercialValue).IsRequired();
            builder.Entity<Bond>().Property(p => p.NumberAnios).IsRequired();
            builder.Entity<Bond>().Property(p => p.CouponFrequency).IsRequired().HasMaxLength(60);
            builder.Entity<Bond>().Property(p => p.DayByAnios).IsRequired();
            builder.Entity<Bond>().Property(p => p.RateType).IsRequired().HasMaxLength(60);
            builder.Entity<Bond>().Property(p => p.Capitalization).IsRequired().HasMaxLength(60);
            builder.Entity<Bond>().Property(p => p.InterestRate).IsRequired();
            builder.Entity<Bond>().Property(p => p.Discount).IsRequired();
            builder.Entity<Bond>().Property(p => p.IncomeTax).IsRequired();
            builder.Entity<Bond>().Property(p => p.BroadcastDate).IsRequired();
            
            builder.UseSnakeCaseNamingConvention();
        }
    }
}