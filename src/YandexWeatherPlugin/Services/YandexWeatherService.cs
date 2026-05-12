using System.Text.Json;
using Flurl.Http;
using Mars.Host.Shared.Dto.Files;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using YandexWeatherPlugin.Dto;
using YandexWeatherPlugin.Models;

namespace YandexWeatherPlugin.Services;

public class YandexWeatherService
{
    private readonly IMemoryCache _memoryCache;
    private readonly FileHostingInfo _fileHostingInfo;
    private readonly TimeSpan _cacheTtl = TimeSpan.FromHours(1);

    public GeoCodesDict GeoCodesDict { get; }

    public YandexWeatherService(IMemoryCache memoryCache, IOptions<FileHostingInfo> fileHostingInfo)
    {
        GeoCodesDict = new();
        _memoryCache = memoryCache;
        _fileHostingInfo = fileHostingInfo.Value;
    }

    public Task<WeatherWidget> GetWeather(int geoCode, string language)
    {
        var key = $"YandexWeather-value-{geoCode}-{language}";

        return _memoryCache.GetOrCreateAsync<WeatherWidget>(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheTtl;
            var res = await GetWeatherFromApi(geoCode, language);

            return new WeatherWidget(res, _fileHostingInfo.Backend.AbsoluteUri);
        })!;
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
