using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace MVCApplication.Models
{
    public class FeedViewModel : AjaxFeedViewModel
    {
        public IPagedList<RSSFeed> Feeds { get; set; }
    }
}