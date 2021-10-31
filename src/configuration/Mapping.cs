using Newtonsoft.Json;

namespace configuration
{
    public class Mapping
    {
        [JsonProperty("from")] public From From { get; set; }
        [JsonProperty("tro")] public To to { get; set; }
    }
}