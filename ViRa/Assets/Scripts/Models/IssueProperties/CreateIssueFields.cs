using System;

namespace Assets.Scripts.Models.IssueProperties
{
    [Serializable]
    public class CreateIssueFields
    {
        /// <summary>
        /// Posiada defultowy klucz, nie zmiania się tutaj wartości
        /// </summary>
        public Project project = new Project();

        /// <summary>
        /// Nagłówek zadania
        /// </summary>
        public string summary;

        /// <summary>
        /// Posiada defultowo typ "zadanie", nie zmiania się tutaj wartości
        /// </summary>
        public Issuetype issuetype = new Issuetype();
    }
}
