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
        private bool _isTaskARunning = false;
        private bool _isTaskBRunning = false;

        public ObservableCollection<Record> Records { get; set; } = new ObservableCollection<Record>();

        public bool IsTaskARunning
        {
            get => _isTaskARunning;
            private set
            {
                _isTaskARunning = value;
                Refresh();
            }
        }

        public bool IsTaskBRunning
        {
            get => _isTaskBRunning;
            private set
            {
                _isTaskBRunning = value;
                Refresh();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            Refresh();
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

        private async void btn_a_start_Click(object sender, RoutedEventArgs e)
        {
            IsTaskARunning = true;

            var record = await _taskRunner.RunTaskAAsync(progressCallback:ShowProgressA);
            Records.Add(record);

            IsTaskARunning = false;
        }

        private async void btn_b_start_Click(object sender, RoutedEventArgs e)
        {
            IsTaskBRunning = true;

            var record = await _taskRunner.RunTaskBAsync(progressCallback: ShowProgressB);
            Records.Add(record);

            IsTaskBRunning = false;
        }

        private void btn_a_stop_Click(object sender, RoutedEventArgs e) => _taskRunner.StopTaskA();

        private void btn_b_stop_Click(object sender, RoutedEventArgs e) => _taskRunner.StopTaskB();

        private void Refresh()
        {
            btn_a_start.IsEnabled = !_isTaskARunning;
            btn_a_stop.IsEnabled = _isTaskARunning;
            btn_b_start.IsEnabled = !_isTaskBRunning;
            btn_b_stop.IsEnabled = _isTaskBRunning;
            sp_QoS.IsEnabled = !_isTaskARunning && !_isTaskBRunning;
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
