using Microsoft.Extensions.DependencyInjection;

namespace WkHtmlToPdf.Extensions;

public static class HtmlToPdfExtensions
{
    /// <summary>
    /// Configure the converter as Singleton 
    /// with the default path as "HtmlToPdfRenderer"
    /// </summary>
    public static IServiceCollection AddHtmlToPdf(this IServiceCollection services)
    {
        return services.AddHtmlToPdf("HtmlToPdfRenderer");
    }

    /// <summary>
    /// Configure the converter as Singleton
    /// </summary>
    /// <param name="htmlToPdfRelativePath">Relative path to the render
    public static IServiceCollection AddHtmlToPdf(this IServiceCollection services, string htmlToPdfRendererRelativePath)
    {
        var htmlToPdfRendererPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, htmlToPdfRendererRelativePath);

        if (!Directory.Exists(htmlToPdfRendererPath))
        {
            throw new Exception("Folder containing wkhtmltopdf not found. Searched for " + htmlToPdfRendererPath);
        }
        services.AddSingleton<IHtmlToPdfConverter>(provider =>
        {
            return new HtmlToPdfConverter(htmlToPdfRendererPath);
        });
        return services;
    }
}