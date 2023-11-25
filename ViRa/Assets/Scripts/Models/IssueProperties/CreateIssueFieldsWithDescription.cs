using System;

namespace Assets.Scripts.Models.IssueProperties
{
    [Serializable]
    public class CreateIssueFieldsWithDescription : CreateIssueFields
    {
        /// <summary>
        /// Opcjonale (może zostać nullem)
        /// </summary>
        public Description description = null;
    }
}
