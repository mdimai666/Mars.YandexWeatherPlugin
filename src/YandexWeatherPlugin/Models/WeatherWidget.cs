using Mars.Core.Extensions;
using YandexWeatherPlugin.Dto;
using YandexWeatherPlugin.Services;

namespace YandexWeatherPlugin.Models;

public record WeatherWidget
{
    public string Html { get; }
    public string Title { get; }
    public string StateDescription { get; }
    public string CelsiusText { get; }
    public string IconCode { get; }
    public string IconUrl { get; }
    public string IconUrlFull { get; }

    public WeatherData Weather { get; }
    public LocationTimeData LocationTime { get; }

    public WeatherWidget(YandexWeatherResponse response, string siteUrl)
    {
        var clock = response.Clocks.Values.First();
        Weather = new WeatherData(clock);
        LocationTime = new LocationTimeData(clock, response.Time);
        IconCode = Weather.Icon?.Replace('-', '_') ?? "";
        if (IconCode.IsNotNullOrEmpty())
        {
#if DEBUG
            var weatherDescription = GeoCodesDict.IconDescription.GetValueOrDefault(IconCode) ?? "Unknown";
#else
            var weatherDescription = GeoCodesDict.IconDescription.GetValueOrDefault(IconCode) ?? ""; 
#endif
            weatherDescription = weatherDescription.Split('(', 2)[0];
            StateDescription = weatherDescription;
            Title = $"{Weather.Temp}°C {StateDescription}";
            CelsiusText = $"{Weather.Temp}°C";
            IconUrl = $"/_plugin/{MainYandexWeatherPlugin.PluginAssemblyName}/icons/{IconCode}.svg";
            IconUrlFull = siteUrl.TrimEnd('/') + IconUrl;
            Html = $"<div class=\"ywp-yandex-weather\"><img src=\"{IconUrlFull}\" data-icon-code=\"{IconCode}\"> {Title}</div>";
        }
        else
        {
            Title = "";
            StateDescription = "";
            CelsiusText = "";
            IconUrl = "";
            IconUrlFull = "";
            Html = "";
        }

    }
}

public record WeatherData
{
    public int Id { get; }
    public string Name { get; }
    public string SkyColor { get; }
    public int Temp { get; }
    public string Icon { get; }
    public string Link { get; }
    public ParentRegionData[] Parents { get; }

    public WeatherData(Clock clock)
    {
        Id = clock.Id;
        Name = clock.Name;
        SkyColor = clock.SkyColor;
        Temp = clock.Weather?.Temp ?? 0;
        Icon = clock.Weather?.Icon ?? "";
        Link = clock.Weather?.Link ?? "";
        Parents = clock.Parents.Select(p => new ParentRegionData(p)).ToArray();
    }
}

public record LocationTimeData
{
    private DateTimeOffset DateTime => DateTimeOffset.UtcNow.AddMilliseconds(Offset);

    public TimeOnly Time => TimeOnly.FromDateTime(DateTime.DateTime);
    public long TimeUnixTimeMillis => DateTime.ToUnixTimeMilliseconds();
    public int Offset { get; }
    public string OffsetString { get; }
    public bool ShowSunriseSunset { get; }
    public string Sunrise { get; }
    public string Sunset { get; }
    public bool IsNight { get; }

    public LocationTimeData(Clock clock, long time)
    {
        Offset = clock.Offset;
        OffsetString = clock.OffsetString;
        ShowSunriseSunset = clock.ShowSunriseSunset;
        Sunrise = clock.Sunrise;
        Sunset = clock.Sunset;
        IsNight = clock.IsNight;
    }
}

public record ParentRegionData
{
    public int Id { get; }
    public string Name { get; }

    public ParentRegionData(ParentRegion parentRegion)
    {
        Id = parentRegion.Id;
        Name = parentRegion.Name;
    }
}
