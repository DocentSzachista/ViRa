using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System;
using System.Collections;
using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class PostItNote : MonoBehaviour
{
    public bool isNew;
    public bool exitedBlock;
    //public bool isSelected = false; 

    public NotesManager notesManager;

    private string _description;
    public string Description { get => _description; set
        {
            _description = value;
            descriptionText.text = _description;
        } }
    public string CurrentSectionName;
    public string TaskId;

    private string CollidedSectionName;

    public TextMeshProUGUI descriptionText;

    XRGrabInteractable m_GrabInteractable;

    private Vector3 currentPosition;
    private string _oldText;

    private void Awake()
    {
        m_GrabInteractable = GetComponent<XRGrabInteractable>();

        descriptionText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isNew && Description.IsNullOrEmpty())
        {
            Description = "New ViRa Note!";
        }
        descriptionText.text = Description;
        _oldText = null;
        if (isNew)
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void Keyboard_OnTextSubmitted(object sender, System.EventArgs e)
    {
        //if (isSelected)
        //{
            // Sprawdź, czy nadawca zdarzenia to klawiatura
            if (sender is NonNativeKeyboard keyboard)
            {
            // Pobierz tekst z pola TextInput
                _oldText = Description;
                Description = keyboard.InputField.text;
                descriptionText.text += keyboard.InputField.text;


                // Tutaj możesz wykonać operacje na wprowadzonym tekście
                Debug.Log("Entered text: " + descriptionText.text);
            }
        //}
    }




    private void OnEnable()
    {
        m_GrabInteractable.selectExited.AddListener(OnSelectExit);
        m_GrabInteractable.selectEntered.AddListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs arg0)
    {
        notesManager.keyboardRef.OnTextSubmitted += Keyboard_OnTextSubmitted;
        notesManager.keyboardRef.PresentKeyboard();
        if (!isNew) return;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        m_GrabInteractable.selectExited.RemoveListener(OnSelectExit);
    }
    private void OnSelectExit(SelectExitEventArgs arg0) => Invoke(nameof(Dropped), 0.1f);

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Dropped()
    {
        notesManager.keyboardRef.OnTextSubmitted -= Keyboard_OnTextSubmitted;
        if (!CollidedSectionName.IsNullOrEmpty())
        {
            if (isNew)
            {
                notesManager.NoteCreated(gameObject, CollidedSectionName);
                isNew = false;
            }
            else if (!_oldText.Equals(Description))
            {
                notesManager.NoteEdited(TaskId, Description);
            }
            else if (CollidedSectionName != CurrentSectionName)
            {
                notesManager.NoteMoved(gameObject, CurrentSectionName, CollidedSectionName);
            }
            else
            {
                MoveToPosition(currentPosition);
            }

            CurrentSectionName = CollidedSectionName;
        }
    }

    public void MoveToPosition(Vector3 position)
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(MoveToPosition(position, 0.6f));
    }

    IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure accurate final position
        currentPosition = targetPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Section"))
        {
            CollidedSectionName = other.name;
            Debug.Log("Collided with section: " + CollidedSectionName);
        }
        if (other.CompareTag("Trash"))
        {
            notesManager.NoteTrashed(TaskId, CurrentSectionName);
            Destroy(gameObject);
        }
    }
}
