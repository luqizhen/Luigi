using System;

namespace EcoQoS.Test.WPF
{
    public class Record
    {
        public string TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int Cycles { get; set; }
        public string QoS { get; set; }
        public string Message { get; set; }
    }
}