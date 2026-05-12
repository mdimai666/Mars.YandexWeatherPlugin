using Mars.Plugin.Front;
using Mars.Plugin.Front.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace YandexWeatherPlugin.Front;

public class MainYandexWeatherPluginFront : IWebAssemblyPluginFront
{
    public void ConfigureServices(WebAssemblyHostBuilder builder)
    {
    }

    public void ConfigureApplication(WebAssemblyHost app)
    {
        app.Services.AutoFrontRegisterHelper([GetType().Assembly]);
    }

}
