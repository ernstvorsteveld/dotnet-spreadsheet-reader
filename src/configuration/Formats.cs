using Newtonsoft.Json;

namespace configuration
{
    public class Formats
    {
        [JsonProperty("date_format")] public string DateFormat { get; set; }
    }
}