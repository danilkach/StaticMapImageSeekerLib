using System.Threading.Tasks;

namespace StaticMapImageSeeker.Model
{
    public interface IGeocoder
    {
        Task<string> GetGeoJsonByName(string name);
        string GeoCoderURL { get; }
    }
}
