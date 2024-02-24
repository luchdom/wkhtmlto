using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net.Mime;
using WkHtmlShared;
using WkHtmlToPdf;

namespace WkHtmlSampleApi.Controllers;
[Route("[controller]")]
[ApiController]
public class PdfController(IHtmlToPdfConverter htmlToPdfConverter) : ControllerBase
{
    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK, MediaTypeNames.Application.Pdf)]
    public IActionResult GeneratePdf()
    {
        var pdfBytes = htmlToPdfConverter.GetPdf(GetDocument());
        return File(pdfBytes, MediaTypeNames.Application.Pdf);
    }


    private WkHtmlDocument GetDocument()
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

    private string GetFilePath(string filename)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"HtmlContent/{filename}");
    }
}
