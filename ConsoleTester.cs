using System;
using System.Timers;

namespace MPlay
{
    internal class ConsoleTester
    {
        public void Main(string args)
        {
            Console.WriteLine("hi");
        }
        void CreateTimer()
        {
            var timer = new Timer(1000); // fire every 1 second
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Hello Mono World");
        }
    }
}
