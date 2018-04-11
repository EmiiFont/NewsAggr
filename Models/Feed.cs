using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace AngularAggr.Models
{
    public class Feed
    {
      public int FeedId { get; set; }
      public string FeedName { get; set; }
      public string Url { get; set; }
      public string FeedType { get; set; }
      public string UpdateDate { get; set; }

    }
}