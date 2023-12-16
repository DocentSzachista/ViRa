using System;

namespace Assets.Scripts.Models.IssueProperties
{
    [Serializable]
    public class UpdateIssueFieldsWithDescription : UpdateIssueFields
    {
        public Description description;
    }
}
