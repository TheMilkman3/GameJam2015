using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace GameJam2015
{
    public class Timer
    {
        public System.Timers.Timer aTimer;
        public static DateTime startTime;

        public Timer()
        {
            aTimer = new System.Timers.Timer();
            startTime = DateTime.Now;

            aTimer.Elapsed += Tick;
        }

        public Timer(Double milli)
        {
            aTimer = new System.Timers.Timer(milli);
            startTime = DateTime.Now;

            aTimer.Elapsed += Tick;
        }

        public void Start()
        {
            aTimer.Start();
        }

        public static void Tick(Object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Time event fired!! Interval: {0}", (e.SignalTime - startTime).TotalSeconds);

            startTime = e.SignalTime;
        }
    }
}
