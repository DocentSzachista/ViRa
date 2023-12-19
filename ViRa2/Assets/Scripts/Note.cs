using UnityEngine;
using TMPro;
using Assets.Scripts.Models.Issues;
using Assets.Scripts.Models.IssueProperties;
using System.Collections.Generic;
using Assets.Scripts.Models.IssueProperties.DescriptionContents;


public class SkryptNotatki : MonoBehaviour
{
    // Przykład użycia TextMeshPro
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI status;
    
    private Issue issue;

    void Start()
    {
        InicjalizujNotatke(PrzykladoweIssue());
    }

    private Issue PrzykladoweIssue()
    {
        // Tutaj możesz zainicjować i zwrócić przykładowy obiekt Issue
        Issue noweIssue = new Issue
        {
            id = "1",
            key = "ISSUE-001",
            summary = "Przykładowe zadanie",
            description = new Description
            {
                content = new List<ParagraphContent>
                {
                    new ParagraphContent
                    {
                        content = new List<TextContent>
                        {
                            new TextContent { text = "To jest przykładowy opis zadania." }
                            // Dodaj więcej tekstów w razie potrzeby
                        }
                    }
                    // Dodaj więcej paragrafów w razie potrzeby
                }
            },
            transitionName = "Do Zrobienia"
        };

        return noweIssue;
    }

    public void InicjalizujNotatke(Issue newIssue)
    {
        issue = newIssue;
        if (issue != null)
        {
            // Przypisanie właściwości z obiektu Issue do odpowiednich TextMeshPro
            if (title != null)
            {
                title.text = $"{issue.summary}";
            }

            // Przykładowa implementacja dla description (załóżmy, że Description ma pole text)
            if (description != null && issue.description != null && issue.description.content.Count > 0)
            {
                // Tutaj zakładamy, że wybieramy tekst z pierwszego paragrafu w opisie
                description.text = $"Description: {issue.description.content[0].content[0].text}";
            }

            // Przykładowa implementacja dla transitionName
            if (status != null)
            {
                status.text = $"Status: {issue.transitionName}";
            }
        }
    }
}
