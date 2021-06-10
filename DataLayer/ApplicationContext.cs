using DataLayer.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataLayer
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(): base("ApplicationContext")
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<MemberHolding> MemberHoldings { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransactionHistory> AccountTransactionHistories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StockMarketOffDates> StockMarketOffDates { get; set; }
        public DbSet<StockMarketTime> StockMarketTimes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
