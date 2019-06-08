using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.IO;
using System.Text.RegularExpressions;

namespace RSSConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveNotExistFeeds(@"https://habr.com/ru/rss/all/all", "Хабрахабр");
            SaveNotExistFeeds(@"http://www.interfax.by/news/feed", "Интерфакс");
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey();
        }

        private static void SaveNotExistFeeds(string url, string source)
        {

            var readFeeds = GetFeed(url, source);
            List<RSSFeed> saveFeeds = null;
            RSSContext db = new RSSContext();
            try
            {
                saveFeeds = db.RSSFeeds.Where(f => f.RSSSource.Name == source).ToList();
                foreach (var feed in readFeeds)
                {
                    if (saveFeeds == null || saveFeeds.FirstOrDefault(f => f.Name == feed.Name && f.PublishDate == feed.PublishDate) == null)
                    {
                        db.RSSFeeds.Add(feed);
                    }
                }
                Console.WriteLine(string.Format("Прочитано {0} новостей из {1}, сохранено {2}", readFeeds.Count, source, db.SaveChanges()));
            }
            catch(Exception e)
            {
                Console.WriteLine(string.Format("Ошибка загрузки новостей из {0}", source));
            }
        }

        private static List<RSSFeed> GetFeed(string url, string sourceName)
        {
            SaveNotExistSource(sourceName, url);
            RSSContext db = new RSSContext();
            var result = new List<RSSFeed>();

            XmlReader xmlReader;
            var webRequest = HttpWebRequest.Create(url) as HttpWebRequest;
            SyndicationFeed feed;
            using (var response = (HttpWebResponse)webRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.Write(reader.ReadToEnd().TrimStart('\n'));
                    writer.Flush();
                    stream.Position = 0;
                    xmlReader = XmlReader.Create(stream);
                    feed = SyndicationFeed.Load(xmlReader);
                    xmlReader.Close();
                    stream.Close();
                }
            }
            
            foreach (var item in feed.Items)
            {
                result.Add(new RSSFeed()
                {
                    Name = item.Title.Text,
                    PublishDate = item.PublishDate.DateTime,
                    Description = Regex.Replace(item.Summary.Text, @"<[^>]*>", String.Empty),
                    URL = item.Id,
                    RSSSourceId = db.RSSSources.FirstOrDefault(f => f.Name == sourceName).Id
                });
            }
            return result;
        }

        private static void SaveNotExistSource(string source, string url)
        {
            RSSContext db = new RSSContext();
            var addingSource = db.RSSSources.FirstOrDefault(s => s.Name == source);
            if(addingSource == null)
            {
                db.RSSSources.Add(new RSSSource()
                {
                    Name = source,
                    URL = url
                });
                db.SaveChanges();
            }
        }
    }
}
