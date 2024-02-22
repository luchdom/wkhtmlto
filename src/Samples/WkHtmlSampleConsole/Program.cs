using Microsoft.Extensions.DependencyInjection;
using WkHtmlToImage.Extensions;
using WkHtmlToPdf.Extensions;

namespace WkHtmlSampleConsole;

internal class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        Console.WriteLine("Type [p] for PDF or [i] for image");
        var key = Console.ReadKey();
        var service = serviceProvider.GetService<SampleService>()!;
        if (key.KeyChar == 'p')
            service.GenerateAndOpenPdf();
        else
            service.GenerateAndOpenImage();
    }

    private static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddHtmlToPdf("Render");
        serviceCollection.AddHtmlToImage("Render");
        serviceCollection.AddTransient<SampleService>();
    }
}
