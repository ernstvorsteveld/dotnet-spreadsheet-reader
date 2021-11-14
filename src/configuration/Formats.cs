using Newtonsoft.Json;

namespace configuration
{
    public class Formats
    {
        [JsonProperty("date_format")] public string DateFormat { get; set; }
        [JsonProperty("true_formats")] public string[] BooleanTrueFormats { get; set; }
    }
}