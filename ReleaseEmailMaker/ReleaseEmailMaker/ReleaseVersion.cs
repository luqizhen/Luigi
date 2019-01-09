using System.Collections.Generic;

namespace ReleaseEmailMaker
{
    internal class ReleaseVersion
    {
        private string productName;
        private string version;
        private string format;
        private List<string> bugItems = new List<string>();
        private List<string> storyItems = new List<string>();
        private List<string> issueItems = new List<string>();
        private string releaseVersion;
        private string debugVersion;
        private string location;

        public ReleaseVersion(string productName, string version, string format)
        {
            if(string.IsNullOrWhiteSpace(productName) || string.IsNullOrWhiteSpace(version) || string.IsNullOrWhiteSpace(format))
            {
                return;
            }

            this.productName = productName;
            this.version = version;
            this.format = format;
        }

        public void AddItem(ItemType type, string ID, string title)
        {
            string item = null;
            if (string.IsNullOrWhiteSpace(ID))
            {
                item = title;
            }
            else
            {
                item = ID + "\t" + title;
            }

            switch (type)
            {
                case ItemType.BUG:
                    bugItems.Add(item);
                    break;
                case ItemType.STORY:
                    storyItems.Add(item);
                    break;
                case ItemType.ISSUE:
                    issueItems.Add(item);
                    break;
                default:
                    break;
            }
        }

        public string Print()
        {
            string issues = null;
            string stories = null;
            string bugs = null;

            // generate issues/bugs/stories string

            return string.Format(format, productName, version, debugVersion, releaseVersion, location, bugs, stories, issues);
        }

        public enum ItemType
        {
            BUG,
            STORY,
            ISSUE
        }
    }
}