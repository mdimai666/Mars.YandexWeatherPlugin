using FluentAssertions;
using YandexWeatherPlugin.Dto;
using YandexWeatherPlugin.Models;

namespace Test.YandexWeatherPlugin;

public class WeatherWidgetResponseTests
{
    [Fact]
    public async Task ToResponse_SavedTimeReturnActualValue_ShouldReturnActualNotWritted()
    {
        //Arrange
        var currentTime = DateTimeOffset.UtcNow;
        var unixMilliseconds = currentTime.ToUnixTimeMilliseconds();
        var time = new LocationTimeData(new Clock { }, unixMilliseconds);
        await Task.Delay(1000);

        //Act
        var response = time.ToResponse();

        //Assert
        response.TimeString.Should().Be(DateTimeOffset.UtcNow.ToString("HH:mm:ss"));
        response.TimeUnixTimeMillis.Should().BeInRange(unixMilliseconds + 1000, unixMilliseconds + 1100);
    }
}
