using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcTemplate.Entities.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Sample> samples { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<MarketPlace> MarketPlaces { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UploadImageLog> UploadImageLogs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<LandingPage> LandingPages { get; set; }
        public DbSet<PdfFile> PdfFiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
