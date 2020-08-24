using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace iMonnitAPIs.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ErrorCode : int
    {
        [Description("Success")]
        Success = 20000,

        [Description("BadRequest")]
        BadRequest = 40000,

        [Description("Unauthorized")]
        Unauthorized = 40100,

        [Description("Forbidden")]
        Forbidden = 40300,

        [Description("NotFound")]
        NotFound = 40400,

        [Description("InternalServerError")]
        InternalServerError = 50000
    }
}