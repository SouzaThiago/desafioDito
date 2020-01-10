using Newtonsoft.Json;

namespace DesafioDito.Adapter.Model
{
    public class CustomData
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
