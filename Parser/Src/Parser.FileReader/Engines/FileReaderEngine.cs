using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Parser.FileReader.Contracts;

namespace Parser.FileReader.Engines
{
    public class FileReaderEngine : IFileReaderEngine
    {
        private bool isRunning = false;

        public void Start()
        {
            this.isRunning = true;

            var wh = new AutoResetEvent(false);
            var fsw = new FileSystemWatcher(".");
            fsw.Filter = @"C:\Users\colley\OneDrive\swtor-parser\sample-logs\CombatLogs\combat_2017-02-21_21_05_53_596881.txt";
            fsw.EnableRaisingEvents = true;
            fsw.Changed += (s, e) => wh.Set();

            var fs = new FileStream(@"C:\Users\colley\OneDrive\swtor-parser\sample-logs\CombatLogs\combat_2017-02-21_21_05_53_596881.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                var s = "";
                while (this.isRunning)
                {
                    s = sr.ReadLine();
                    if (s != null)
                    {

                        Console.WriteLine(s);
                    }
                    else
                    {
                        wh.WaitOne(1000);
                    }
                }
            }
        }

        public void Stop()
        {
            this.isRunning = false;
        }
    }
}
