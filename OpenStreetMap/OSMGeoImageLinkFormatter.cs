using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using StaticMapImageSeeker.Model;
using StaticMapImageSeeker.OpenStreetMap.Model;

namespace StaticMapImageSeeker.OpenStreetMap
{
    public class OSMGeoImageLinkFormatter : IGeoImageLinkFormatter
    {
        public string APIKey => null; 
        public string StaticMapImageServiceURL => "http://osm-static-maps.herokuapp.com/";
        public string GetImageStringURL(string rootJSON, int polygonMultiplier, int imageWidth, int imageHeight)
        {
            var root = JsonSerializer.Deserialize<List<OSMJSONRoot>>(rootJSON).
                OrderByDescending(r => r.importance);
            double maxImportance = root.ElementAt(0).importance;
            List<string> geojsons = new List<string>();
            List<OSMGeoJSON> geoJSONs = new List<OSMGeoJSON>();
            foreach (OSMJSONRoot r in root)
            {
                if (r.importance == maxImportance)
                {
                    geoJSONs.Add(r.geojson);
                }
                else
                    break;
            }
            OSMGeoJSON polygon = geoJSONs.OrderBy(gjsn => Enum.Parse(typeof(OSMPolygonTypes), gjsn.type)).ElementAt(0);
            int urlByteLimit = 8192;
            string url;
            OSMPolygonTypes polygonType = (OSMPolygonTypes)Enum.Parse(typeof(OSMPolygonTypes), polygon.type);
            if (polygonMultiplier < 2)
            {
                url = this.StaticMapImageServiceURL + $"?geojson={JsonSerializer.Serialize<OSMGeoJSON>(polygon).Replace("[\"[", "[[").Replace("]\"]", "]]")}" + $"&height={imageHeight}&width={imageWidth}";
                if (url.Length > urlByteLimit)
                    return null;
            }
            else
                do
                {
                    for (int i = 0; i < polygon.coordinates.Count; i++)
                    {
                        switch (polygonType)
                        {
                            case OSMPolygonTypes.MultiPolygon:
                                List<List<List<double>>> polygons =
                                    JsonSerializer.Deserialize<List<List<List<double>>>>(polygon.coordinates[i].ToString());
                                for (int k = 3; k < polygons.ElementAt(0).Count; k += polygonMultiplier - 1)
                                    polygons.ElementAt(0).RemoveAt(k);
                                polygon.coordinates[i] = JsonSerializer.Serialize(polygons);
                                break;
                            case OSMPolygonTypes.Polygon:
                                List<List<double>> simplepolygons =
                                    JsonSerializer.Deserialize<List<List<double>>>(polygon.coordinates[i].ToString());
                                for (int k = 3; k < simplepolygons.Count; k += polygonMultiplier - 1)
                                    simplepolygons.RemoveAt(k);
                                polygon.coordinates[i] = JsonSerializer.Serialize(simplepolygons);
                                break;
                        }
                    }
                    url = this.StaticMapImageServiceURL + $"?geojson={JsonSerializer.Serialize<OSMGeoJSON>(polygon).Replace("[\"[", "[[").Replace("]\"]", "]]")}" + $"&height={imageHeight}&width={imageWidth}";
                } while (url.Length > urlByteLimit);
            //This nonsense was just hardcoded because of the (currently) unknown json serializing issue
            return url.Replace("{", "%7B").Replace("}", "%7D").Replace("]\",\"[", "],[");
        }
    }
}
