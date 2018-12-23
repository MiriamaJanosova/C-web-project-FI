using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new Logger();
            log.Log(Logger.LoggedType.Exception, "kokotina", new NotImplementedException());
            log.Stop();
        }
    }
}
