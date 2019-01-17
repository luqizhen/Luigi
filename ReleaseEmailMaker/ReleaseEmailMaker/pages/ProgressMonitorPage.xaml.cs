using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Atlassian.Jira;

namespace ReleaseEmailMaker
{
    /// <summary>
    /// Interaction logic for ProgressMonitorPage.xaml
    /// </summary>
    public partial class ProgressMonitorPage : Page
    {
        internal List<MyIssue> MonitoredIssues = new List<MyIssue>();
        internal List<PullRequest> MonitoredPRs = new List<PullRequest>();

        public ProgressMonitorPage()
        {
            InitializeComponent();
            monitorIssuesLB.ItemsSource = MonitoredIssues;
            monitorPRLB.ItemsSource = MonitoredPRs;
        }

        private void JIRA_link_click(object sender, RoutedEventArgs e)
        {
            MyIssue issue = (sender as Button).DataContext as MyIssue;
            Process p = new Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = $"\"{issue.URL}\"";
            p.Start();
        }

        private void PR_link_click(object sender, RoutedEventArgs e)
        {

        }
        


        private void Set_Green_Click(object sender, RoutedEventArgs e)
        {
            var item = (ListBoxItem)monitorIssuesLB.ContainerFromElement(sender as Button);
            item.Background = new SolidColorBrush(Colors.LightGreen);
            (item.DataContext as MyIssue).Level = "0";
        }

        private void Set_Yellow_Click(object sender, RoutedEventArgs e)
        {
            var item = (ListBoxItem)monitorIssuesLB.ContainerFromElement(sender as Button);
            item.Background = new SolidColorBrush(Colors.LightYellow);
            (item.DataContext as MyIssue).Level = "1";
        }

        private void Set_Red_Click(object sender, RoutedEventArgs e)
        {
            var item = (ListBoxItem)monitorIssuesLB.ContainerFromElement(sender as Button);
            item.Background = new SolidColorBrush(Colors.LightPink);
            (item.DataContext as MyIssue).Level = "2";
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
                MonitoredIssues.AddRange(JiraManager.Instance.GetIssuesUnderMonitor().Select(issue => new MyIssue(issue)));

                UpdatePage();
                TurnAllButton(true);
            });
        }

        private void UpdatePage()
        {
            monitorIssuesLB.Dispatcher.Invoke(() =>
            {
                //MonitoredIssues.Sort((i, j) => string.Compare(i.Assignee, j.Assignee));
                //MonitoredPRs.Sort((i, j) => string.Compare(i.Assignee, j.Assignee));
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

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var issuesText = SimpleJson.SimpleJson.SerializeObject(MonitoredIssues);
            File.WriteAllText("MonitoredIssues.json", issuesText);
            MessageBox.Show(issuesText);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var issuesText = File.ReadAllText("MonitoredIssues.json");
                var issueJSON = SimpleJson.SimpleJson.DeserializeObject(issuesText);
                MonitoredIssues.Clear();
                MonitoredIssues.AddRange(issueJSON as List<MyIssue>);
                UpdatePage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't find/load cache file: "+ex);
            }
        }
    }

    internal class MyIssue
    {
        public string Assignee { get; private set; }
        public string Key { get; private set; }
        public string Summary { get; private set; }
        public string Status { get; private set; }
        public string Priority { get; private set; }
        public string Updated { get; private set; }
        public string Level { get; set; }
        public string URL { get => @"https://jira.cpgswtools.com/browse/" + Key; }

        public MyIssue(Issue e)
        {
            Assignee = e.Assignee.Replace(@"_",@" ");
            Key = e.Key.Value;
            Summary = e.Summary.Replace(@"_", @" ");
            Status = e.Status.Name;
            Priority = e.Priority.Name;
            Updated = e.Updated.Value.Date.ToShortDateString();
            Level = "?";
        }

        public override string ToString()
        {
            return SimpleJson.SimpleJson.SerializeObject(this);
        }
    }
}
