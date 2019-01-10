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
            var packageLocation = locationTB.Text;
            ReleaseVersion rv = new ReleaseVersion(ProductName);
            rv.UpdateLocation(locationTB.Text);
            rv.UpdateVersion(ReleaseVersion.VersionType.NONE, versionTB.Text);
            rv.UpdateVersion(ReleaseVersion.VersionType.DEBUG, debugVersionTB.Text);
            rv.UpdateVersion(ReleaseVersion.VersionType.RELEASE, releaseVersionTB.Text);
            ReleaseVersions.Add(rv);
            documentTB.Text = ReleaseVersions.ToString();
        }

        private void DeleteVersionBtn_Click(object sender, RoutedEventArgs e)
        {
            ReleaseVersions.Remove(_releaseVersions.Find(p=>p.VersionNumber == versionTB.Text));
            documentTB.Text = ReleaseVersions.ToString();
        }
    }
}
