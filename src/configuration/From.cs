using Newtonsoft.Json;

namespace configuration
{
    public class From
    {
        [JsonProperty("column")] public string Column { get; set; }
        [JsonProperty("from_type")] public string Type { get; set; }
        [JsonProperty("key")] public string Key { get; set; }
    }
}