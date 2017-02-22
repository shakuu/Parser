using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var wh = new AutoResetEvent(false);
            var fsw = new FileSystemWatcher(".");
            fsw.Filter = @"C:\Users\colley\OneDrive\swtor-parser\sample-logs\CombatLogs\combat_2017-02-21_21_05_53_596881.txt";
            fsw.EnableRaisingEvents = true;
            fsw.Changed += (s, e) => wh.Set();

            var fs = new FileStream(@"C:\Users\colley\OneDrive\swtor-parser\sample-logs\CombatLogs\combat_2017-02-21_21_05_53_596881.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                var s = "";
                while (true)
                {
                    s = sr.ReadLine();
                    if (s != null)
                        Console.WriteLine(s);
                    else
                        wh.WaitOne(1000);
                }
            }

            wh.Close();
        }
    }
}
