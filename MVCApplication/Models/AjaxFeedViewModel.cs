using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCApplication.Models
{
    public class AjaxFeedViewModel
    {
        public RSSSource Source { get; set; }
        [Display(Name = "Источник ленты")]
        public int SourceId { get; set; }
        public string SortField { get; set; }
        public int? Page { get; set; }
    }
}