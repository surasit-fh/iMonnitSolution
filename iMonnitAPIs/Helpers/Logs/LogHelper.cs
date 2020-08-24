using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMonnitAPIs.Helpers.Logs
{
    public class LogHelper
    {
        private TraceLogWriter _TraceLogWriter;
        private CultureInfo _ci = new CultureInfo("en-US");
        private string _LogPath;

        public LogHelper(string logPath)
        {
            int lenght = AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin");
            _LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, lenght), logPath);
            Init();
        }

        public void Init()
        {
            _TraceLogWriter = new TraceLogWriter(_LogPath, "", "", "", "log", _ci);
            _TraceLogWriter.SubfolderType = TraceLogWriter.FolderOption.Date;
            _TraceLogWriter.TransId = DateTime.Now.ToString("yyyyMMdd", _ci);
        }

        public void WriteTraceLog(string line)
        {
            if (_TraceLogWriter != null)
            {
                lock (_TraceLogWriter)
                {
                    _TraceLogWriter.WriteLine(line);
                }
            }
        }

        public string CreateErrorMessage(string errorCode, Exception ex)
        {
            StringBuilder message = new StringBuilder();

            message.Append("Exception\r\n");
            message.Append("=======================\r\n");
            message.Append("Message: ");

            if (ex.Message != null)
                message.Append(errorCode + " : " + ex.Message);

            message.Append("\r\n");
            message.Append("DateTime: ");
            message.Append(DateTime.Now.ToString());

            message.Append("\r\n");
            message.Append("Source: ");

            if (ex.Source != null)
                message.Append(ex.Source);

            message.Append("\r\n");
            message.Append("TargetSite: ");

            if (ex.TargetSite != null)
                message.Append(ex.TargetSite.ToString());

            message.Append("\r\n");
            message.Append("Type: ");

            if (ex.GetType() != null)
                message.Append(ex.GetType().ToString());

            message.Append("\r\n");
            message.Append("StackTrace: ");

            if (ex.StackTrace != null)
                message.Append(ex.StackTrace);

            message.Append("\r\n");
            message.Append("InnerException: ");

            if (ex.InnerException != null)
                message.Append((ex.InnerException).ToString());

            message.Append("\r\n");
            message.Append("HelpLink: ");

            if (ex.HelpLink != null)
                message.Append(ex.HelpLink);

            message.Append("\r\n");
            message.Append("=======================");
            message.Append("\r\n");

            return message.ToString();
        }
    }
}