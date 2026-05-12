using System.Text.Json;
using FluentAssertions;
using YandexWeatherPlugin.Dto;
using YandexWeatherPlugin.Services;

namespace Test.YandexWeatherPlugin;

public class GeoCodesDictTests
{
    [Fact]
    public async Task ReadGeoCodes_ShouldSuccess()
    {
        //Arrange
        //Act
        var action = () => new GeoCodesDict();

        //Assert
        var result = action.Should().NotThrow();
        result.Subject.GeoCodes.Should().HaveCountGreaterThan(0);
    }

    public const string CityJsonExample = """
        {
          "time": 1778464977658,
          "clocks": {
            "74": {
              "id": 74,
              "name": "Yakutsk",
              "offset": 32400000,
              "offsetString": "UTC+9:00",
              "showSunriseSunset": true,
              "sunrise": "03:39",
              "sunset": "20:56",
              "isNight": false,
              "skyColor": "#68bfff",
              "weather": {
                "temp": 2,
                "icon": "bkn-d",
                "link": "https://yandex.ru/pogoda/74?lat=62.02722&lon=129.732181"
              },
              "parents": [
                {
                  "id": 11443,
                  "name": "Sakha (Yakutia) Republic"
                },
                {
                  "id": 225,
                  "name": "Russia"
                }
              ]
            },
            "isSensetiveRegion": false
          }
        }
        """;

    public const string MoscowRegionObjectExample = """
        {
        "time":1778481699907,
        "clocks":{
            "1":{
                "id":1,
                "name":"Москва и Московская область",
                "offset":10800000,
                "offsetString":"UTC+3:00",
                "showSunriseSunset":false,
                "sunrise":"06:00",
                "sunset":"21:00",
                "isNight":false,
                "skyColor":"#97cbff",
                "weather":{},
                "parents":[
                    {
                        "id":225,
                        "name":"Россия"
                    }
                ]},
                "isSensetiveRegion":false
            }
        }
        """;

    [Theory]
    [InlineData([CityJsonExample])]
    [InlineData([MoscowRegionObjectExample])]
    public async Task ParseYandexResponse_MixedAssocArrayObject_ShouldBeDeserializeToValidObj(string json)
    {
        //Arrange
        var yandexResponse = YandexWeatherService.CleanJson(json);

        //Act
        var action = () => JsonSerializer.Deserialize<YandexWeatherResponse>((yandexResponse))!;

        //Assert
        var result = action.Should().NotThrow();
        result.Subject.Clocks.Values.First().Id.Should().BeGreaterThan(0);

    }
}
