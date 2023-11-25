using Assets.Scripts.Models.IssueProperties.DescriptionContents;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models.IssueProperties
{
    [Serializable]
    public class Description
    {
        /// <summary>
        /// Lista paragrafów w opisie
        /// </summary>
        public List<ParagraphContent> content = new List<ParagraphContent>();

        /// <summary>
        /// Defultowy typ dla Description to doc, nie zmieniamy
        /// </summary>
        public string type = "doc";

        /// <summary>
        /// Wersja, początkowa wartość to ofc 1
        /// </summary>
        public int? version = 1;
    }
}
