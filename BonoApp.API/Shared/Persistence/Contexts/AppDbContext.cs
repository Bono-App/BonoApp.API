using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BonoApp.API.Shared.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User.Domain.Models.User> Users { get; set; }

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
        }
    }
}