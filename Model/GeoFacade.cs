using System.Drawing;
using System.Text.Json;
using System.Threading.Tasks;
using StaticMapImageSeeker.Accessors;

namespace StaticMapImageSeeker.Model
{
    public abstract class GeoFacade
    {
        public GeoFacade(IGeocoder geocoder, IGeoImageLinkFormatter linkFormatter)
        {
            this.Geocoder = geocoder;
            this.LinkFormatter = linkFormatter;
        }

        public async virtual Task<Bitmap> GetStaticMapImage(string region, int polygonMultiplier, int width, int height)
        {
            try
            {
                string geoJSON = await Geocoder.GetGeoJsonByName(region);
                string geoImageURL = LinkFormatter.GetImageStringURL(geoJSON, polygonMultiplier, width, height);
                Bitmap image = await BitmapAccessor.Download(geoImageURL);
                return image;
            }
            catch { return null; }
        }
        public IGeocoder Geocoder { get; set; }
        public IGeoImageLinkFormatter LinkFormatter { get; set; }
    }
}
