using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMonnitAPIs.Models.Interfaces
{
    public class AttemptInfo
    {
        [JsonProperty(PropertyName = "gatewayMessage")]
        public GatewayInfo GatewayInfo { get; set; }

        [JsonProperty(PropertyName = "sensorMessages")]
        public List<SensorInfo> SensorInfos { get; set; }
    }
}