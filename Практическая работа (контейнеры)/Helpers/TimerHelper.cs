using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практическая_работа__контейнеры_.Helpers;

class TimerHelper
{
    public static long MeasureExecutionTime(Action algorithm)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        algorithm();

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}
