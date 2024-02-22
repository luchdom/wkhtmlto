using System.Diagnostics;
using WkHtmlShared;
using WkHtmlToImage;
using WkHtmlToPdf;

namespace WkHtmlSampleConsole;
public class SampleService(
    IHtmlToPdfConverter htmlToPdfConverter,
    IHtmlToImageConverter htmlToImageConverter)
{
    private readonly IHtmlToPdfConverter _htmlToPdfConverter = htmlToPdfConverter;
    private readonly IHtmlToImageConverter _htmlToImageConverter = htmlToImageConverter;

    private WkHtmlDocument GetDocument()
    {
        var body = File.ReadAllText(GetFilePath("receipt.html"));
        var htmlDocument = new WkHtmlDocument(body);
        htmlDocument.GlobalOptions.ImageQuality = 100;
        htmlDocument.GlobalOptions.PageMargins.Top = 5;
        htmlDocument.GlobalOptions.PageMargins.Bottom = 10;
        htmlDocument.GlobalOptions.PageMargins.Left = 5;
        htmlDocument.GlobalOptions.PageMargins.Right = 5;

        htmlDocument.GlobalOptions.Replace = GetReplacementDict();
        return htmlDocument;
    }

    public void GenerateAndOpenImage()
    {
        var outputPath = $"{Path.GetTempPath()}output_image_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.jpg";
        var pdfBytes = _htmlToImageConverter.GetImage(GetDocument());
        File.WriteAllBytes(outputPath, pdfBytes);
        OpenFile(outputPath);
    }

    public void GenerateAndOpenPdf()
    {
        var outputPath = $"{Path.GetTempPath()}output_pdf_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.pdf";
        var pdfBytes = _htmlToPdfConverter.GetPdf(GetDocument());
        File.WriteAllBytes(outputPath, pdfBytes);
        OpenFile(outputPath);
    }

    private void OpenFile(string outputPath)
    {
        Process p = new Process();
        p.StartInfo = new ProcessStartInfo(outputPath)
        {
            UseShellExecute = true
        };
        p.Start();
        p.WaitForExit();
    }

    private string GetFilePath(string filename)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"HtmlContent/{filename}");
    }

    private Dictionary<string, string> GetReplacementDict()
    {
        var dict = new Dictionary<string, string>
        {
            { "_NAME_", "John Doe" },
            { "_DATE_", "31/05/2019" }
        };
        return dict;
    }
}
