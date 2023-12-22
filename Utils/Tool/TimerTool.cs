using System;
using System.Timers;

namespace Utils.Tool
{
    public class TimerTool
    {
        /// <summary>
        /// 添加定时任务
        /// </summary>
        /// <param name="dateTime">指定触发时间，若已经过了时间则立即触发</param>
        public static Timer AddTimerEvent(DateTime dateTime, Action action)
        {
            long delay = dateTime.Ticks - DateTime.Now.Ticks;
            if (delay < 0)
            {
                delay = 0;
            }
            return AddTimerEvent(delay, action);
        }

        /// <summary>
        /// 添加延时任务
        /// </summary>
        /// <param name="delay">延时（毫秒），若小于0则立即触发</param>
        public static Timer AddTimerEvent(long delay, Action action)
        {
            if (delay < 0)
            {
                delay = 0;
            }
            Timer timerRun = new()
            {
                AutoReset = false,
                Interval = delay
            };
            timerRun.Elapsed += delegate
            {
                action?.Invoke();
                timerRun.Dispose();
            };
            timerRun.Start();
            return timerRun;
        }
    }
}
