using System;

namespace Assets.Scripts.Models.IssueProperties.DescriptionContents
{
    /// <summary>
    /// Tekst w paragrafie
    /// </summary>
    [Serializable]
    public class TextContent
    {
        /// <summary>
        /// Tekst paragafu
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Typ jest tutaj defultowy jako text, nie zmianiamy
        /// </summary>
        public string type { get; } = "text";
    }
}
