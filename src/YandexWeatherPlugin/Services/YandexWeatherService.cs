using System.Text.Json;
using Flurl.Http;
using Mars.Host.Shared.Dto.Files;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;
using YandexWeatherPlugin.Dto;
using YandexWeatherPlugin.Models;
using YandexWeatherPlugin.Shared.Dto.Weather;

namespace YandexWeatherPlugin.Services;

public class YandexWeatherService
{
    const string CacheTag = "YandexWeatherPlugin";
    private readonly HybridCache _cache;
    private readonly FileHostingInfo _fileHostingInfo;
    private readonly HybridCacheEntryOptions _entryOptions = new()
    {
        LocalCacheExpiration = TimeSpan.FromHours(1),
        Expiration = TimeSpan.FromHours(2),
    };

    public GeoCodesDict GeoCodesDict { get; }

    public YandexWeatherService(HybridCache cache, IOptions<FileHostingInfo> fileHostingInfo)
    {
        GeoCodesDict = new();
        _cache = cache;
        _fileHostingInfo = fileHostingInfo.Value;
    }

    public void SiteDomainChanges()
    {
        _cache.RemoveByTagAsync(CacheTag);
    }

    string CacheKey(int geoCode, string language) => $"{CacheTag}:Weather-value-{geoCode}-{language}";

    public ValueTask<WeatherWidgetResponse> GetWeather(int geoCode, string language)
    {
        var key = CacheKey(geoCode, language);
        return _cache.GetOrCreateAsync<WeatherWidgetResponse>(key, async entry =>
        {
            var res = await GetWeatherFromApi(geoCode, language);
            var weatherWidget = new WeatherWidget(res, _fileHostingInfo.Backend.AbsoluteUri);
            return weatherWidget.ToResponse();
        },
        options: _entryOptions,
        tags: [CacheTag])!;
    }

    private async Task<YandexWeatherResponse> GetWeatherFromApi(int geoCode, string language = "ru", CancellationToken cancellationToken = default)
    {
        var json = await $"https://yandex.com/time/sync.json?geo={geoCode}&lang={language}".GetStringAsync(cancellationToken: cancellationToken);
        var cleanedJson = CleanJson(json);
        return JsonSerializer.Deserialize<YandexWeatherResponse>(cleanedJson)!;
    }

    public static string CleanJson(string dirtyJson)
    {
        // Удаляем isSensetiveRegion из объектов clocks
        var cleaned = System.Text.RegularExpressions.Regex.Replace(
            dirtyJson,
            @"\s*?\,\s*?""isSensetiveRegion""\s*:\s*(true|false)",
            "");
        return cleaned;
    }

}
