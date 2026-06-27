using Mars.Nodes.Core;
using Mars.Nodes.Host.Shared;
using YandexWeatherPlugin.Front.Nodes;
using YandexWeatherPlugin.Services;

namespace YandexWeatherPlugin.FrontImplement;

public class YandexWeatherNodeImpl : INodeImplement<YandexWeatherNode>
{
    private readonly YandexWeatherService _weatherService;

    public YandexWeatherNode Node { get; }
    public IRuntimeNodeScope RNS { get; set; }
    Node INodeImplement.Node => Node;

    public YandexWeatherNodeImpl(YandexWeatherNode node, IRuntimeNodeScope rns, YandexWeatherService weatherService)
    {
        Node = node;
        RNS = rns;
        _weatherService = weatherService;
    }

    public async Task Execute(NodeMsg input, ExecuteAction callback, ExecutionParameters parameters)
    {
        var weather = await _weatherService.GetWeather(Node.GeoCode, Node.Language);

        var payload1 = input.Copy(weather);
        var payload2 = input.Copy(weather.Weather.Temp);

        callback(payload1, 0);
        callback(payload2, 1);

    }
}
