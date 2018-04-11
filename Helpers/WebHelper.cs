using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace AngularAggr.Helpers
{
public class WebHelper
{
    private ILoggerFactory _factory;

    public WebHelper(ILoggerFactory factory)
    {
        _factory = factory;
    }
    public string DownloadContent(string loadUrl)
    {
        // Using RestSharp
            RestClient client = new RestClient(loadUrl);
            RestRequest request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            // Checking for Exception
            if (response.ErrorException != null)
            {
                throw new WebException(response.ErrorMessage, response.ErrorException);
            }

            string content = response.Content;
            if (content == null)
            {
                return null;
            }
            var byteOrderMark = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            // Removing Byte Order Mark from Content
            if (content.StartsWith(byteOrderMark))
            {
                content = content.Replace(byteOrderMark, string.Empty);
            }
            return content;
    }
  }
}