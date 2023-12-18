using Assets.Scripts.Models.IssueProperties;
using System;

namespace Assets.Scripts.Models.Issues
{
    [Serializable]
    public class ReceivedIssue
    {
        public string id;
        public string key;
        public ReceivedIssueFields fields;
    }
}
