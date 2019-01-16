using Atlassian.Jira;
using System;
using System.Collections.Generic;
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

namespace ReleaseEmailMaker
{
    /// <summary>
    /// Interaction logic for ProgressMonitorPage.xaml
    /// </summary>
    public partial class ProgressMonitorPage : Page
    {
        internal List<Issue> MonitoredIssues = new List<Issue>();
        internal List<PullRequest> MonitoredPRs = new List<PullRequest>();

        public ProgressMonitorPage()
        {
            InitializeComponent();
            monitorIssuesLB.ItemsSource = MonitoredIssues;
            monitorPRLB.ItemsSource = MonitoredPRs;
        }

        private void JIRA_link_click(object sender, RoutedEventArgs e)
        {

        }

        private void PR_link_click(object sender, RoutedEventArgs e)
        {

        }
        


        private void Set_Green_Click(object sender, RoutedEventArgs e)
        {
            var item = (ListBoxItem)monitorIssuesLB.ContainerFromElement(sender as Button);
            item.Background = new SolidColorBrush(Colors.LightGreen);
        }

        private void Set_Yellow_Click(object sender, RoutedEventArgs e)
        {
            var item = (ListBoxItem)monitorIssuesLB.ContainerFromElement(sender as Button);
            item.Background = new SolidColorBrush(Colors.LightYellow);
        }

        private void Set_Red_Click(object sender, RoutedEventArgs e)
        {
            var item = (ListBoxItem)monitorIssuesLB.ContainerFromElement(sender as Button);
            item.Background = new SolidColorBrush(Colors.LightPink);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            TurnAllButton(false);
            if (!JiraManager.Instance.IsLogin)
            {
                MessageBox.Show("Please login first!");
                TurnAllButton(true);
                return;
            }
            Task.Run(() =>
            {
                //Get latest JIRA info from remote
                MonitoredIssues.Clear();
                MonitoredIssues.AddRange(JiraManager.Instance.GetIssuesUnderMonitor());

                UpdatePage();
                TurnAllButton(true);
            });
        }

        private void UpdatePage()
        {
            monitorIssuesLB.Dispatcher.Invoke(() =>
            {
                MonitoredIssues.Sort((i, j) => string.Compare(i.Assignee, j.Assignee));
                MonitoredPRs.Sort((i, j) => string.Compare(i.Assignee, j.Assignee));
                monitorIssuesLB.Items.Refresh();
                monitorPRLB.Items.Refresh();
            });
        }

        private void TurnAllButton(bool enable)
        {
            this.Dispatcher.Invoke(() =>
            {
                statusTB.Text = enable ? "Ready" : "Processing...";
                workGrid.IsEnabled = enable;
            });
        }
    }
}
