using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class RSSSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public virtual ICollection<RSSFeed> RSSFeeds { get; set; }
    }
}