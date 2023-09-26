using Microsoft.EntityFrameworkCore;
using ShopCore.Domain;

namespace Shop.data
{
    public class ShopContext : DbContext
    {
        public ShopContext
            (
                DbContextOptions<ShopContext> options
            ) : base(options) { }

        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
    }
}