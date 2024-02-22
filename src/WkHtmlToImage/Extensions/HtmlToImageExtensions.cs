using Microsoft.Extensions.DependencyInjection;

namespace WkHtmlToImage.Extensions;

public static class HtmlToImageExtensions
{
    /// <summary>
    /// Configure the converter as Singleton 
    /// with the default path as "HtmlToImageRenderer"
    /// </summary>
    public static IServiceCollection AddHtmlToImage(this IServiceCollection services)
    {
        return services.AddHtmlToImage("HtmlToImageRenderer");
    }

    /// <summary>
    /// Configure the converter as Singleton
    /// </summary>
    /// <param name="htmlToPdfRelativePath">Relative path to the render
    public static IServiceCollection AddHtmlToImage(this IServiceCollection services, string htmlToImageRendererRelativePath)
    {
        var htmlToImageRendererPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, htmlToImageRendererRelativePath);

        if (!Directory.Exists(htmlToImageRendererPath))
        {
            throw new Exception("Folder containing wkhtmltoimage not found. Searched for " + htmlToImageRendererPath);
        }
        services.AddSingleton<IHtmlToImageConverter>(provider =>
        {
            return new HtmlToImageConverter(htmlToImageRendererPath);
        });
        return services;
    }
}