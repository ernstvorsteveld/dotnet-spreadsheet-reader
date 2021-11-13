using Newtonsoft.Json;

namespace configuration
{
    public class To
    {
        [JsonProperty("to_destination")] public string Destination { get; set; }
        [JsonProperty("to_type")] public string Type { get; set; }
    }
}