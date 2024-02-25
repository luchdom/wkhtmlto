using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WkHtmlToImage;
using WkHtmlToPdf;

namespace WkHtmlSampleApi.Controllers;
[Route("[controller]")]
[ApiController]
public class ImageController(IHtmlToImageConverter htmlToImageConverter) : ControllerBase
{
    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK, MediaTypeNames.Image.Jpeg)]
    public IActionResult GenerateImage()
    {
        var pdfBytes = htmlToImageConverter.GetImage(WkHtmlDocumentHelper.GetDocument());
        return File(pdfBytes, MediaTypeNames.Image.Jpeg);
    }
}
