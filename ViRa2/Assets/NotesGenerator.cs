using Assets.Scripts.Models.Issues;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesGenerator : MonoBehaviour
{
    ApiRequest apiRequest;
    public GameObject BaseNote;
    // Start is called before the first frame update
    void Start()
    {
        apiRequest = new ApiRequest();

        StartCoroutine(apiRequest.GetAllIssues(FillBoard));
    }

    void FillBoard()
    {
        foreach (var issue in apiRequest.issues)
        {
            CreateNote(issue);
        }
    }

    private void CreateNote(Issue issue)
    {
        // Check if the object to copy is not null
        if (BaseNote != null)
        {
            // Instantiate a copy of the object at the same position and rotation
            GameObject copy = Instantiate(BaseNote, transform.position, BaseNote.transform.rotation);
            PostItNote noteComponent;
            var result = copy.TryGetComponent(out noteComponent);
            if (result)
            {
                noteComponent.Description = issue.summary;
                noteComponent.TaskId = issue.id;
                copy.name = "Note-" + issue.id;
                noteComponent.IsTemplate = false;
                noteComponent.CurrentSection = issue.transitionName;
                Debug.Log($"Section: {noteComponent.CurrentSection}");
                noteComponent.MoveToSection();
            }

        }
        else
        {
            Debug.LogWarning("Object to copy is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
