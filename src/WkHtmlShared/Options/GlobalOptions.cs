using WkHtmlShared.Enums;

namespace WkHtmlShared.Options;

public class GlobalOptions : BaseOptions
{
    public GlobalOptions()
    {
        PageMargins = new Margins();
        Replace = new Dictionary<string, string>();
    }

    /// <summary>
    /// Dpi
    /// </summary>
    [OptionFlag("--dpi")]
    public int? Dpi { get; set; }

    /// <summary>
    /// Page size by type
    /// </summary>
    [OptionFlag("-s")]
    public Size? PageSize { get; set; }

    /// <summary>
    /// Horizontal page size in mm.
    /// </summary>
    /// <remarks>Has priority over <see cref="PageSize"/> but <see cref="PageHeight"/> has to be also specified.</remarks>
    [OptionFlag("--page-width")]
    public double? PageWidth { get; set; }

    /// <summary>
    /// Vertical page size in mm.
    /// </summary>
    /// <remarks>Has priority over <see cref="PageSize"/> but <see cref="PageWidth"/> has to be also specified.</remarks>
    [OptionFlag("--page-height")]
    public double? PageHeight { get; set; }

    [OptionFlag("-O")]
    public Orientation? PageOrientation { get; set; }

    /// <summary>
    /// Page margin in mm.
    /// </summary>
    public Margins PageMargins { get; set; }

    protected string GetContentType()
    {
        return "application/pdf";
    }

    [OptionFlag("-l")]
    public bool IsLowQuality { get; set; }

    [OptionFlag("--copies")]
    public int? Copies { get; set; }

    /// <summary>
    /// Make PDF in gray scale  (Default: false)
    /// </summary>
    [OptionFlag("-g")]
    public bool IsGrayScale { get; set; }

    /// <summary>
    /// Dont use 'lossless compression' on pdf objects. (Default: false)
    /// </summary>
    [OptionFlag("--no-pdf-compression")]
    public bool NoPdfCompression { get; set; }

    /// <summary>
    /// Disable compression smart strategy used by WebKit which keeps proportions of pixel/dpi bot constants (Default: false)
    /// </summary>
    [OptionFlag("--disable-smart-shrinking")]
    public bool DisableSmartShrinking { get; set; }

    /// <summary>
    /// Header html content or path
    /// </summary>
    [OptionFlag("--header-html")]
    public string HeaderHtml { get; set; }

    /// <summary>
    /// Header spacing
    /// </summary>
    [OptionFlag("--header-spacing")]
    public int? HeaderSpacing { get; set; }

    /// <summary>
    /// Dictionary containing values to be replaced on HTML
    /// </summary>
    [OptionFlag("--replace")]
    public Dictionary<string, string> Replace { get; set; }

    /// <summary>
    /// Footer html content or path
    /// </summary>
    [OptionFlag("--footer-html")]
    public string FooterHtml { get; set; }

    /// <summary>
    /// Footer spacing
    /// </summary>
    [OptionFlag("--footer-spacing")]
    public int? FooterSpacing { get; set; }

    /// <summary>
    /// Quality compression. (Default 94)
    /// </summary>
    [OptionFlag("--image-quality")]
    public int? ImageQuality { get; set; }

    /// <summary>
    /// Dpi (Default 600)
    /// </summary>
    [OptionFlag("--image-dpi")]
    public int? ImageDpi { get; set; }

    public override string GetConvertOptionFlags()
    {
        return $"{PageMargins} {base.GetConvertOptionFlags()}";
    }
}