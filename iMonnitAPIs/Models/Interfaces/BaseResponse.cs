using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMonnitAPIs.Models.Interfaces
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public int ErrorCode { get; set; }
        public string Description { get; set; }
        public string ErrorMessage { get; set; }
    }
}