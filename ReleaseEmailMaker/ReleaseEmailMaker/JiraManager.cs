using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace ReleaseEmailMaker
{
    internal class JiraManager
    {
        private static JiraManager _instance = null;
        public static JiraManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new JiraManager();
                }
                return _instance;
            }
        }

        private Jira _jira;
        private bool _isLogin = false;
        private Issue _tempissue;
        private string _tempID;

        public string URL { get => @"https://jira.cpgswtools.com"; }
        public bool IsLogin { get => _isLogin; private set => _isLogin = value; }

        private JiraManager()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public Issue GetIssue(string jiraID, bool useLocal = true)
        {
            if (_tempID == jiraID)
            {
                return _tempissue;
            }

            Issue issue = null;
            try
            {
                issue = _jira.Issues.GetIssueAsync(jiraID).Result;
                _tempissue = issue;
                _tempID = jiraID;
                return _tempissue;
            }
            catch
            {
                return null;
            }
        }

        public string GetTitle(string jiraID, bool useLocal = true)
        {
            var issue = GetIssue(jiraID, useLocal);
            return issue.Summary;
        }

        public ReleaseVersion.ItemType GetType(string jiraID, bool useLocal = true)
        {
            var issue = GetIssue(jiraID, useLocal);
            var type = issue.Type.Name.ToUpper();
            if (type == "BUG")
            {
                return ReleaseVersion.ItemType.BUG;
            }
            else
            {
                return ReleaseVersion.ItemType.STORY;
            }
        }

        public DateTime? GetCreatedTime(string jiraID, bool useLocal = true)
        {
            var issue = GetIssue(jiraID, useLocal);
            return issue.Created;
        }

        public IssueStatus GetStatus(string jiraID, bool useLocal = true)
        {
            var issue = GetIssue(jiraID, useLocal);
            return issue.Status;
        }

        public string GetSprint(string jiraID, bool useLocal = true)
        {
            var issue = GetIssue(jiraID, useLocal);
            var sprintName = issue.CustomFields.GetCascadingSelectField("sprint").ParentOption;
            return sprintName;
        }
        
        public List<Issue> GetIssuesInSprint(string sprintName)
        {
            var issues = from i in _jira.Issues.Queryable
                         where i.CustomFields.GetCascadingSelectField("sprint").ParentOption == sprintName
                         orderby i.JiraIdentifier
                         select i;
            return issues.ToList();
        }

        public List<Issue> GetIssuesFinishInThisWeek(string version = null)
        {
            var issues = _jira.Filters.GetIssuesFromFavoriteAsync("finish in this week").Result;
            if (version == null)
            {
                return issues.ToList();
            }
            else
            {
                return issues.Where(p => p.FixVersions == version || ((p.FixVersions == null || p.FixVersions.Count == 0) && p.AffectsVersions == version)).ToList();
            }
        }

        public bool IsDone(string jiraID, bool useLocal = true)
        {
            var issue = GetIssue(jiraID, useLocal);
            return issue.Resolution.Name.ToUpper() == "DONE";
        }



        internal bool Login(string username, string password)
        {
            try
            {
                _jira = Jira.CreateRestClient(URL, username,password);
            }
            catch (Exception)
            {
                IsLogin = false;
                return false;
            }
            IsLogin = true;
            return true;
        }
    }
}