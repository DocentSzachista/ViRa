using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models.IssueProperties.DescriptionContents
{
    /// <summary>
    /// Paragraf w opisie
    /// </summary>
    [Serializable]
    public class ParagraphContent
    {
        /// <summary>
        /// Lista tekstów w paragrafie
        /// </summary>
        [SerializeField]
        public List<TextContent> content = new List<TextContent>();

        /// <summary>
        /// Typ jest tutaj defultowy jako pagaragraf, nie zmieniamy
        /// </summary>
        public string type = "paragraph";
    }
}
