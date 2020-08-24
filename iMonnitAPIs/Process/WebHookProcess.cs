using iMonnitAPIs.Helpers.Logs;
using iMonnitAPIs.Models.Enums;
using iMonnitAPIs.Models.Interfaces;
using iMonnitAPIs.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMonnitAPIs.Process
{
    public class WebHookProcess
    {
        CultureInfo _ci = new CultureInfo("en-US", false);

        public WebHookControlResponse AttemptsProcess(AttemptInfo attemptInfo)
        {
            LogHelper log = new LogHelper("iMonnitLogs/Trace/Incoming/");
            StringBuilder sb = new StringBuilder();

            try
            {
                DateTime startTime = DateTime.Now;
                DateTime endTime = new DateTime();
                sb.AppendLine("Attempts at " + startTime.ToString(@"dd-MM-yyyy HH:mm:ss.fffffff", _ci));

                if (attemptInfo != null)
                {
                    sb.AppendLine("Param from Client request : {\"attemptInfo\":" + JsonConvert.SerializeObject(attemptInfo) + "}");
                    endTime = DateTime.Now;
                    sb.AppendLine("Response result IsSucess : true, ErrorCode : 20000, Description : Success");
                    sb.AppendLine("Finish to Attempts at " + endTime.ToString(@"dd-MM-yyyy HH:mm:ss.fffffff", _ci));
                    sb.AppendLine("Process time : " + GetDurationTime(startTime, endTime).ToString(@"dd\.hh\:mm\:ss\.fffffff", _ci));
                    sb.AppendLine("----------------------------------------------------------------------");
                    log.WriteTraceLog(sb.ToString());
                    return MappingWebHookControlSuccessResponse();
                }
                else
                {
                    sb.AppendLine("Param from Client request : {\"attemptInfo\": null}");
                    endTime = DateTime.Now;
                    sb.AppendLine("Response result IsSucess : false, ErrorCode : 40000, Description : AttemptInfo is null");
                    sb.AppendLine("Finish to Attempts at " + endTime.ToString(@"dd-MM-yyyy HH:mm:ss.fffffff", _ci));
                    sb.AppendLine("Process time : " + GetDurationTime(startTime, endTime).ToString(@"dd\.hh\:mm\:ss\.fffffff", _ci));
                    sb.AppendLine("----------------------------------------------------------------------");
                    log.WriteTraceLog(sb.ToString());
                    return MappingWebHookControlErrorResponse(ErrorCode.BadRequest, "AttemptInfo is null");
                }
            }
            catch (Exception ex)
            {
                log.WriteTraceLog(log.CreateErrorMessage(string.Format(@"{0}, Response result IsSucess : false, ErrorCode : {1}, Description : {2}", sb.ToString() + "\r\n", (int)ErrorCode.InternalServerError, ErrorCode.InternalServerError), ex));
                return MappingWebHookControlExceptionResponse(ex);
            }
            finally
            {
                sb.Clear();
                log = null;
            }
        }

        private TimeSpan GetDurationTime(DateTime startTime, DateTime endTime)
        {
            TimeSpan _startTime = new TimeSpan(Convert.ToDateTime(startTime).Day, Convert.ToDateTime(startTime).Hour, Convert.ToDateTime(startTime).Minute, Convert.ToDateTime(startTime).Second, Convert.ToDateTime(startTime).Millisecond);
            TimeSpan _endTime = new TimeSpan(Convert.ToDateTime(endTime).Day, Convert.ToDateTime(endTime).Hour, Convert.ToDateTime(endTime).Minute, Convert.ToDateTime(endTime).Second, Convert.ToDateTime(endTime).Millisecond);
            TimeSpan durationTime = _endTime.Subtract(_startTime);
            return durationTime;
        }

        private WebHookControlResponse MappingWebHookControlSuccessResponse()
        {
            return new WebHookControlResponse() { IsSuccess = true, ErrorCode = (int)ErrorCode.Success, Description = ErrorCode.Success.ToString() };
        }

        private WebHookControlResponse MappingWebHookControlErrorResponse(ErrorCode errorCode, string description)
        {
            return new WebHookControlResponse() { IsSuccess = false, ErrorCode = (int)errorCode, Description = description };
        }

        private WebHookControlResponse MappingWebHookControlExceptionResponse(Exception ex)
        {
            return new WebHookControlResponse() { IsSuccess = false, ErrorCode = (int)ErrorCode.InternalServerError, Description = ErrorCode.InternalServerError.ToString(), ErrorMessage = ex.Message };
        }
    }
}