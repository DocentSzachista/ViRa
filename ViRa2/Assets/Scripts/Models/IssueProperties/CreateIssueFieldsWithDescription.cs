using System;

namespace Assets.Scripts.Models.IssueProperties
{
    [Serializable]
    public class CreateIssueFieldsWithDescription : CreateIssueFields
    {
        public Description description;
    }
}
