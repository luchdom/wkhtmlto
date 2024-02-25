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
        var pdfBytes = htmlToPdfConverter.GetPdf(WkHtmlDocumentHelper.GetDocument());
        return File(pdfBytes, MediaTypeNames.Application.Pdf);
    }


    
}
