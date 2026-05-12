using System.Reflection;

namespace YandexWeatherPlugin.Services;

public static class GeoCodeReader
{
    /// <summary>
    /// Считывает все строки из embedded resource в список
    /// </summary>
    /// <param name="resourcePath">Путь к ресурсу (например, "YandexWeatherPlugin.Services.geo-codes.txt")</param>
    /// <returns>Список строк из файла</returns>
    public static List<string> ReadLinesFromResource(string resourcePath)
    {
        var lines = new List<string>();
        var assembly = Assembly.GetExecutingAssembly();

        using (Stream stream = assembly.GetManifestResourceStream(resourcePath)!)
        {
            if (stream == null)
            {
                throw new ArgumentException($"Ресурс не найден: {resourcePath}. Проверьте путь и настройки embedded resource.");
            }

            using (StreamReader reader = new(stream))
            {
                string line;
                while ((line = reader.ReadLine()!) != null)
                {
                    lines.Add(line);
                }
            }
        }

        return lines;
    }

    /// <summary>
    /// Вариант для использования внутри того же assembly (YandexWeatherPlugin)
    /// </summary>
    public static List<string> ReadGeoLines()
    {
        // Полное имя ресурса: DefaultNamespace + папки + имя файла
        // Обычно это: YandexWeatherPlugin.Services.geo-codes.txt
        string resourceName = "YandexWeatherPlugin.Services.geo-codes.txt";
        return ReadLinesFromResource(resourceName);
    }
}
