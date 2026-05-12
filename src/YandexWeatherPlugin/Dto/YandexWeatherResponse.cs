using System.Text.Json.Serialization;

namespace YandexWeatherPlugin.Dto;

public record YandexWeatherResponse
{
    [JsonPropertyName("time")]
    public long Time { get; init; }

    [JsonPropertyName("clocks")]
    public Dictionary<string, Clock> Clocks { get; init; } = default!;

    //[JsonPropertyName("isSensetiveRegion")]
    //public bool IsSensitiveRegion { get; init; }
}

public record Clock
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("offset")]
    public int Offset { get; init; }

    [JsonPropertyName("offsetString")]
    public string OffsetString { get; init; } = default!;

    [JsonPropertyName("showSunriseSunset")]
    public bool ShowSunriseSunset { get; init; }

    [JsonPropertyName("sunrise")]
    public string Sunrise { get; init; } = default!;

    [JsonPropertyName("sunset")]
    public string Sunset { get; init; } = default!;

    [JsonPropertyName("isNight")]
    public bool IsNight { get; init; }

    [JsonPropertyName("skyColor")]
    public string SkyColor { get; init; } = default!;

    [JsonPropertyName("weather")]
    public Weather? Weather { get; init; } = default!;

    [JsonPropertyName("parents")]
    public List<ParentRegion> Parents { get; init; } = default!;
}

public record Weather
{
    [JsonPropertyName("temp")]
    public int Temp { get; init; }

    [JsonPropertyName("icon")]
    public string Icon { get; init; } = default!;

    [JsonPropertyName("link")]
    public string Link { get; init; } = default!;
}

public record ParentRegion
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;
}
