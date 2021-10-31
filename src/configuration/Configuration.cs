using System.Collections.Generic;
using Newtonsoft.Json;

namespace configuration
{
    public class Configuration
    {
        [JsonProperty("mappings")] public List<Mapping> Mappings { get; set; }
    }
}