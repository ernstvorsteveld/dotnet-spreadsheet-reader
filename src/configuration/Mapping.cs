using Newtonsoft.Json;

namespace configuration
{
    public class Mapping
    {
        [JsonProperty("from")] public From From { get; set; }
        [JsonProperty("to")] public To To { get; set; }
    }
}