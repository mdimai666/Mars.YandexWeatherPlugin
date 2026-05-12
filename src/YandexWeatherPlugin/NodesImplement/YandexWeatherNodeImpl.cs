using Mars.Nodes.Core;
using Mars.Nodes.Core.Implements;
using Microsoft.Extensions.Logging;
using YandexWeatherPlugin.Dto;
using YandexWeatherPlugin.Front.Nodes;
using YandexWeatherPlugin.Services;

namespace YandexWeatherPlugin.FrontImplement;

public class YandexWeatherNodeImpl : INodeImplement<YandexWeatherNode>, INodeImplement
{
    private readonly ILogger<YandexWeatherNodeImpl> _logger;
    private readonly YandexWeatherService _weatherService;

    public YandexWeatherNode Node { get; }
    public IRED RED { get; set; }
    Node INodeImplement<Node>.Node => Node;

    public YandexWeatherNodeImpl(YandexWeatherNode node, IRED red, ILogger<YandexWeatherNodeImpl> logger, YandexWeatherService weatherService)
    {
        Node = node;
        RED = red;
        _logger = logger;
        _weatherService = weatherService;
    }

    public async Task Execute(NodeMsg input, ExecuteAction callback, ExecutionParameters parameters)
    {
        _logger.LogTrace("Request weather");

        var weather = (await _weatherService.GetWeather(Node.GeoCode, Node.Language)).ToResponse();

        var payload1 = input.Copy(weather);
        var payload2 = input.Copy(weather.Weather.Temp);

        callback(payload1, 0);
        callback(payload2, 1);

    }
}
