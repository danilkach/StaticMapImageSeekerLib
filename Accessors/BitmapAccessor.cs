using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace StaticMapImageSeeker.Accessors
{
    public static class BitmapAccessor
    {
        public static async Task<Bitmap> Download(string uriString, string userAgent = "Some user agent")
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(new Uri(uriString));
                request.UserAgent = userAgent;
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                using (Stream stream = response.GetResponseStream())
                    return new Bitmap(stream);
            }
            catch { return null; }
        }
    }
}
