using System.Collections.Generic;
using Newtonsoft.Json;

namespace configuration
{
    public class Configuration
    {
        [JsonProperty("header")] public Header Header { get; set; }
        [JsonProperty("formats")] public Formats Formats { get; set; }
        [JsonProperty("mappings")] public List<Mapping> Mappings { get; set; }

        public Mapping Get(int index)
        {
            return Mappings[index];
        }
    }
}