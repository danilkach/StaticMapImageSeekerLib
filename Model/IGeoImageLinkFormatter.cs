namespace StaticMapImageSeeker.Model
{
    public interface IGeoImageLinkFormatter
    {
        string APIKey { get; }
        string StaticMapImageServiceURL { get; }
        string GetImageStringURL(string rootJSON, int polygonMultiplier, int imageWidth, int imageHeight);
    }
}
