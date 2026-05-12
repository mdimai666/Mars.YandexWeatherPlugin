using System.Text.Json.Serialization;

namespace YandexWeatherPlugin.Shared.Dto.Weather;

public record WeatherWidgetResponse
{
    public required string Html { get; init; }
    public required string Title { get; init; }
    public required string StateDescription { get; init; }
    public required string CelsiusText { get; init; }
    public required string IconCode { get; init; }
    public required string IconUrl { get; init; }
    public required string IconUrlFull { get; init; }

    public required WeatherDataResponse Weather { get; init; }
    public required LocationTimeDataResponse LocationTime { get; init; }

}

public record WeatherDataResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string SkyColor { get; init; }
    public required int Temp { get; init; }
    public required string Icon { get; init; }
    public required string Link { get; init; }
    public required ParentRegionDataResponse[] Parents { get; init; }
}

public record LocationTimeDataResponse
{
    private DateTimeOffset DateTime => DateTimeOffset.UtcNow.AddMilliseconds(Offset);

    [JsonIgnore]
    public TimeOnly Time => TimeOnly.FromDateTime(DateTime.DateTime);

    [JsonPropertyName("time")]
    public string TimeString => Time.ToString("HH:mm:ss");
    public long TimeUnixTimeMillis => DateTime.ToUnixTimeMilliseconds();

    public required int Offset { get; init; }
    public required string OffsetString { get; init; }
    public required bool ShowSunriseSunset { get; init; }
    public required string Sunrise { get; init; }
    public required string Sunset { get; init; }
    public required bool IsNight { get; init; }

}
public record ParentRegionDataResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}
