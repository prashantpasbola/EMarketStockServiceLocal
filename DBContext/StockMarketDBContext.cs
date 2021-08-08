using Microsoft.EntityFrameworkCore;
using stockservice.Model;

namespace stockservice.DBContext
{
    public class StockMarketDBContext : DbContext
    {
        public StockMarketDBContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Stock> Stocks { get; set; }
    }
}
