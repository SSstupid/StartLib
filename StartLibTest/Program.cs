using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartLib.Tools;
using StartLib.Extensions;

namespace StartLibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string contents = "Hello there, <br />This is Ryan.";

            //Function 사용
            EmailManager email = new EmailManager("smtp.com", 25, "id", "password");
            email.From = "sender@test.com";
            email.To.Add("receiver@test.com");
            email.subject = "Subject";
            email.Body = contents;
            email.Send();

            //email.To.Clear(); 받는 사람 제거

            //함수 사용
            EmailManager.Send("receiver@test.com", "Hi...", contents);  // from이 없는 경우 defalut value

            EmailManager.Send("from@test.com", "receiver@test.com", "Hi...", contents);     //cc,bcc 제외

            EmailManager.Send("from@test.com", "receiver@test.com", "Hi...", contents, "cc@test.com", "bcc@test.com");      //cc,bcc 포함

            /* // DateTimeExtensions
            DateTime DateTest = DateTime.Now;

            string temp1 = "abc";
            string temp2 = "123";
            string temp3 = "08/26/1994 10:10";

            Console.WriteLine("FirstDateOfMonth : " + DateTest.FirstDateOfMonth());
            Console.WriteLine("LastDateOrMonth : " + DateTest.LastDateOrMonth());

            Console.WriteLine("IsNumeric : " + temp1.IsNumeric());
            Console.WriteLine("IsDateTime : " + temp1.IsDateTime());

            Console.WriteLine("IsNumeric : " + temp2.IsNumeric());
            Console.WriteLine("IsDateTime : " + temp2.IsDateTime());

            Console.WriteLine("IsNumeric : " + temp3.IsNumeric());
            Console.WriteLine("IsDateTime : " + temp3.IsDateTime());
            */

            //LogManager Test
            /*
            //LogManager log = new LogManager("Start_", "_Finish");
            //log.Write("[Begin Processing]-----");
            LogManager log = new LogManager();
            log.WriteConsole("test");
            
            for (int index = 0; index < 10; index++)
            {
                log.WriteLine("Processing: " + index);

                System.Threading.Thread.Sleep(500);

                log.WriteLine("Done: " + index);
            }

            log.WriteLine("[End Processing]-----");
            //Application ex)
            //Console.WriteLine(Application.Root);
           // Console.ReadLine();
            */

        }
    }
    public static class ExtensionText // 확장매서드 static으로 선언해야함
    {
        public static void WriteConsole(this LogManager log, string data)
        {
            log.Write(data);
            Console.Write(data);
        }
    }
}
