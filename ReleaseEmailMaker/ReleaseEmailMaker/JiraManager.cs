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
        public string URL { get => @"https://jira.cpgswtools.com"; }
        public string USERNAME { get => @"qizhen_lu"; }
        public string PASSWORD { get => @"5qgTfvu7eniE9WSd^!Np0*tr"; }

        private JiraManager()
        {
            _jira = new Jira.SDK.Jira();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _jira.Connect(URL, USERNAME, PASSWORD);
        }

        public string GetTitle(string jiraID)
        {
            var issue = _jira.GetIssue(jiraID.ToUpper());
            return issue?.Summary;
        }
    }
}