using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models.Issues;
using Assets.Scripts.Models.IssueProperties;
using System.Collections.Generic;
using Assets.Scripts.Models.IssueProperties.DescriptionContents;
using Assets.Scripts;


public class GameManager : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform[] columns;

    public ApiRequest apiRequest; 

    public int notesNumber = 10; // Ilość notatek do wygenerowania na jedną kolumnę w funkcji testowej
    public float offset = 5f; 


    void Start()
    {
        // StworzNotatki();

        if (apiRequest == null)
        {
            Debug.LogError("Brak skryptu ApiRequest");
            StworzNotatki();
            return;
        }

        // Zarejestruj funkcję obsługującą zdarzenie pobrania issues
        apiRequest.OnIssuesDownloaded += OnIssuesDownloaded;

        // Uruchom pobieranie issues z API
        StartCoroutine(apiRequest.GetAllIssues());

    }

    // Funkcja obsługująca zdarzenie pobrania issues
    void OnIssuesDownloaded(List<Issue> issues)
    {

        List<Issue> toDoIssues = new List<Issue>(); ;
        List<Issue> doingIssues = new List<Issue>(); ;
        List<Issue> doneIssues = new List<Issue>(); ;

        foreach (Issue issue in issues)
        {
            switch (issue.transitionName)
            {
                case "To Do":
                    toDoIssues.Add(issue);
                    break;
                case "In Progress":
                    doingIssues.Add(issue);
                    break;
                case "Done":
                    doneIssues.Add(issue);
                    break;
                default:
                    Debug.LogWarning($"Nieobsługiwany status: {issue.transitionName}");
                    break;
            }
        }

        CreateNotes(columns[0], toDoIssues);
        CreateNotes(columns[1], doingIssues);
        CreateNotes(columns[2], doneIssues);
    }

    void CreateNotes(Transform column, List<Issue> issues) 
    {
        Vector3 columnSize = column.GetComponent<Renderer>().bounds.size;
        Vector3 noteSize = notePrefab.GetComponent<Renderer>().bounds.size;

        float row = 0;
        float col = 0;

        foreach (Issue issue in issues)
        {
            Vector3 notePosition = new Vector3(
                column.position.x - columnSize.x / 2f + col * (noteSize.x + noteSize.x / offset), 
                column.position.y + columnSize.y / 2f - row * (noteSize.y + noteSize.y / offset),
                column.position.z
            );
            col++; ;

            GameObject note = Instantiate(notePrefab, notePosition, column.rotation);

            note.GetComponent<Note>().InitiateNote(issue);

            note.transform.rotation = Quaternion.Euler(-180f, 0f, 180f);

            // Sprawdź, czy notatka przekroczyła granicę kolumny
            if (notePosition.x + noteSize.x / 2f > column.position.x + columnSize.x / 2f)
            {
                note.transform.position = new Vector3(
                    column.position.x - columnSize.x / 2f,
                    note.transform.position.y - noteSize.y - noteSize.y / offset,
                    column.position.z
                );

                col = 1;
                row++;
            }


        }

    }

    private Issue ExampleIssue()
    {
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
                        }
                    }
                }
            },
            transitionName = "Done"
        };

        return noweIssue;
    }

    void StworzNotatki() // funkcja testowa
    {
        foreach (Transform column in columns)
        {
            Vector3 columnSize = column.GetComponent<Renderer>().bounds.size;
            Vector3 noteSize = notePrefab.GetComponent<Renderer>().bounds.size;

            float row = 0;
            float col = 0;

            for (int i = 0; i < notesNumber; i++)
            {
                
                Vector3 notePosition = new Vector3(
                    column.position.x - columnSize.x / 2f + col * (noteSize.x + noteSize.x / offset), 
                    column.position.y + columnSize.y / 2f - row * (noteSize.y + noteSize.y / offset),
                    column.position.z
                );
                col++; ;

                GameObject note = Instantiate(notePrefab, notePosition, column.rotation);
                note.GetComponent<Note>().InitiateNote(ExampleIssue());

                note.transform.rotation = Quaternion.Euler(-180f, 0f, 180f);

                // Sprawdź, czy notatka przekroczyła granicę kolumny
                if (notePosition.x + noteSize.x / 2f > column.position.x + columnSize.x / 2f)
                {
                    note.transform.position = new Vector3(
                        column.position.x - columnSize.x / 2f,
                        note.transform.position.y - noteSize.y - noteSize.y / offset,
                        column.position.z
                    );

                    col = 1;
                    row++;
                }
            }

        }
    }

}