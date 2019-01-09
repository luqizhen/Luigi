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

namespace DellSetupAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<DataItem> Database = new List<DataItem>();

        public MainWindow()
        {
            InitializeComponent();
            databaseDG.ItemsSource = Database;
        }

        private void SendTelemetryBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var inTags = tagsTB.Text.Split(',').Select(p => p.Trim().ToUpper()).ToList();
                var inAPPs = appsTB.Text.Split(',').Select(p => p.Trim().ToUpper()).ToList();

                foreach (var app in inAPPs)
                {
                    DataItem dataItem = Database.Find(p => p.App == app);
                    if (dataItem == null)
                    {
                        DataItem newItem = new DataItem();
                        newItem.App = app;
                        inTags.Distinct().ToList().ForEach(p => newItem.Tags.Add(p, 1));
                        Database.Add(newItem);
                    }
                    else
                    {
                        inTags.Distinct().ToList().ForEach(p =>
                        {
                            if (dataItem.Tags.Keys.Contains(p))
                            {
                                dataItem.Tags[p] += 1;
                            }
                            else
                            {
                                dataItem.Tags.Add(p, 1);
                            }
                        });
                    }
                }
                databaseDG.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MatchBtn_Click(object sender, RoutedEventArgs e)
        {
            var outTags = outTagsTB.Text.Split(',').Select(p => p.Trim().ToUpper()).ToList();
            string outApps = string.Empty;
            Database.ForEach(p =>
            {
                if(p.SelectedTags.Any(ele => outTags.Contains(ele)))
                {
                    outApps += $"[{p.App}]";
                }
            });
            outAppsTB.Text = outApps;
        }
    }

    public class DataItem
    {
        private string _app = string.Empty;
        public Dictionary<string, int> Tags = new Dictionary<string, int>();
        public List<string> SelectedTags = new List<string>();

        public void Update()
        {
            SelectedTags.Clear();
            int threshold = Tags.Values.Max() / 2;
            foreach (var tag in Tags)
            {
                if (tag.Value > threshold)
                {
                    SelectedTags.Add(tag.Key);
                }
            }
        }

        public string TagsStr
        {
            get
            {
                Update();
                string tagsStr = string.Empty;
                SelectedTags.ForEach(p => tagsStr += $"[{p}]");
                return tagsStr;
            }
        }

        public string App
        {
            get
            {
                return _app;
            }
            set
            {
                _app = value;
            }
        }
    }
}
