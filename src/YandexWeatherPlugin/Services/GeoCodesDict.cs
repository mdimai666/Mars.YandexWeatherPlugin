namespace YandexWeatherPlugin.Services;

public class GeoCodesDict
{
    public Dictionary<int, string> GeoCodes;

    public GeoCodesDict()
    {
        GeoCodes = GeoCodeReader.ReadGeoLines().Select(s =>
        {
            var bracerIndex = s.LastIndexOf('(');
            var name = s.Substring(0, bracerIndex).TrimStart().TrimStart('-').Trim();
            var code = int.Parse(s.Substring(bracerIndex + 1).TrimEnd(')').Trim());
            return new KeyValuePair<int, string>(code, name);
        }).OrderBy(s => s.Value).ToDictionary();
    }

    public static Dictionary<string, string> IconDescription = new() {
        {"bkn_-ra_d", "облачно с прояснениями, небольшой дождь(день)" },
        {"bkn_-ra_n", "облачно с прояснениями, небольшой дождь(ночь)"},
        {"bkn_-sn_d", "облачно с прояснениями, небольшой снег(день)"},
        {"bkn_-sn_n", "облачно с прояснениями, небольшой снег(ночь)"},
        {"bkn_d", "переменная облачность(день)"},
        {"bkn_n", "переменная облачность(ночь)"},
        {"bkn_ra_d", "переменная облачность, дождь(день)"},
        {"bkn_ra_n", "переменная облачность, дождь(ночь)"},
        {"bkn_sn_d", "переменная облачность, снег(день)"},
        {"bkn_sn_n", "переменная облачность, снег(ночь)"},
        {"bl", "метель"},
        {"fg_d", "туман"},
        {"ovc", "облачно"},
        {"ovc_-ra", "облачно, временами дождь"},
        {"ovc_f-sn", "облачно, временами снег"},
        {"ovc_ra", "облачно, дождь"},
        {"ovc_sn", "облачно, снег"},
        {"ovc_ts_ra", "облачно, дождь, гроза"},
        {"skc_d", "ясно(день)"},
        {"skc_n", "ясно(ночь)"},
        {"ovc_ra_sn.svg","облачно, дождь, снег" }
    };
}
