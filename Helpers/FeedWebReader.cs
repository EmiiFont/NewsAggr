using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.Extensions.Logging;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using RestSharp;
using System.Text.RegularExpressions;
using System.Linq;
using AngularAggr.Models;

namespace AngularAggr.Helpers
{
    public class FeedWebReader
    {
        private ILoggerFactory _factory;
        public static List<FeedItem> _feedItems = new List<FeedItem>();

        public FeedWebReader(ILoggerFactory factory)
        {
            _factory = factory;
        }
        
        private List<Feed> GetFeedList()
        {
           
           var feed3 = new Feed();
            feed3.Url = "https://bitcoin.net.do/feed/";
            feed3.FeedType = "RSS";
             
            var feed4 = new Feed();
            feed4.Url = "https://feeds.feedburner.com/diariobitcoin/ZGXu";
            feed4.FeedType = "ATOM";
            
            var feed2 = new Feed();
            feed2.Url = "https://feeds.feedburner.com/cointelegraph/cNEF";
            feed2.FeedType = "RSS";

            var feed1 = new Feed();
            feed1.Url = "https://feeds.feedburner.com/http/criptonoticiascom/feed";
            feed1.FeedType = "RSS";

            return new List<Feed> {feed3, feed2, feed1};
        }
       
        public List<FeedItem> GetFeed(int page = 1, int pageSize = 10)
        {
            var listOfCryptoSites = GetFeedList();
            var rssItems = _feedItems;
            var processitems = true;

            var smithWaterman = new SmithWaterman();

            if(_feedItems.Any())
            {
              var firstItem =  _feedItems.OrderByDescending(b => b.Published).First();
              if(firstItem.Published.Date >= DateTime.Now.Date) processitems = false;
            } 

            if(processitems)
            {
                foreach(var feed in listOfCryptoSites)
                {

                    using (var xmlReader = XmlReader.Create(feed.Url, new XmlReaderSettings() { Async = false }))
                    {
                        ISyndicationFeedReader feedObject = new RssFeedReader(xmlReader);
                    
                        if(feed.FeedType == "ATOM"){
                            feedObject = new AtomFeedReader(xmlReader);
                        }
                        
                        while (feedObject.Read().Result)
                        {
                        switch (feedObject.ElementType)
                            {
                                // Read Image
                                case SyndicationElementType.Image:
                                    ISyndicationImage image = feedObject.ReadImage().Result;
                                    break;

                                // Read Item
                                case SyndicationElementType.Item:
                                    ISyndicationItem item = feedObject.ReadItem().Result;

                                    var feedItem = new FeedItem();
                                    feedItem.Title = item.Title;
                                    feedItem.Links = item.Links;
                                    feedItem.Url = item.Id;
                                    feedItem.Categories = item.Categories;
                                    feedItem.Contributors = item.Contributors;
                                    feedItem.Published = item.Published;
                                    feedItem.LastUpdated = item.LastUpdated;
                                    feedItem.Description = item.Description.Replace("data-src", "src");
                                
                                    rssItems.Add(feedItem);
                                    break;
                            }
                        }
                    }
                }

                var itemsToProcess = rssItems.ToArray();
                for(var i=0; i <= itemsToProcess.Length - 1; i++)
                {
                    for(var j= i+1; j <= itemsToProcess.Length - 1; j++)
                    {
                        if((smithWaterman.compare(itemsToProcess[i].Title, itemsToProcess[j].Title) * 100) > 50 )
                        {
                          rssItems.Remove(itemsToProcess[j]);
                        }
                    }
                }
            }

           var skip = page * pageSize;
           return rssItems.OrderByDescending(d => d.Published).Skip(skip).Take(pageSize).ToList();
        }
    }
}
