using Microsoft.EntityFrameworkCore;
using API_Usage.Models;

namespace API_Usage.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Equity> Equities { get; set; }
        //public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Performance> Performance { get; set; }
        public DbSet<MarketVolume> MarketVolume { get; set; }
        public DbSet<Gainers> Gainers { get; set; }
        public DbSet<EffSpread> EffSpread { get; set; }

        //public DbSet<Marketshare> Marketshares { get; set; }
    }
}