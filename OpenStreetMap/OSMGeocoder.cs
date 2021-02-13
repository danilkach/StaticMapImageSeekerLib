using StaticMapImageSeeker.Model;
using StaticMapImageSeeker.Accessors;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StaticMapImageSeeker.OpenStreetMap
{
    public class OSMGeocoder : IGeocoder
    {
        public string GeoCoderURL => "https://nominatim.openstreetmap.org/search/";
        public async Task<string> GetGeoJsonByName(string name)
        {
            NameValueCollection query = new NameValueCollection();
            query.Add("q", name);
            query.Add("format", "json");
            query.Add("polygon_geojson", "1");
            Uri uri = new Uri(this.GeoCoderURL);
            UriBuilder builder = new UriBuilder(this.GeoCoderURL);
            builder.Query = String.Join("&", query.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(query[a], Encoding.UTF8)));
            return await JsonAccessor.Download(builder.ToString());
        }
    }
}
