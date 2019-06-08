using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MVCApplication.Models
{
    public class RSSContext : DbContext
    {
        public RSSContext() : base("RSSFeed")
        {
        }
        public DbSet<RSSSource> RSSSources { get; set; }
        public DbSet<RSSFeed> RSSFeeds { get; set; }
    }
}