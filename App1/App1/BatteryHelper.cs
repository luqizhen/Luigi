using System;
using System.Threading.Tasks;
using Windows.Devices.Power;

namespace App1
{
    public class BatteryHelper
    {
        public static BatteryReport GetLatestBatteryStatus()
        {
            var report = Battery.AggregateBattery.GetReport();
            return report;
        }
        public static void StartGetLatestBatteryStatus(Action<BatteryReport> callback)
        {
            var battery = Battery.AggregateBattery;

            battery.ReportUpdated += (sender, args)=>
            {
                callback(battery.GetReport());
            };
        }
    }
}
