using StaticMapImageSeeker.Model;
using StaticMapImageSeeker.OpenStreetMap.Model;
using System.Text.Json;
using System.Threading.Tasks;

namespace StaticMapImageSeeker.OpenStreetMap
{
    public class OSMGeoFacade : GeoFacade
    {
        public OSMGeoFacade(OSMGeocoder geocoder, OSMGeoImageLinkFormatter linkFormatter) :
            base(geocoder, linkFormatter)
        { }
    }
}
