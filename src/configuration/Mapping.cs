using Newtonsoft.Json;

namespace configuration
{
    public class Mapping
    {
        [JsonProperty("formats")] public Formats Formats { get; set; }
        [JsonProperty("index")] public int Index { get; set; }
        [JsonProperty("from")] public From From { get; set; }
        [JsonProperty("to")] public To To { get; set; }
    }
}