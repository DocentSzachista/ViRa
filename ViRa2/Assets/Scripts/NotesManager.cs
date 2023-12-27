using Assets.Scripts.Models.Issues;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    ApiRequest apiRequest;

    public GameObject NotePrefab;

    public Transform ToDoSection;
    public Transform DoingSection;
    public Transform DoneSection;
    public NonNativeKeyboard keyboardRef;

    private List<GameObject> ToDoNotes;
    private List<GameObject> DoingNotes;
    private List<GameObject> DoneNotes;

    public float offset = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        apiRequest = new ApiRequest();

        ToDoNotes = new List<GameObject>();
        DoingNotes = new List<GameObject>();
        DoneNotes = new List<GameObject>();

        StartCoroutine(apiRequest.GetAllIssues(FillBoard));
    }

    void FillBoard()
    {
        StartCoroutine(apiRequest.GetAllTransitions());

        foreach (var issue in apiRequest.issues)
        {
            var note = CreateNote(issue);
            if (note != null)
            {
                switch (issue.transitionName)
                {
                    case "To Do":
                        ToDoNotes.Add(note);
                        break;
                    case "In Progress":
                        DoingNotes.Add(note);
                        break;
                    case "Done":
                        DoneNotes.Add(note);
                        break;
                }
            }
        }

        PlaceNotes(ToDoNotes, ToDoSection);
        PlaceNotes(DoingNotes, DoingSection);
        PlaceNotes(DoneNotes, DoneSection);
    }

    private void PlaceNotes(List<GameObject> notes, Transform section, int startIndex = 0)
    {
        var sectionSize = section.GetComponent<BoxCollider>().size;
        var noteSize = NotePrefab.GetComponent<BoxCollider>().size;

        //Debug.Log($"section size: {sectionSize}");
        //Debug.Log($"note size: {noteSize}");

        //Debug.Log(sectionSize.x / (noteSize.x * (1 + offset)));
        int maxColumns = (int)(sectionSize.x / (noteSize.x * (1 + offset)));
        int maxRows = (int)(sectionSize.y / (noteSize.y * (1 + offset)));

        Vector3 firstPosition = new Vector3(
            section.transform.position.x - sectionSize.x / 2f + noteSize.x / 2f,
            section.transform.position.y + sectionSize.y / 2f - noteSize.y / 2f,
            section.transform.position.z
            );
        Debug.Log($"first position: {firstPosition}");

        for (int i = startIndex; i < notes.Count; i++)
        {
            int col = i % maxColumns;
            int row = i / maxColumns;
            Vector3 newPosition = firstPosition + new Vector3(
                    col * (noteSize.x * (1 + offset)),
                    - row * (noteSize.y * (1 + offset)),
                    0
                );
            var postitScript = notes[i].GetComponent<PostItNote>();
            postitScript.MoveToPosition(newPosition);
            //notes[i].transform.rotation = section.rotation;
        }
    }

    private GameObject CreateNote(Issue issue)
    {
        GameObject newNote = null;
        // Check if the object to copy is not null
        if (NotePrefab != null)
        {
            // Instantiate a copy of the object at the same position and rotation
            newNote = Instantiate(NotePrefab, transform.position, ToDoSection.rotation);
            
            PostItNote noteComponent;
            var result = newNote.TryGetComponent(out noteComponent);
            if (result)
            {
                newNote.name = "Note-" + issue.id;
                noteComponent.isSelected = false;
                noteComponent.Description = issue.summary;
                noteComponent.TaskId = issue.id;
                noteComponent.CurrentSectionName = issue.transitionName;

                noteComponent.notesManager = this;

                Debug.Log($"Section: {noteComponent.CurrentSectionName}");
            }

        }
        else
        {
            Debug.LogWarning("Object to copy is not assigned!");
        }
        return newNote;
    }

    public void NoteCreated(GameObject note, string section)
    {
        CreateNote(note);
        switch (section)
        {
            case "To Do":
                ToDoNotes.Add(note); 
                PlaceNotes(ToDoNotes, ToDoSection, ToDoNotes.Count - 1);
                break;
            case "In Progress":
                DoingNotes.Add(note);
                PlaceNotes(DoingNotes, DoingSection, DoingNotes.Count - 1);
                break;
            case "Done":
                DoneNotes.Add(note);
                PlaceNotes(DoneNotes, DoneSection, ToDoNotes.Count - 1);
                break;
        }
    }

    private void CreateNote(GameObject note)
    {
        string summary = note.GetComponent<PostItNote>().Description;

        CreateIssue newIssue = new CreateIssue()
        {
            fields = new Assets.Scripts.Models.IssueProperties.CreateIssueFields()
            {
                summary = summary
            }
        };

        void SetNoteId(string id)
        {
            note.GetComponent<PostItNote>().TaskId = id;
            note.name = $"Note-{id}";
        }

        StartCoroutine(apiRequest.CreateIssue(newIssue, SetNoteId));
    }

    public void NoteTrashed(string id, string currentSection)
    {
        int originalId;
        GameObject noteTrashed;
        switch (currentSection)
        {
            case "To Do":
                noteTrashed = ToDoNotes.Find(x => x.GetComponent<PostItNote>().TaskId == id);
                originalId = ToDoNotes.IndexOf(noteTrashed);
                ToDoNotes.Remove(noteTrashed);
                PlaceNotes(ToDoNotes, ToDoSection, originalId);
                break;
            case "In Progress":
                noteTrashed = DoingNotes.Find(x => x.GetComponent<PostItNote>().TaskId == id);
                originalId = DoingNotes.IndexOf(noteTrashed);
                DoingNotes.Remove(noteTrashed);
                PlaceNotes(DoingNotes, DoingSection, originalId);
                break;
            case "Done":
                noteTrashed = DoneNotes.Find(x => x.GetComponent<PostItNote>().TaskId == id);
                originalId = DoneNotes.IndexOf(noteTrashed);
                DoneNotes.Remove(noteTrashed);
                PlaceNotes(DoneNotes, DoneSection, originalId);
                break;
        }
        StartCoroutine(apiRequest.DeleteIssue(id));
    }

    public void NoteEdited(string id, string currentText)
    {
        UpdateIssue updatedIssue = new UpdateIssue(){
            fields = new Assets.Scripts.Models.IssueProperties.UpdateIssueFields()
            {
                summary = currentText
            }
        };

        StartCoroutine(apiRequest.UpdateIssue(id, updatedIssue));   
    }



    public void NoteMoved(GameObject note, string from, string to)
    {
        var postitScript = note.GetComponent<PostItNote>();
        var issueId = postitScript.TaskId;

        int originalId = -1;
        switch (from)
        {
            case "To Do":
                originalId = ToDoNotes.IndexOf(note);
                ToDoNotes.Remove(note);
                PlaceNotes(ToDoNotes, ToDoSection, originalId);
                break;
            case "In Progress":
                originalId = DoingNotes.IndexOf(note);
                DoingNotes.Remove(note);
                PlaceNotes(DoingNotes, DoingSection, originalId);
                break;
            case "Done":
                originalId = DoneNotes.IndexOf(note);
                DoneNotes.Remove(note);
                PlaceNotes(DoneNotes, DoneSection, originalId);
                break;
        }


        switch (to)
        {
            case "To Do":
                ToDoNotes.Add(note);
                PlaceNotes(ToDoNotes, ToDoSection, ToDoNotes.Count-1);
                UpdateTransition(issueId, to);
                break;
            case "In Progress":
                DoingNotes.Add(note);
                PlaceNotes(DoingNotes, DoingSection, DoingNotes.Count-1);
                break;
            case "Done":
                DoneNotes.Add(note);
                PlaceNotes(DoneNotes, DoneSection, DoneNotes.Count-1);
                break;
        }
        UpdateTransition(issueId, to);
    }

    void UpdateTransition(string issueId, string transitionName)
    {

        switch (transitionName)
        {
            // Magic numbers! Whoohoo!
            case "To Do":
                StartCoroutine(apiRequest.UpdateTransitions(issueId, 11));
                
                break;
            case "In Progress":
                StartCoroutine(apiRequest.UpdateTransitions(issueId, 21));
                break;
            case "Done":
                StartCoroutine(apiRequest.UpdateTransitions(issueId, 31));
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
