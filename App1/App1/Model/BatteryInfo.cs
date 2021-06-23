using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.Devices.Power;
using Windows.System.Power;

namespace App1.Model
{
    public class BatteryInfo: ObservableObject
    {
        public BatteryInfo(BatteryReport batteryReport)
        {
            Status = batteryReport.Status;

            switch (Status)
            {
                case BatteryStatus.NotPresent:
                    StatusColor = "#777777";
                    break;
                case BatteryStatus.Discharging:
                    StatusColor = "#770000";
                    break;
                case BatteryStatus.Idle:
                    StatusColor = "#000077";
                    break;
                case BatteryStatus.Charging:
                    StatusColor = "#007700";
                    break;
                default:
                    StatusColor = "#000000";
                    break;
            }

            Maximum = batteryReport.FullChargeCapacityInMilliwattHours ?? 0;
            Remaining = batteryReport.RemainingCapacityInMilliwattHours ?? 0;
            Design = batteryReport.DesignCapacityInMilliwattHours ?? 0;

            Percentage = Remaining / Maximum;
            PercentageString = Percentage.ToString("P");
        }

        private BatteryStatus status;
        public BatteryStatus Status
        {
            get => status;
            set
            {
                SetProperty(ref status, value);
            }
        }

        private string statusColor;
        public string StatusColor
        {
            get => statusColor;
            set
            {
                SetProperty(ref statusColor, value);
            }
        }

        private double remaining;
        public double Remaining
        {
            get => remaining;
            set
            {
                SetProperty(ref remaining, value);
            }
        }

        private double maximum;
        public double Maximum
        {
            get => maximum;
            set
            {
                SetProperty(ref maximum, value);
            }
        }

        private double design;
        public double Design
        {
            get => design;
            set
            {
                SetProperty(ref design, value);
            }
        }

        private double percentage;
        public double Percentage
        {
            get => percentage;
            set
            {
                SetProperty(ref percentage, value);
            }
        }

        private string percentageString;
        public string PercentageString
        {
            get => percentageString;
            set
            {
                SetProperty(ref percentageString, value);
            }
        }
    }
}
