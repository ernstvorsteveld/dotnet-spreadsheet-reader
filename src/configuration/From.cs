using Newtonsoft.Json;

namespace configuration
{
    public class From
    {
        [JsonProperty("column")] public string Column { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
    }
}