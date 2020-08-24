using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMonnitAPIs.Models.Interfaces
{
    public class SensorInfo
    {
        [JsonProperty(PropertyName = "sensorID")]
        public string SensorId { get; set; }

        [JsonProperty(PropertyName = "sensorName")]
        public string SensorName { get; set; }

        [JsonProperty(PropertyName = "applicationID")]
        public string ApplicationId { get; set; }

        [JsonProperty(PropertyName = "networkID")]
        public string NetworkId { get; set; }

        [JsonProperty(PropertyName = "dataMessageGUID")]
        public string DataMessageGUID { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "messageDate")]
        public string MessageDate { get; set; }

        [JsonProperty(PropertyName = "rawData")]
        public string RawData { get; set; }

        [JsonProperty(PropertyName = "dataType")]
        public string DataType { get; set; }

        [JsonProperty(PropertyName = "dataValue")]
        public string DataValue { get; set; }

        [JsonProperty(PropertyName = "plotLabels")]
        public string PlotLabels { get; set; }

        [JsonProperty(PropertyName = "plotValues")]
        public string PlotValues { get; set; }

        [JsonProperty(PropertyName = "batteryLevel")]
        public string BatteryLevel { get; set; }

        [JsonProperty(PropertyName = "signalStrength")]
        public string SignalStrength { get; set; }

        [JsonProperty(PropertyName = "pendingChange")]
        public string PendingChange { get; set; }

        [JsonProperty(PropertyName = "voltage")]
        public string Voltage { get; set; }
    }
}