using Mars.Plugin.Abstractions;
using Mars.Plugin.Kit.Host;
using Mars.Plugin.PluginHost;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using YandexWeatherPlugin;
using YandexWeatherPlugin.Front;
using YandexWeatherPlugin.Services;

[assembly: WebApplicationPlugin(typeof(MainYandexWeatherPlugin))]

namespace YandexWeatherPlugin;

public class MainYandexWeatherPlugin : WebApplicationPlugin
{
    public const string PluginPackageName = "mdimai666.YandexWeatherPlugin";

    public static string PluginAssemblyName = default!;

    public override void ConfigureWebApplicationBuilder(WebApplicationBuilder builder, PluginSettings settings)
    {
        PluginAssemblyName = GetType().Assembly.GetName().Name!;

        builder.Services.AddSingleton<YandexWeatherService>();
    }

    public override void ConfigureWebApplication(WebApplication app, PluginSettings settings)
    {
        app.Services.AutoHostRegisterHelper([GetType().Assembly, typeof(MainYandexWeatherPluginFront).Assembly]);

        //var logger = MarsLogger.GetStaticLogger<MainYandexWeatherPlugin>();
        //logger.LogWarning($"> {PluginPackageName} - Work!" + Locale.Username);

#if DEBUG
        app.UseDevelopingServePluginFilesDefinition(GetType().Assembly, settings, [typeof(MainYandexWeatherPluginFront).Assembly, GetType().Assembly]);
#endif

        //some option
        //var optionService = app.Services.GetRequiredService<IOptionService>();
        //optionService.RegisterOption<YandexWeatherPluginOption>(appendToInitialSiteData: true);
        //optionService.SetConstOption(new Example1PluginConstOptionForFront() { ForFrontValue = "123" }, appendToInitialSiteData: true);
    }

}
