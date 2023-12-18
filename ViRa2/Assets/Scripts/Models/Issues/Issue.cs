using Assets.Scripts.Models.IssueProperties;

namespace Assets.Scripts.Models.Issues
{
    public class Issue
    {
        public string id;

        public string key;

        public string summary;

        public Description description;

        public string transitionName { get; set; }
    }
}
