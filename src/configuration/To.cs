using Newtonsoft.Json;

namespace configuration
{
    public class To
    {
        [JsonProperty("destination")] public string Destination { get; set; }
    }
}