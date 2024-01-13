using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class NotesBlock : MonoBehaviour
{
    static int counter = 1;
    private Vector3 newNotePosition = new Vector3(-2.6321f, 0.776f, 1.1f);

    public GameObject NewNotePrefab;
    public NotesManager notesManager;

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " " + other.gameObject.name);
        
        var postIt = other.GetComponent<PostItNote>();

        if (postIt != null && postIt.isNew && !postIt.exitedBlock)
        {
            CreateNewInvisibleNote();
            postIt.exitedBlock = true;
        }
    }

    private void CreateNewInvisibleNote()
    {
        var note = Instantiate(NewNotePrefab, newNotePosition, Quaternion.Euler(0, 0, 0));
        note.name = NewNotePrefab.name + counter;
        
        var postit = note.GetComponent<PostItNote>();
        postit.Description = $"New ViRa Note! ({counter++})";
        postit.notesManager = notesManager;
    }
}
