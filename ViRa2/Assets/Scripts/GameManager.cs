using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform[] columns;

    public int notesNumber = 20; // Ilość notatek do wygenerowania na jedną kolumnę
    public float offset = 5f; // Odstęp między notatkami na osi X


    void Start()
    {
        StworzNotatki();
    }

    void StworzNotatki()
    {
        foreach (Transform column in columns)
        {
            Vector3 columnSize = column.GetComponent<Renderer>().bounds.size;
            Vector3 noteSize = notePrefab.GetComponent<Renderer>().bounds.size;

            float row = 0;
            float col = 0;

            for (int i = 0; i < notesNumber; i++)
            {
                // Ustaw pozycję notatki w górnym lewym rogu kostki (z odstępem między notatkami na osi X)
                Vector3 notePosition = new Vector3(
                    column.position.x - columnSize.x / 2f + col * (noteSize.x+noteSize.x/offset), //+ i * (notatkaSize.x + odstepMiedzyNotatkami),
                    column.position.y + columnSize.y / 2f - row * (noteSize.y+noteSize.y/offset),
                    column.position.z
                );
                col++;;

                // Twórz notatkę w górnym lewym rogu kostki
                GameObject note = Instantiate(notePrefab, notePosition, column.rotation);

                // Obróć notatkę (możesz dostosować ten kąt według potrzeb)
                note.transform.rotation = Quaternion.Euler(-180f, 0f, 180f);

                // Sprawdź, czy notatka przekroczyła granicę kolumny
                if (notePosition.x + noteSize.x / 2f > column.position.x + columnSize.x / 2f)
                {
                    // Przenieś notatkę na początkowy x z obniżeniem o jeden rozmiar notatki w dół
                    note.transform.position = new Vector3(
                        column.position.x - columnSize.x / 2f,
                        note.transform.position.y - noteSize.y - noteSize.y/offset,
                        column.position.z
                    );

                    col=1;
                    row++;
                }
            }
            
        }

    }

}
