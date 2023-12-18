using Assets.Scripts.Models.IssueProperties;
using System;

namespace Assets.Scripts.Models.Issues
{
    /// <summary>
    /// Obiekt issue wykorzystywany do aktualizowania tytułu zadania (minimum wymaganych danych)
    /// </summary>
    [Serializable]
    public class UpdateIssue
    {
        public UpdateIssueFields fields;
    }
}
