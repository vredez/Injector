using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NetIntruder
{
    public class Intruder
    {
        public static int Intrude(string args)
        {
            MessageBox.Show(args);
            ThreadPool.QueueUserWorkItem(Run, args);
            return 0;
        }

        static void Run(object args)
        {
            
        }
    }
}
