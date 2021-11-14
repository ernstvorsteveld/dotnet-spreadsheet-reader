using Newtonsoft.Json;

namespace configuration
{
    public class From
    {
        [JsonProperty("from_column")] public string Column { get; set; }
        [JsonProperty("from_type")] public string Type { get; set; }
        [JsonProperty("from_format")] public string Format { get; set; }
        [JsonProperty("from_pattern")] public string Pattern { get; set; }
        [JsonProperty("from_regex")] public string RegEx { get; set; }
        [JsonProperty("from_key")] public string Key { get; set; }
    }
}