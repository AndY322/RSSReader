using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RSSConsoleApp
{
    public class RSSContext : DbContext
    {
        const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=RSSFeed;Integrated Security=True";
        public RSSContext() : base(connectionString)
        {
        }
        public DbSet<RSSSource> RSSSources { get; set; }
        public DbSet<RSSFeed> RSSFeeds { get; set; }
    }
}
