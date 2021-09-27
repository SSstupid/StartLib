using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StartLib.Tools
{
    public enum LogType { Daily, Monthly }

    public class LogManager
    {
        private string _path;

        #region Constructors 
        public LogManager(string path, LogType logType, string prefix, string postfix)
        {
            _path = path;
            _SetLogPath(logType, prefix, postfix);
        }
        public LogManager(string prefix, string postfix)
            : this(Path.Combine(Application.Root, "Log"), LogType.Daily, prefix, postfix)
        { 
        }

        public LogManager()
            : this(Path.Combine(Application.Root, "Log"), LogType.Daily, null, null)
        {
        }
        #endregion

        #region Methods
        private void _SetLogPath(LogType logType, string prefix, string postfix)
        {
            string path = String.Empty;
            string name = String.Empty;

            switch (logType)
            {
                case LogType.Daily:
                    path = String.Format(@"{0}\{1}\", DateTime.Now.Year, DateTime.Now.ToString("MM"));
                    name = DateTime.Now.ToString("yyyyMMdd");
                    break;
                case LogType.Monthly:
                    path = String.Format(@"{0}\", DateTime.Now.Year);
                    name = DateTime.Now.ToString("yyyyMM");
                    break;
            }

            _path = Path.Combine(_path, path);
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            if (!string.IsNullOrEmpty(prefix))
                name = prefix + name;
            if (!string.IsNullOrEmpty(postfix))
                name = name + postfix ;
            name += ".txt";

            _path = Path.Combine(_path, name);
            //string LogFile = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            //_path = Path.Combine(_path, LogFile);
        }


        public void Write(string data)
        {
            try 
            {
                using (StreamWriter writer = new StreamWriter(_path, true))
                {
                    writer.Write(data);
                }
            }
            catch (Exception ex)
            { }
        }
        public void WriteLine(string data)
        {
            try 
            {
                using (StreamWriter writer = new StreamWriter(_path, true))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss\t") + data);
            }
            }
            catch (Exception ex)
            { }
        }
        #endregion
    }

}
