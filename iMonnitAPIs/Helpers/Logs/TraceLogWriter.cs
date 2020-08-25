using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMonnitAPIs.Helpers.Logs
{
    public class TraceLogWriter
    {
        CultureInfo _ci = new CultureInfo("en-US", false);
        private string _logPath;
        private string _prefix = "";
        private string _format = "yyyy-MM-dd";
        private string _suffix = "";
        private string _extension = "txt";
        private CultureInfo _culture = CultureInfo.CurrentCulture;
        private Encoding _enc;
        private string _msg;
        private FolderOption _subfolderType = FolderOption.None;
        private string _transId;

        public enum FolderOption { None, Year, Month, Date, Hour, Quater };

        public FolderOption SubfolderType
        {
            get
            {
                return _subfolderType;
            }
            set
            {
                _subfolderType = value;
            }
        }

        public string TransId
        {
            get
            {
                return _transId;
            }
            set
            {
                _transId = value;
            }
        }

        public TraceLogWriter(string logPath, string prefix, string format, string suffix, string extension, CultureInfo culture)
        {
            try
            {
                Init(logPath, prefix, format, suffix, extension, culture);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Init(string logPath, string prefix, string format, string suffix, string extension, CultureInfo culture)
        {
            _logPath = logPath;
            _prefix = prefix;
            _format = format;
            _suffix = suffix;
            _extension = extension;
            _culture = culture;
            _enc = Encoding.UTF8;
        }

        public void WriteLine(string message)
        {
            WriteLine(_transId, message);
        }

        public void WriteLine(string transId, string message)
        {
            _msg = message + "\r\n";
            _transId = transId;
            DateTime dt = DateTime.Now;
            string dirPath = GetDirectoryPath(_logPath, dt, _subfolderType);
            CreateDirectory(dirPath);
            string fileName = GetFileName(transId);
            string ymd = DateTime.Now.ToString("yyyyMMddHH", _ci);
            int min = DateTime.Now.Minute;

            if (min <= 15)
                fileName = string.Format("{0}{1}.{2}", ymd, "00", _extension);
            else if (min > 15 && min <= 30)
                fileName = string.Format("{0}{1}.{2}", ymd, "15", _extension);
            else if (min > 30 && min <= 45)
                fileName = string.Format("{0}{1}.{2}", ymd, "30", _extension);
            else if (min > 45)
                fileName = string.Format("{0}{1}.{2}", ymd, "45", _extension);
            else
                fileName = string.Format("{0}{1}", ymd, "00");

            string filePath = dirPath + fileName;
            StreamWriter ssw = null;
            TextWriter sw = null;

            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 500, false);
                ssw = new StreamWriter(fs, new System.Text.UTF8Encoding(false));
                ssw.AutoFlush = true;
                sw = TextWriter.Synchronized(ssw);
                sw.Write(_msg);
                sw.Flush();
            }
            catch (Exception)
            {
                try
                {
                    filePath = dirPath + "Exception_" + fileName;
                    FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 500, false);
                    ssw = new StreamWriter(fs, new System.Text.UTF8Encoding(false));
                    ssw.AutoFlush = true;
                    sw = TextWriter.Synchronized(ssw);
                    sw.Write(_msg);
                    sw.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            finally
            {
                if (ssw != null)
                    ssw.Close();

                if (sw != null)
                    sw.Close();
            }
        }

        private string GetDirectoryPath(string logPath, DateTime now, FolderOption fo)
        {
            CultureInfo ci = new CultureInfo("en-US", false);
            string y = now.ToString("yyyy", ci);
            string ym = now.ToString("yyyyMM", ci);
            string ymd = now.ToString("yyyyMMdd", ci);
            string ymdh = now.ToString("HHmm", ci);
            int min = now.Minute;
            string dirPath;

            if (fo == FolderOption.Year)
                dirPath = string.Format(@"{0}/{1}/", logPath, y);
            else if (fo == FolderOption.Month)
                dirPath = string.Format(@"{0}/{1}/{2}/", logPath, y, ym);
            else if (fo == FolderOption.Date)
                dirPath = string.Format(@"{0}/{1}/{2}/{3}/", logPath, y, ym, ymd);
            else if (fo == FolderOption.Hour)
                dirPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", logPath, y, ym, ymd, ymdh.Substring(0, 3) + "0");
            else if (fo == FolderOption.Quater)
            {
                if (min <= 15)
                    dirPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", logPath, y, ym, ymd, "00");
                else if (min > 15 && min <= 30)
                    dirPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", logPath, y, ym, ymd, "15");
                else if (min > 30 && min <= 45)
                    dirPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", logPath, y, ym, ymd, "30");
                else if (min > 45)
                    dirPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", logPath, y, ym, ymd, "45");
                else
                    dirPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", logPath, y, ym, ymd, "00");
            }
            else
                dirPath = logPath + @"/";

            return dirPath;
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to create a directory for logging at " + path, ex);
                }
            }
        }

        public string GetFileName(string transId)
        {
            string fileName = _prefix + transId + _suffix + "." + _extension;
            return fileName;
        }
    }
}