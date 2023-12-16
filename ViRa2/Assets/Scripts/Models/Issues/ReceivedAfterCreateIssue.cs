using System;

namespace Assets.Scripts.Models.Issues
{
    /// <summary>
    /// Obiekt zwracany po dodaniu Issue
    /// </summary>
    [Serializable]
    public class ReceivedAfterCreateIssue
    {
        /// <summary>
        /// Id zadania
        /// </summary>
        public string id;

        /// <summary>
        /// Klucz zadania, unikatowy (VIRA-counter)
        /// </summary>
        public string key;

        /// <summary>
        /// Url jako odwolanie do samego siebie w jira
        /// </summary>
        public string self;
    }
}
