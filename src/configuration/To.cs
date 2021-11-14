using Newtonsoft.Json;

namespace configuration
{
    public class To
    {
        [JsonProperty("to_destination")] public string Destination { get; set; }
    }
}