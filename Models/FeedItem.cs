using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.SyndicationFeed;
using RestSharp;

namespace AngularAggr.Models
{
    public class FeedItem
    {
      public int FeedItemId { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public IEnumerable<ISyndicationCategory> Categories { get; set;}
      public IEnumerable<ISyndicationPerson> Contributors { get; set;}
      public IEnumerable<ISyndicationLink> Links { get; set;}
      public DateTimeOffset LastUpdated { get; set;}
      public DateTimeOffset Published { get; set;}
        public string Url { get; set; }
    }
}