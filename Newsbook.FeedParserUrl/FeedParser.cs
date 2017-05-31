using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Newsbook.FeedParserUrl
{
    /// <summary>
    /// A simple RSS, RDF and ATOM feed parser.
    /// </summary>
    public class FeedParser
    {
        /// <summary>
        /// Parses the given <see cref="FeedType"/> and returns a <see cref="IList&amp;lt;Item&amp;gt;"/>.
        /// </summary>
        /// <returns></returns>
        public static Feed Parse(string url)
        {
            XDocument doc = XDocument.Load(url);
            Feed feed = new Feed();


            if (doc.ToString().IndexOf("syndication", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                try
                {
                    feed = ParseAtomSyndication(doc);
                }
                catch 
                {
                }
              
            }
            else
            {
                feed = ParseRss(doc);

                if (feed.Items.Count == 0)
                {
                    feed = ParseAtom(doc);
                }

                if (feed.Items.Count == 0)
                {
                    feed = ParseRdf(doc);
                }

                if (feed.Items.Count == 0)
                {
                    feed = ParseRssWithoutChannel(doc);
                }


                
            }


            if (feed.Items.Count == 0)
            {
                throw new NotSupportedException(string.Format("{0} não suportado.", url));
            }

            return feed;
        }

        /// <summary>
        /// Parses an Atom feed and returns a <see cref="IList&amp;lt;Item&amp;gt;"/>.
        /// </summary>
        private static Feed ParseAtom(XDocument doc)
        {
            try
            {

                var feed = from f in doc.Root.Descendants().Where(i => i.Name.LocalName == "channel")
                           select new Feed
                           {
                               Description = f.Elements().First(i => i.Name.LocalName == "description").Value,
                               Title = f.Elements().First(i => i.Name.LocalName == "title").Value,
                           };


                var feedReturn = feed.FirstOrDefault();

                // Feed/Entry
                var entries = from item in doc.Root.Elements().Where(i => i.Name.LocalName == "entry")
                              select new Item
                              {
                                  FeedType = FeedType.Atom,
                                  Content = item.Elements().First(i => i.Name.LocalName == "content").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value,
                                  PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "published").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                                  Categories = item.Elements().Where(i => i.Name.LocalName == "category").Select(x => x.ToString()).ToArray()
                              };
                feedReturn.Items = entries.ToList();
                feedReturn.Type = FeedType.Atom;
                return feedReturn;

            }
            catch
            {
                return new Feed();
            }
        }

        private static Feed ParseAtomSyndication(XDocument doc)
        {
            try
            {
                SyndicationFeed syndication = SyndicationFeed.Load(doc.CreateReader());
                Feed feedReturn = new Feed();
                var itens = syndication.Items.ToList();
                feedReturn.Title = syndication.Title.Text;
                feedReturn.Description = syndication.Description.Text;
                for (int i = 0; i < itens.Count; i++)
                {
                    Item news = new Item();

                    news.Title = itens[i].Title.Text;
                    news.PublishDate = itens[i].PublishDate.DateTime;

                    List<string> c = new List<string>();
                    for (int x = 0; x < itens[i].Categories.Count; x++)
                    {                        
                        c.Add(itens[i].Categories[x].Name);
                    }

                    news.Categories = c.ToArray();

                    
                   

                    string link = "#";
                    if (itens[i].Links != null && itens[i].Links.Count > 0)
                    {
                        link = itens[i].Links[0].Uri.ToString();
                    }

                    news.Link = link;
                    news.Content = itens[i].Summary.Text;
                    feedReturn.Items.Add(news);
                }
                
                feedReturn.Type = FeedType.Atom;
                return feedReturn;

            }
            catch
            {
                return new Feed();
            }
        }

        /// <summary>
        /// Parses an RSS feed and returns a <see cref="IList&amp;lt;Item&amp;gt;"/>.
        /// </summary>
        private static Feed ParseRss(XDocument doc)
        {
            try
            {
                // RSS/Channel/item

                var feed = from f in doc.Root.Descendants().Where(i => i.Name.LocalName == "channel")
                           select new Feed
                           {
                               Description = f.Elements().First(i => i.Name.LocalName == "description").Value,
                               Title = f.Elements().First(i => i.Name.LocalName == "title").Value,
                           };


                var feedReturn = feed.FirstOrDefault();

                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                              select new Item
                              {
                                  FeedType = FeedType.RSS,
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                                  Categories = item.Elements().Where(i => i.Name.LocalName == "category").Select(x => x.Value.ToString()).ToArray()
                              };

                feedReturn.Items = entries.ToList();
                feedReturn.Type = FeedType.RSS;
                return feedReturn;
            }
            catch
            {
                return new Feed();
            }
        }

        /// <summary>
        /// Parses an RSS feed and returns a <see cref="IList&amp;lt;Item&amp;gt;"/>.
        /// </summary>
        private static Feed ParseRssWithoutChannel(XDocument doc)
        {
            try
            {
                // RSS/Channel/item

                var feed = from f in doc.Elements().Where(i => i.Name.LocalName == "feed")
                           select new Feed
                           {
                               //Description = f.Elements().First(i => i.Name.LocalName == "description").Value,
                               Title = f.Elements().First(i => i.Name.LocalName == "title").Value,
                           };


                var feedReturn = feed.FirstOrDefault();

                var entries = from item in doc.Root.Elements().Where(i => i.Name.LocalName == "entry")
                              select new Item
                              {
                                  FeedType = FeedType.RSS,
                                  Content = item.Elements().First(i => i.Name.LocalName == "content").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "published").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                                  Categories = item.Elements().Where(i => i.Name.LocalName == "category").Select(x => x.Value.ToString()).ToArray()
                              };

                feedReturn.Items = entries.ToList();
                feedReturn.Type = FeedType.RSS;
                return feedReturn;
            }
            catch
            {
                return new Feed();
            }
        }

        /// <summary>
        /// Parses an RDF feed and returns a <see cref="IList&amp;lt;Item&amp;gt;"/>.
        /// </summary>
        private static Feed ParseRdf(XDocument doc)
        {
            try
            {
                // RSS/Channel/item

                var feed = from f in doc.Root.Descendants().Where(i => i.Name.LocalName == "channel")
                           select new Feed
                           {
                               Description = f.Elements().First(i => i.Name.LocalName == "description").Value,
                               Title = f.Elements().First(i => i.Name.LocalName == "title").Value,
                           };


                var feedReturn = feed.FirstOrDefault();

                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                              select new Item
                              {
                                  FeedType = FeedType.RSS,
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "date").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                              };

                feedReturn.Items = entries.ToList();
                feedReturn.Type = FeedType.RDF;
                return feedReturn;
            }
            catch
            {
                return new Feed();
            }
        }

        private static DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.MinValue;
        }
    }
    /// <summary>
    /// Represents the XML format of a feed.
    /// </summary>
    public enum FeedType
    {
        /// <summary>
        /// Really Simple Syndication format.
        /// </summary>
        RSS,
        /// <summary>
        /// RDF site summary format.
        /// </summary>
        RDF,
        /// <summary>
        /// Atom Syndication format.
        /// </summary>
        Atom
    }
}
