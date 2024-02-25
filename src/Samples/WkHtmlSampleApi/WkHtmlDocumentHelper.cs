using WkHtmlShared;

namespace WkHtmlSampleApi;

public class WkHtmlDocumentHelper
{
    public static WkHtmlDocument GetDocument()
    {
        var body = System.IO.File.ReadAllText(GetFilePath("receipt.html"));
        var htmlDocument = new WkHtmlDocument(body);
        htmlDocument.GlobalOptions.ImageQuality = 100;
        htmlDocument.GlobalOptions.PageMargins.Top = 5;
        htmlDocument.GlobalOptions.PageMargins.Bottom = 10;
        htmlDocument.GlobalOptions.PageMargins.Left = 5;
        htmlDocument.GlobalOptions.PageMargins.Right = 5;

        return htmlDocument;
    }

    private static string GetFilePath(string filename)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"HtmlContent/{filename}");
    }
}
