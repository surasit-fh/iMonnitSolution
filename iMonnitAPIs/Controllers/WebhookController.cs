using iMonnitAPIs.Models.Interfaces;
using iMonnitAPIs.Models.Responses;
using iMonnitAPIs.Process;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMonnitAPIs.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WebhookController : Controller
    {
        [HttpPost("Attempts")]
        public WebHookControlResponse Attempts([FromBody]AttemptInfo attemptInfo)
        {
            WebHookProcess webHookProcess = new WebHookProcess();
            WebHookControlResponse response = webHookProcess.AttemptsProcess(attemptInfo);
            return response;
        }
    }
}