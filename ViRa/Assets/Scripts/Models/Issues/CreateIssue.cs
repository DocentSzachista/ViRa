using Assets.Scripts.Models.IssueProperties;
using System;

namespace Assets.Scripts.Models.Issues
{
    /// <summary>
    /// Obiekt issue wykorzystywany do tworzenia zadania
    /// </summary>
    [Serializable]
    public class CreateIssue
    {
        /// <summary>
        /// Issue data fields
        /// </summary>
        public CreateIssueFields fields;
    }
}
