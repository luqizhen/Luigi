using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for InternalTestPage.xaml
    /// </summary>
    public partial class InternalTestPage : Page
    {
        public InternalTestPage()
        {
            InitializeComponent();
            testVersionsLB.ItemsSource = TestVersions;
        }
        
        private List<TestVersion> testVersions = new List<TestVersion>();
        public List<TestVersion> TestVersions { get => testVersions; set => testVersions = value; }

        private void TestVersionCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void testVersionBtn_Click(object sender, RoutedEventArgs e)
        {
            TestVersion testVersion = (sender as Button).DataContext as TestVersion;
            Process p = new Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = $"\"{testVersion.URL}\"";
            p.Start();
        }

        private void PartialBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FailBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteTestVersionBtn_Click(object sender, RoutedEventArgs e)
        {
            TestVersion testVersion = (sender as Button).DataContext as TestVersion;
            TestVersions.Remove(testVersion);
            testVersionsLB.Items.Refresh();
        }

        private void AddVersionBtn_Click(object sender, RoutedEventArgs e)
        {
            TestVersion testVersion = new TestVersion();
            testVersion.Date = addDateTB.Text;
            testVersion.Project = addProjectTB.Text;
            testVersion.Version = addVersionTB.Text;
            TestVersions.Add(testVersion);
            testVersionsLB.Items.Refresh();
        }
    }

    public class TestVersion
    {
        public string Date { get; set; }
        public string Project { get; set; }
        public string Version { get; set; }
        public string ProjectVersion => Project + " " + Version;
        public string URL => @"https://www.baidu.com/s?wd=" + ProjectVersion + " " + Date.Replace("/","");
        public int Pass { get; set; }
        public int Partial { get; set; }
        public int Fail { get; set; }
    }
}
