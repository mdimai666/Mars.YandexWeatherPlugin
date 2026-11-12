using Mars.Options.Abstractions.Services;
using Mars.Plugin.Abstractions;
using Mars.Plugin.Kit.Host;
using Mars.Server.Contracts.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using YandexWeatherPlugin;
using YandexWeatherPlugin.Front;
using YandexWeatherPlugin.Services;

[assembly: MarsPlugin(typeof(MainYandexWeatherPlugin))]

namespace YandexWeatherPlugin;

public class MainYandexWeatherPlugin : MarsPlugin
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

        //some option
        var optionService = app.Services.GetRequiredService<IOptionService>();
        //optionService.RegisterOption<YandexWeatherPluginOption>(appendToInitialSiteData: true);
        //optionService.SetConstOption(new Example1PluginConstOptionForFront() { ForFrontValue = "123" }, appendToInitialSiteData: true);

        var weatherService = app.Services.GetRequiredService<YandexWeatherService>();

        var oldSiteUrl = optionService.GetOption<SiteSettings>().SiteUrl;

        //optionService.OnOptionUpdate += opt =>
        //{
        //    if(opt is SysOptions sysOptions)
        //    {
        //        if(oldSiteUrl != sysOptions.SiteUrl)
        //        {
        //            oldSiteUrl = sysOptions.SiteUrl;
        //            weatherService.SiteDomainChanges();
        //        }
        //    }
        //};
    }

}
