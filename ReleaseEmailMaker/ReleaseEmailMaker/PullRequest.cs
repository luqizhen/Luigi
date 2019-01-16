namespace ReleaseEmailMaker
{
    public class PullRequest
    {
        public string Assignee { get; private set; }
        public string IssueID { get; private set; }
        public string Title { get; private set; }
        public string KeyReviewers { get; private set; }
        public string Updated { get; private set; }
    }
}