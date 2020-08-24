using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMonnitAPIs.Models.Interfaces
{
    public class GatewayInfo
    {
        [JsonProperty(PropertyName = "gatewayID")]
        public string GatewayId { get; set; }

        [JsonProperty(PropertyName = "gatewayName")]
        public string GatewayName { get; set; }

        [JsonProperty(PropertyName = "accountID")]
        public string AccountId { get; set; }

        [JsonProperty(PropertyName = "networkID")]
        public string NetworkId { get; set; }

        [JsonProperty(PropertyName = "messageType")]
        public string MessageType { get; set; }

        [JsonProperty(PropertyName = "power")]
        public string Power { get; set; }

        [JsonProperty(PropertyName = "batteryLevel")]
        public string BatteryLevel { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
        
        [JsonProperty(PropertyName = "count")]
        public string Count { get; set; }

        [JsonProperty(PropertyName = "signalStrength")]
        public string SignalStrength { get; set; }

        [JsonProperty(PropertyName = "pendingChange")]
        public string PendingChange { get; set; }
    }
}