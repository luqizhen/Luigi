using EcoQoS.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EcoQoS.Test.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskRunner _taskRunner = new TaskRunner();

        public ObservableCollection<Record> Records { get; set; } = new ObservableCollection<Record>();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void rbtn_EcoQoS_Checked(object sender, RoutedEventArgs e)
        {
            if (!ProcessInformationWrapper.SwitchToEcoQoS())
            {
                rbtn_HighQoS.IsChecked = true;
                MessageBox.Show("Failed to switch to EcoQoS.");
            }
        }

        private void rbtn_HighQoS_Checked(object sender, RoutedEventArgs e)
        {
            if (!ProcessInformationWrapper.SwitchToHighQoS())
            {
                rbtn_EcoQoS.IsChecked = true;
                MessageBox.Show("Failed to switch to HighQoS.");
            }
        }

        private void btn_a_start_Click(object sender, RoutedEventArgs e)
        {
            btn_a_start.IsEnabled = false;
            btn_a_stop.IsEnabled = true;
            _taskRunner.StartTaskA(progressCallback:ShowProgressA);
        }

        private void btn_a_stop_Click(object sender, RoutedEventArgs e)
        {
            var newRecord = _taskRunner.StopTaskA();
            Records.Add(newRecord);
            btn_a_stop.IsEnabled = false;
            btn_a_start.IsEnabled = true;
        }

        private void btn_b_start_Click(object sender, RoutedEventArgs e)
        {
            btn_b_start.IsEnabled = false;
            btn_b_stop.IsEnabled = true;
            _taskRunner.StartTaskB(progressCallback: ShowProgressB);
        }

        private void btn_b_stop_Click(object sender, RoutedEventArgs e)
        {
            var newRecord = _taskRunner.StopTaskB();
            Records.Add(newRecord);
            btn_b_stop.IsEnabled = false;
            btn_b_start.IsEnabled = true;
        }

        private void AddRecord(string task, DateTime startTime, TimeSpan duration, int cycles, string msg)
        {
            Record newRecord = new Record() { TaskName = task, StartTime = startTime, Duration = duration, Cycles = cycles, Message = msg };
            Records.Add(newRecord);
        }

        private void ShowProgressA(int cycles, int progress)
        {
            Dispatcher.Invoke(() =>
            {
                tb_a_cycles.Text = cycles.ToString();
                pb_a_progress.Value = progress;
            });
        }

        private void ShowProgressB(int cycles, int progress)
        {
            Dispatcher.Invoke(() =>
            {
                tb_b_cycles.Text = cycles.ToString();
                pb_b_progress.Value = progress;
            });
        }
    }
}
