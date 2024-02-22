using WkHtmlShared;

namespace WkHtmlToPdf;

public interface IHtmlToPdfConverter
{
    byte[] GetPdf(string html);

    byte[] GetPdf(WkHtmlDocument wkHtmlDocument);
}