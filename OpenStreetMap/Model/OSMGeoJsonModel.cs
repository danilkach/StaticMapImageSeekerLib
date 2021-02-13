using System.Collections.Generic;

namespace StaticMapImageSeeker.OpenStreetMap.Model
{
    public class OSMGeoJSON
    {
        public string type { get; set; }
        public List<object> coordinates { get; set; }
    }

    public class OSMJSONRoot
    {
        public string display_name { get; set; }
        public double importance { get; set; }
        public OSMGeoJSON geojson { get; set; }
    }

}
