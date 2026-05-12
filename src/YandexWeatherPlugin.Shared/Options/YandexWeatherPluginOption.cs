using System.ComponentModel.DataAnnotations;

namespace YandexWeatherPlugin.Shared.Options;

[Display(Name = "Настройки YandexWeather плагина")]
public class YandexWeatherPluginOption
{
    [Required]
    [Display(Name = "Option value", Description = "some value description")]
    public string OptionValue { get; set; } = "default1";
}
