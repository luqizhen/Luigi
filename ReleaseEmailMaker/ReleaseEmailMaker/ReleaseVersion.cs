using System;
using System.Collections.Generic;

namespace ReleaseEmailMaker
{
    internal class ReleaseVersion
    {

        private List<string> bugItems = new List<string>();
        private List<string> storyItems = new List<string>();
        private List<string> issueItems = new List<string>();
        private string productName;
        private string version;
        private string releaseVersion;
        private string debugVersion;
        private string location;

        public ReleaseVersion(string productName)
        {
            if(string.IsNullOrWhiteSpace(productName))
            {
                return;
            }

            this.productName = productName;
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

            return string.Format(Constants.FORMAT, productName, version, debugVersion, releaseVersion, location, bugs, stories, issues);
        }

        internal void UpdateLocation(string text)
        {
            location = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }

        internal void UpdateVersion(VersionType type, string text)
        {
            switch (type)
            {
                case VersionType.NONE:
                    version = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
                    break;
                case VersionType.DEBUG:
                    debugVersion = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
                    break;
                case VersionType.RELEASE:
                    releaseVersion = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
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