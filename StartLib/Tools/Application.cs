using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartLib.Tools
{
    public static class Application
    {
        public static string Root
        {
            get 
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
    }
}
