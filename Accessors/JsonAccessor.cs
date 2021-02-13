using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace StaticMapImageSeeker.Accessors
{
    public static class JsonAccessor
    {
        public static async Task<string> Download(string uriString)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(new Uri(uriString));
                request.UserAgent = "Some user agent";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    return reader.ReadToEnd();
            }
            catch { return null; }
        }
    }
}
