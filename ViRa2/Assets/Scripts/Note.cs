using UnityEngine;
using TMPro;
using Assets.Scripts.Models.Issues;
using Assets.Scripts.Models.IssueProperties;
using System.Collections.Generic;
using Assets.Scripts.Models.IssueProperties.DescriptionContents;

public class Note : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI status;
    
    private Issue issue;

    void Start()
    {

    }

    public void InitiateNote(Issue newIssue)
    {
        issue = newIssue;
        if (issue != null)
        {

            if (title != null)
            {
                title.text = $"{issue.summary}";
            }

            if (description != null && issue.description != null && issue.description.content.Count > 0)
            {
                description.text = $"{issue.description.content[0].content[0].text}";
            }

            if (status != null)
            {
                status.text = $"Status: {issue.transitionName}";
            }
        }
    }
}
