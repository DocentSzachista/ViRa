using System;
using System.Collections.Generic;

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
        public List<TextContent> content { get; set; } = new List<TextContent>();

        /// <summary>
        /// Typ jest tutaj defultowy jako pagaragraf, nie zmieniamy
        /// </summary>
        public string type { get; } = "paragraph";
    }
}
