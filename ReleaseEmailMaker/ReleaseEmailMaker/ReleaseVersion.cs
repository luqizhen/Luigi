using System.Collections.Generic;

namespace ReleaseEmailMaker
{
    internal class ReleaseVersion
    {

        private List<string> bugItems = new List<string>();
        private List<string> storyItems = new List<string>();
        private List<string> issueItems = new List<string>();

        public ReleaseVersion(string productName)
        {
            if(string.IsNullOrWhiteSpace(productName))
            {
                return;
            }

            this.ProductName = productName;
        }

        public string ProductName { get; private set; }
        public string VersionNumber { get; private set; }
        public string DebugVersionNumber { get; private set; }
        public string ReleaseVersionNumber { get; private set; }
        public string Location { get; private set; }

        public void AddItem(ItemType type, string ID, string custom)
        {
            string item = null;
            if (!string.IsNullOrWhiteSpace(ID))
            {
                item = JiraManager.Instance.GetTitle(ID);
                if (!string.IsNullOrWhiteSpace(item))
                {
                    item = ID + "\t" + item;
                }
            }
            else if (!string.IsNullOrWhiteSpace(custom))
            {
                item = custom;
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

        public override string ToString()
        {
            string bugs = bugItems.Count > 0 ? string.Join("\n", bugItems) : "None";
            string stories = storyItems.Count > 0 ? string.Join("\n", storyItems) : "None";
            string issues = issueItems.Count > 0 ? string.Join("\n", issueItems) : "None";

            // generate issues/bugs/stories string

            if (!string.IsNullOrWhiteSpace(DebugVersionNumber) && !string.IsNullOrWhiteSpace(ReleaseVersionNumber))
            {
                return string.Format(Constants.FORMAT_TWO_VERSIONS, ProductName, VersionNumber, DebugVersionNumber, ReleaseVersionNumber, Location, bugs, stories, issues);
            }
            else if (!string.IsNullOrWhiteSpace(DebugVersionNumber))
            {
                return string.Format(Constants.FORMAT_ONE_VERSION, ProductName, VersionNumber, DebugVersionNumber, "debug", Location, bugs, stories, issues);
            }
            else if (!string.IsNullOrWhiteSpace(ReleaseVersionNumber))
            {
                return string.Format(Constants.FORMAT_ONE_VERSION, ProductName, VersionNumber, ReleaseVersionNumber, "release", Location, bugs, stories, issues);
            }
            return "Please enter debug/release version";
        }

        internal void UpdateLocation(string text)
        {
            Location = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }

        internal void UpdateVersion(VersionType type, string text)
        {
            switch (type)
            {
                case VersionType.NONE:
                    VersionNumber = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
                    break;
                case VersionType.DEBUG:
                    DebugVersionNumber = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
                    break;
                case VersionType.RELEASE:
                    ReleaseVersionNumber = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
                    break;
                default:
                    break;
            }
        }

        public enum ItemType
        {
            BUG,
            STORY,
            ISSUE
        }

        public enum VersionType
        {
            NONE,
            DEBUG,
            RELEASE
        }
    }
}