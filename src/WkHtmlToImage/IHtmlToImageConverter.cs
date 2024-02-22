using WkHtmlShared;

namespace WkHtmlToImage;

public interface IHtmlToImageConverter
{
    byte[] GetImage(string html);

    byte[] GetImage(WkHtmlDocument wkHtmlDocument);
}