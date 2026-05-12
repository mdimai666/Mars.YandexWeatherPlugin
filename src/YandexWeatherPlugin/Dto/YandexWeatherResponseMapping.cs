using YandexWeatherPlugin.Models;
using YandexWeatherPlugin.Shared.Dto.Weather;

namespace YandexWeatherPlugin.Dto;

public static class YandexWeatherResponseMapping
{
    public static WeatherWidgetResponse ToResponse(this WeatherWidget entity)
        => new()
        {
            Html = entity.Html,
            Title = entity.Title,
            StateDescription = entity.StateDescription,
            CelsiusText = entity.CelsiusText,
            IconCode = entity.IconCode,
            IconUrl = entity.IconUrl,
            IconUrlFull = entity.IconUrlFull,
            Weather = entity.Weather.ToResponse(),
            LocationTime = entity.LocationTime.ToResponse(),
        };

    public static WeatherDataResponse ToResponse(this WeatherData entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            SkyColor = entity.SkyColor,
            Temp = entity.Temp,
            Icon = entity.Icon,
            Link = entity.Link,
            Parents = entity.Parents.Select(x => x.ToResponse()).ToArray(),
        };

    public static LocationTimeDataResponse ToResponse(this LocationTimeData entity)
        => new()
        {
            Offset = entity.Offset,
            OffsetString = entity.OffsetString,
            ShowSunriseSunset = entity.ShowSunriseSunset,
            Sunrise = entity.Sunrise,
            Sunset = entity.Sunset,
            IsNight = entity.IsNight,
        };

    public static ParentRegionDataResponse ToResponse(this ParentRegionData entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name
        };

}
