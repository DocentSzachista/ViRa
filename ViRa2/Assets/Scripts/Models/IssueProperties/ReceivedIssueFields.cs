using System;

namespace Assets.Scripts.Models.IssueProperties
{
    [Serializable]
    public class ReceivedIssueFields
    {

        public string summary;
        public Status status;
        public Description description;
    }
}
