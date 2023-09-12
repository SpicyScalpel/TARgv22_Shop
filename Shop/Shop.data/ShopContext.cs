using Microsoft.EntityFrameworkCore;
using ShopCore.Domain;

namespace Shop.data
{
    public class ShopContext:DbContext
    {
        public ShopContext
            (
                DbContextOptions<ShopContext> options
            ) : base( options ) { }

        public DbSet<Spaceship> Spaceship { get; set; }
    }
}