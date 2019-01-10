using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ReleaseEmailMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private string _format = string.Empty;

        internal string ProductName { get; set; }

        internal List<ReleaseVersion> ReleaseVersions { get => _releaseVersions; set => _releaseVersions = value; }
        private List<ReleaseVersion> _releaseVersions = new List<ReleaseVersion>();

        private void ExcaliburRB_Checked(object sender, RoutedEventArgs e)
        {
            ProductName = "Excalibur";
        }

        private void OsriRB_Checked(object sender, RoutedEventArgs e)
        {
            ProductName = "OSRI";
        }
        
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void HelpBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddVersionBtn_Click(object sender, RoutedEventArgs e)
        {
            ReleaseVersion rv = ReleaseVersions.Find(p => p.VersionNumber == versionTB.Text);
            if (rv == null)
            {
                rv = new ReleaseVersion(ProductName);
                ReleaseVersions.Add(rv);
            }

            var packageLocation = locationTB.Text;
            rv.UpdateLocation(locationTB.Text);
            rv.UpdateVersion(ReleaseVersion.VersionType.NONE, versionTB.Text);
            rv.UpdateVersion(ReleaseVersion.VersionType.DEBUG, debugVersionTB.Text);
            rv.UpdateVersion(ReleaseVersion.VersionType.RELEASE, releaseVersionTB.Text);
            documentTB.Text = string.Join("\n", ReleaseVersions);
        }

        private void DeleteVersionBtn_Click(object sender, RoutedEventArgs e)
        {
            ReleaseVersion rv = ReleaseVersions.Find(p => p.VersionNumber == versionTB.Text);
            if (rv != null)
            {
                ReleaseVersions.Remove(rv);
            }
            documentTB.Text = string.Join("\n", ReleaseVersions);
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ReleaseVersion rv = ReleaseVersions.Find(p => p.VersionNumber == versionItemTB.Text);
            
            if (bugRB.IsChecked.Value)
            {
                rv.AddItem(ReleaseVersion.ItemType.BUG, jiraTB.Text, CustomTB.Text);
            }
            else if (storyRB.IsChecked.Value)
            {
                rv.AddItem(ReleaseVersion.ItemType.STORY, jiraTB.Text, CustomTB.Text);
            }
            else
            {
                rv.AddItem(ReleaseVersion.ItemType.ISSUE, jiraTB.Text, CustomTB.Text);
            }
        }
    }
}
