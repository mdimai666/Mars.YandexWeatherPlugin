using System.ComponentModel;
using System.Net.Mime;
using Mars.Host.Shared.ExceptionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YandexWeatherPlugin.Dto;
using YandexWeatherPlugin.Services;
using YandexWeatherPlugin.Shared.Dto.Weather;

namespace YandexWeatherPlugin.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[Produces(MediaTypeNames.Application.Json)]
[UserActionResultExceptionFilter]
[NotFoundExceptionFilter]
[FluentValidationExceptionFilter]
[AllExceptionCatchToUserActionResultFilter]
public class YandexWeatherPluginController : ControllerBase
{
    private readonly YandexWeatherService _service;

    public YandexWeatherPluginController(YandexWeatherService service)
    {
        _service = service;
    }

    [HttpGet("GetWeather")]
    public async Task<WeatherWidgetResponse> GetWeather(int geoCode = 213, [DefaultValue("ru")] string language = "ru")
    {
        return (await _service.GetWeather(geoCode, language)).ToResponse();
    }

    [HttpGet("GeoCodesList")]
    public IReadOnlyDictionary<int, string> GeoCodesList()
    {
        return _service.GeoCodesDict.GeoCodes;
    }

}
