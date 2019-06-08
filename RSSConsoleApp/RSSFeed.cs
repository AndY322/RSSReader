﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSConsoleApp
{
    public class RSSFeed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public int RSSSourceId { get; set; }
        public virtual RSSSource RSSSource { get; set; }
    }
}
