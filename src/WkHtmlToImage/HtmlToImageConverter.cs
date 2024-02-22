using System.Runtime.InteropServices;
using System.Text;
using WkHtmlShared;

namespace WkHtmlToImage;

public class HtmlToImageConverter : IHtmlToImageConverter
{
    public static string HtmlToImageRendererPath { get; set; }
    public static bool IsWindows { get; set; }
    public HtmlToImageConverter(string htmlToPdfRendererPath)
    {
        IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        HtmlToImageRendererPath =
               IsWindows
               ? Path.Combine(htmlToPdfRendererPath, "Windows", "wkhtmltoimage.exe")
               : Path.Combine(htmlToPdfRendererPath, "Linux", "wkhtmltoimage");

        if (!File.Exists(HtmlToImageRendererPath))
            throw new Exception("Render not found. Expected path: " + HtmlToImageRendererPath);
    }
    public byte[] GetImage(string html)
    {
        var wkHtmlDocument = new WkHtmlDocument(html);
        return GetImage(wkHtmlDocument);
    }

    public byte[] GetImage(WkHtmlDocument wkHtmlDocument)
    {
        return WkHtmlDriver.Convert(GetOptions(wkHtmlDocument), wkHtmlDocument.HtmlContent, HtmlToImageRendererPath);
    }

    private string GetOptions(WkHtmlDocument wkHtmlDocument)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(wkHtmlDocument.GlobalOptions.GetConvertOptionFlags());
        return sb.ToString();
    }
}