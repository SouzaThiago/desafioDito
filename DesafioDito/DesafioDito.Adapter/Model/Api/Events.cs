using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DesafioDito.Adapter.Model.Api
{
    public class Events
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty(PropertyName = "revenue", Required = Required.Default)]
        public int? Revenue { get; set; }
        
        [JsonProperty("custom_data")]
        public List<CustomData> CustomData { get; set; }
    }
}
