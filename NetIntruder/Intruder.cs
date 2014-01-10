using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetIntruder
{
    public class Intruder
    {
        public static int Intrude(string args)
        {
            ThreadPool.QueueUserWorkItem(Run, args);
            return 0;
        }

        static void Run(object args)
        {

        }
    }
}
