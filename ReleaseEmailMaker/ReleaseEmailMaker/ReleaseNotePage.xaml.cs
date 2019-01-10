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
    /// Interaction logic for ReleaseEmailPage.xaml
    /// </summary>
    public partial class ReleaseNotePage : Page
    {
        public ReleaseNotePage()
        {
            InitializeComponent();
        }

        internal string ProductName { get; set; }

        internal List<ReleaseVersion> ReleaseVersions { get => _releaseVersions; set => _releaseVersions = value; }
        public bool IsAutoType { get; private set; }

        private List<ReleaseVersion> _releaseVersions = new List<ReleaseVersion>();

        private void ExcaliburRB_Checked(object sender, RoutedEventArgs e)
        {
            ProductName = "Excalibur";
        }

        private void OsriRB_Checked(object sender, RoutedEventArgs e)
        {
            ProductName = "OSRI";
        }
        
        private void CustomProjectRB_Checked(object sender, RoutedEventArgs e)
        {
            ProductName = customProjectTB.Text;
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

        private void ChangeItem(bool isAdd)
        {
            ReleaseVersion rv = ReleaseVersions.Find(p => p.VersionNumber == versionItemTB.Text);
            if (rv == null)
            {
                MessageBox.Show("Please add version first!");
                return;
            }

            var id = jiraTB.Text;
            var custom = CustomTB.Text;
            ReleaseVersion.ItemType type;
            if (IsAutoType)
            {
                type = ReleaseVersion.ItemType.AUTO;
            }
            else if (bugRB.IsChecked.Value)
            {
                type = ReleaseVersion.ItemType.BUG;
            }
            else if (storyRB.IsChecked.Value)
            {
                type = ReleaseVersion.ItemType.STORY;
            }
            else if (issueRB.IsChecked.Value)
            {
                type = ReleaseVersion.ItemType.ISSUE;
            }
            else
            {
                MessageBox.Show("Please choose one type or use AUTO mode.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(id) && !JiraManager.Instance.IsLogin)
            {
                MessageBox.Show("Please login JIRA first.");
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    if (isAdd)
                    {
                        rv.AddItem(type, id, custom);
                    }
                    else
                    {
                        rv.DeleteItem(type, id, custom);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("ERROR in add/delete item: " + ex);
                }

                documentTB.Dispatcher.Invoke(() => documentTB.Text = string.Join("\n", ReleaseVersions));
            });

        }

        private void AutoCB_Checked(object sender, RoutedEventArgs e)
        {
            IsAutoType = autoCB.IsChecked.Value;
            bugRB.IsEnabled = !IsAutoType;
            storyRB.IsEnabled = !IsAutoType;
            issueRB.IsEnabled = !IsAutoType;
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeItem(true);
        }

        private void DeleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeItem(false);
        }
    }
}
