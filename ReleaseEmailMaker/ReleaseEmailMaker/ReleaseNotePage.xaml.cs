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
            TurnAllButton(false);
            if (!JiraManager.Instance.IsLogin)
            {
                MessageBox.Show("Please login JIRA first.");
                TurnAllButton(true);
                return;
            }

            string location = locationTB.Text;
            string version = versionTB.Text;
            string debugVersion = debugVersionTB.Text;
            string releaseVersion = releaseVersionTB.Text;

            Task.Run(() =>
            {
                ReleaseVersion rv = ReleaseVersions.Find(p => p.VersionNumber == version);
                if (rv == null)
                {
                    rv = new ReleaseVersion(ProductName);
                    ReleaseVersions.Add(rv);
                }

                rv.UpdateLocation(location);
                rv.UpdateVersion(ReleaseVersion.VersionType.NONE, version);
                rv.UpdateVersion(ReleaseVersion.VersionType.DEBUG, debugVersion);
                rv.UpdateVersion(ReleaseVersion.VersionType.RELEASE, releaseVersion);
                rv.UpdateItemsByVersion();
                UpdateDocoment();
                TurnAllButton(true);
            });
        }


        private void DeleteVersionBtn_Click(object sender, RoutedEventArgs e)
        {
            TurnAllButton(false);
            ReleaseVersion rv = ReleaseVersions.Find(p => p.VersionNumber == versionTB.Text);
            if (rv != null)
            {
                ReleaseVersions.Remove(rv);
            }
            documentTB.Text = string.Join("\n", ReleaseVersions);
            TurnAllButton(true);
        }

        private void ChangeItem(bool isAdd)
        {
            TurnAllButton(false);
            ReleaseVersion rv = ReleaseVersions.Find(p => p.VersionNumber == versionItemTB.Text);
            if (rv == null)
            {
                MessageBox.Show("Please add version first!");
                TurnAllButton(true);
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
                TurnAllButton(true);
                return;
            }

            if (!string.IsNullOrWhiteSpace(id) && !JiraManager.Instance.IsLogin)
            {
                MessageBox.Show("Please login JIRA first.");
                TurnAllButton(true);
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
                UpdateDocoment();
                TurnAllButton(true);
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

        private void versionTB_GotFocus(object sender, RoutedEventArgs e)
        {
            string version = versionTB.Text;
            if (!debugVersionTB.Text.StartsWith(version))
            {
                debugVersionTB.Text = version + ".";
            }
            if (!releaseVersionTB.Text.StartsWith(version))
            {
                releaseVersionTB.Text = version + ".";
            }
        }

        private void UpdateDocoment()
        {
            documentTB.Dispatcher.Invoke(() =>
            {
                var title = "";
                ReleaseVersions.Sort((i, j) => string.Compare(i.VersionNumber, j.VersionNumber));
                foreach (var releaseVersion in ReleaseVersions)
                {
                    if (title.Contains(releaseVersion.ProductName))
                    {
                        title += "& " + releaseVersion.VersionNumber + " ";
                    }
                    else
                    {
                        title += releaseVersion.ProductName + " " + releaseVersion.VersionNumber + " ";
                    }
                }
                title += ReleaseVersions.Count > 1 ? "are " : "is ";
                title += "ready";
                docTitleTB.Text = title;
                docToTB.Text = null;
                docCCTB.Text = null;
                documentTB.Text = Constants.FORMAT_EMAIL_START + string.Join("\n", ReleaseVersions) + Constants.FORMAT_EMAIL_END;
                docEndTB.Text = "Luigi Lu";
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

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            var emailTitle = docTitleTB.Text;
            var emailContent = documentTB.Text + docEndTB.Text;

            if(string.IsNullOrWhiteSpace(emailTitle) ||
                string.IsNullOrWhiteSpace(emailContent))
            {
                MessageBox.Show("Title/Content can't be blank!");
                return;
            }
            MessageBox.Show(emailTitle + "\n\n" + emailContent, "Please confirm the email!!!");
        }
    }
}
