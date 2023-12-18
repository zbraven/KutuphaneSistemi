using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ZeroToHeroZBS.Models;

namespace ZeroToHeroZBS.Utility
{
    public class UygulamaDbContext:DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options):base(options)
        {
        }

        public DbSet<KitapTuru> KitapTurleri => Set<KitapTuru>();
        public DbSet<Kitap> Kitaplar => Set<Kitap>();
        public DbSet<Kiralama> Kiralamalar => Set<Kiralama>();

    }
}
