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
        // prefix 날짜앞에 붙이는 글 postfix 뒤에 붙이는 말
        public LogManager(string prefix, string postfix)
            : this(Path.Combine(Application.Root, "Log"), LogType.Daily, prefix, postfix)
        { 
        }

        public LogManager()
            : this(Path.Combine(Application.Root, "Log"), LogType.Daily, null, null)
        {
            //경로가 없는 경우 실행자 경로그대로 Log생성후 저장
        }
        //생성자 2개인 이유 => 옵션을 둘려고 (특이사항)
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
            try //로그를 찍는 부분에서 인셉션??이 발생하면 무시(로그로인한 버그발생 방지)
            {
                using (StreamWriter writer = new StreamWriter(_path, true)) //true ==> not override 단지 이어서 기록함
                {
                    writer.Write(data);
                }
            }
            catch (Exception ex)
            { }
        }
        //파일이 없는 경우 생성 
        //usning => {}안에서 동작하고 끝나면 자동으로 닫아줌(무한대기 방지??)
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
    //무궁무진하지만 보통 시간체크하는데 쓰임
}
