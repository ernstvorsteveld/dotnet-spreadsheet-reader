using System;
using Newtonsoft.Json;

namespace configuration
{
    public class Header
    {
        [JsonProperty("read")] public bool Read { get; set; }
        [JsonProperty("skip_before")] public int SkipBefore { get; set; }
        [JsonProperty("skip_after")] public int SkipAfter { get; set; }
    }
}