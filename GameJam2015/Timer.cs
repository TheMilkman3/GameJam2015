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
        public static DateTime startTime, previousTime;
        public static double totalTime;
        public static int eventsFired = 0;

        public Timer()
        {
            aTimer = new System.Timers.Timer();
            startTime = DateTime.Now;
            previousTime = DateTime.Now;

            aTimer.Elapsed += Tick;
        }

        public Timer(Double milli)
        {
            aTimer = new System.Timers.Timer(milli);
            startTime = DateTime.Now;
            previousTime = DateTime.Now;

            aTimer.Elapsed += Tick;
        }

        public void Start()
        {
            aTimer.Start();
        }

        public Double ElapsedTime()
        {
            return totalTime;
        }

        public static void Tick(Object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Time event fired!! Interval: {0}", (e.SignalTime - previousTime).TotalSeconds);

            previousTime = e.SignalTime;
            totalTime = (e.SignalTime - startTime).TotalSeconds;

            Console.WriteLine("Elapsed time: {0}", totalTime);
        }
    }
}
