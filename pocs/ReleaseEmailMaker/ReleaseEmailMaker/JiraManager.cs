using System;
using System.Net;
using Jira.SDK;
using Jira.SDK.Domain;

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

        private Jira.SDK.Jira _jira;
        private bool _isLogin = false;
        private Issue _tempissue;
        private string _tempID;

        public string URL { get => @"https://jira.cpgswtools.com"; }
        public bool IsLogin { get => _isLogin; private set => _isLogin = value; }

        private JiraManager()
        {
            _jira = new Jira.SDK.Jira();
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
                issue = _jira.GetIssue(jiraID.ToUpper());
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
            return GetIssue(jiraID, useLocal)?.Summary;
        }

        internal ReleaseVersion.ItemType GetType(string jiraID, bool useLocal = true)
        {
            var id = GetIssue(jiraID, useLocal)?.IssueType.ID;
            if (id.HasValue)
            {
                return id == 1 ? ReleaseVersion.ItemType.BUG : ReleaseVersion.ItemType.STORY;
            }
            else
            {
                return ReleaseVersion.ItemType.UNKNOWN;
            }
        }

        internal DateTime? GetCreatedTime(string jiraID, bool useLocal = true)
        {
            return GetIssue(jiraID, useLocal)?.Created;
        }

        internal Status GetStatus(string jiraID, bool useLocal = true)
        {
            return GetIssue(jiraID, useLocal)?.Status;
        }

        internal bool Login(string username, string password)
        {
            try
            {
                _jira.Connect(URL, username, password);
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