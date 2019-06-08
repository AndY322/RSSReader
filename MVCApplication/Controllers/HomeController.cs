using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApplication.Models;
using PagedList;
using PagedList.Mvc;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {
        const int pageSize = 10;

        RSSContext db = new RSSContext();
        public ActionResult Index(int? page, string sortField, int? sourceId)
        {
            var feeds = db.RSSFeeds.Where(f => f.RSSSourceId == sourceId).ToList();
            var feed = new FeedViewModel()
            {
                SourceId = sourceId ?? 0,
                SortField = sortField
            };
            if(sourceId == 0 || sourceId == null)
            {
                feeds = db.RSSFeeds.ToList();
            }
            feed.Feeds = GetSortedFeeds(sortField, feeds, page);
            var sources = db.RSSSources;
            ViewBag.sources = new SelectList(sources, "Id", "Name");
            return View(feed);
        }

        public ActionResult GetFeeds(FeedViewModel feedViewModel)
        {
            return RedirectToAction("Index", new
            {
                sortField = feedViewModel.SortField,
                sourceId = feedViewModel.SourceId
            });
        }

        public ActionResult FeedsAjax()
        {
            var feedViewModel = new AjaxFeedViewModel();
            var sources = db.RSSSources.ToList();
            ViewBag.sources = new SelectList(sources, "Id", "Name");
            return View(feedViewModel);
        }

        public ActionResult GetFeedsTable(int? page, string sortField,int? sourceId)
        {
            var feedViewModel = new FeedViewModel();
            var feeds = db.RSSFeeds.Where(f => f.RSSSourceId == sourceId).ToList();
            if(sourceId == 0 || sourceId == null)
            {
                feeds = db.RSSFeeds.ToList();
            }
            feedViewModel.Feeds = GetSortedFeeds(sortField, feeds, page);
            return PartialView(feedViewModel);
        }

        private IPagedList<RSSFeed> GetSortedFeeds(string sortField, List<RSSFeed> feeds, int? page)
        {
            IEnumerable<RSSFeed> sortedFeeds;
            switch (sortField)
            {
                case "Date":
                    sortedFeeds = feeds.OrderBy(f => f.PublishDate);
                    break;
                case "Source":
                    sortedFeeds = feeds.OrderBy(f => f.RSSSourceId);
                    break;
                default:
                    sortedFeeds = feeds;
                    break;
            }
            return sortedFeeds.ToPagedList(page ?? 1, pageSize);
        }
    }
}