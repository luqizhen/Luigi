using System;
using System.Net;

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

        public string URL { get => @"https://jira.cpgswtools.com"; }
        public bool IsLogin { get => _isLogin; private set => _isLogin = value; }

        private JiraManager()
        {
            _jira = new Jira.SDK.Jira();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public string GetTitle(string jiraID)
        {
            var issue = _jira.GetIssue(jiraID.ToUpper());
            return issue?.Summary;
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