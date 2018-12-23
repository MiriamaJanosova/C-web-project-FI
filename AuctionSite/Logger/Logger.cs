using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger
    {
        private readonly object mtx = new object();

        public readonly Queue<string> Queue = new Queue<string>();

        private CancellationTokenSource Source = new CancellationTokenSource();

        private CancellationToken Token => Source.Token;

        public string FileName { get; set; }

        private void process()
        {
            lock (mtx)
            {
                Task.Run(async () =>
                {
                    while (true)
                    {
                        Monitor.Wait(mtx);
                        using (var stream = File.Open(FileName, FileMode.OpenOrCreate))
                        using (var writer = new StreamWriter(stream))
                        {
                            await writer.WriteAsync(Queue.Dequeue());
                        }
                    }
                }, Token);
            }
        }

        public Logger()
        {
            process();
        }

        public enum LoggedType
        {
            Exception,
            Information
        }

        public void Log(LoggedType Type, params object[] parameters)
        {
            lock (mtx)
            {
                var sb = new StringBuilder();
                switch (Type)
                {
                    case LoggedType.Exception:
                        FileName = $"critical-{DateTime.Now}.txt";
                        sb.AppendLine("Critical error occured:");
                        break;
                }

                var text = parameters.Aggregate(sb, (str, o) => str.AppendLine($"\t{o}")).ToString();
                Queue.Enqueue(text);
                Monitor.Pulse(mtx);
            }
        }


        public void Stop()
        {
            Source.Cancel();
        }
    }
}
