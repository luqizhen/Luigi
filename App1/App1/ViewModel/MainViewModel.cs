using App1.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace App1.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Battery = new BatteryInfo(BatteryHelper.GetLatestBatteryStatus());

            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Dt_Tick; ;
            dt.Start();
        }

        private void Dt_Tick(object sender, object e)
        {
            dt.Start();
            Battery = new BatteryInfo(BatteryHelper.GetLatestBatteryStatus());
            dt.Start();
        }

        private DispatcherTimer dt;

        private BatteryInfo battery;
        public BatteryInfo Battery
        {
            get => battery;
            set
            {
                SetProperty(ref battery, value);
            }
        }
    }
}
