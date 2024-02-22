using System.Runtime.InteropServices;
using System.Text;
using WkHtmlShared;

namespace WkHtmlToPdf;

public class HtmlToPdfConverter : IHtmlToPdfConverter
{
    public static string HtmlToPdfRendererPath { get; set; }
    public static bool IsWindows { get; set; }
    public HtmlToPdfConverter(string htmlToPdfRendererPath)
    {
        IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        HtmlToPdfRendererPath =
               IsWindows
               ? Path.Combine(htmlToPdfRendererPath, "Windows", "wkhtmltopdf.exe")
               : Path.Combine(htmlToPdfRendererPath, "Linux", "wkhtmltopdf");

        if (!File.Exists(HtmlToPdfRendererPath))
            throw new Exception("Render not found. Expected path: " + HtmlToPdfRendererPath);
    }
    public byte[] GetPdf(string html)
    {
        var wkHtmlDocument = new WkHtmlDocument(html);
        return GetPdf(wkHtmlDocument);
    }

    public byte[] GetPdf(WkHtmlDocument wkHtmlDocument)
    {
        return WkHtmlDriver.Convert(GetOptions(wkHtmlDocument), wkHtmlDocument.HtmlContent, HtmlToPdfRendererPath);
    }

    private string GetOptions(WkHtmlDocument wkHtmlDocument)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(wkHtmlDocument.GlobalOptions.GetConvertOptionFlags());
        return sb.ToString();
    }
}