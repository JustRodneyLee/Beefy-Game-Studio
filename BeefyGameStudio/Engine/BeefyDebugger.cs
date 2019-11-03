using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeefyEngine
{
    public static class BeefyDebugger
    {
        readonly static List<string> InternalLogs = new List<string>();
        readonly static List<string> GameLogs = new List<string>();
        readonly static bool DeveloperMode = true;
        static int ptr = 0;
        static int xptr = 0;

        public static void LogInternal(string log)
        {
            InternalLogs.Add(log);
        }

        public static void LogGame(string log)
        {
            GameLogs.Add(log);
        }

        public static void Update()
        {
            if (DeveloperMode)
            {
                for (int i = ptr; i<InternalLogs.Count; i++)
                {
                    Console.WriteLine(InternalLogs[i]);
                    xptr++;
                }
                ptr = xptr;
            }
        }       
    }
}
