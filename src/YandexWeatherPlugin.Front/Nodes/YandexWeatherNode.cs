using System.ComponentModel.DataAnnotations;
using Mars.Core.Attributes;
using Mars.Nodes.Core;
using Mars.Nodes.Core.Fields;

namespace YandexWeatherPlugin.Front.Nodes;

[FunctionApiDocument("./_plugin/YandexWeatherPlugin/nodes/docs/YandexWeatherNode/YandexWeatherNode{.lang}.md")]
[Display(GroupName = "other")]
public class YandexWeatherNode : Node
{
    public int GeoCode { get; set; } = 213;

    [Display(Description = "[ru, en, ...] two letter code")]
    public string Language { get; set; } = "ru";

    public YandexWeatherNode()
    {
        Inputs = [new()];
        Color = "#f2d35c";
        Outputs = [new (){ Label = "Weather object" }, new() { Label = "temperature in Celsius" } ];
        Icon = "/_plugin/YandexWeatherPlugin/icon.png";
    }
}
