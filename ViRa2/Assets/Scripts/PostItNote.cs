using System;
using System.Collections;
using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PostItNote : MonoBehaviour
{
    public NotesManager notesManager;

    public string Description { get; set; }
    public string CurrentSectionName;
    public string TaskId;

    private string CollidedSection;

    public TextMeshProUGUI descriptionText;

    XRGrabInteractable m_GrabInteractable;

    private void Awake()
    {
        m_GrabInteractable = GetComponent<XRGrabInteractable>();

        descriptionText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        descriptionText.text = Description;
        if (CurrentSectionName.IsNullOrEmpty())
        {
            CurrentSectionName = "To Do";
        }
    }

    private void OnEnable()
    {
        m_GrabInteractable.selectExited.AddListener(OnSelectExit);
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
        if (!CollidedSection.IsNullOrEmpty())
        {
            CurrentSectionName = CollidedSection;
        }
    }

    Vector3 GetRandomPositionOffset(Vector3 sectionSize)
    {
        return new Vector3(
            UnityEngine.Random.Range(-sectionSize.x / 2, sectionSize.x / 2),
            UnityEngine.Random.Range(-sectionSize.y / 2, sectionSize.y / 2),
            0);
    }

    public void MoveToPosition(Vector3 position)
    {
        StartCoroutine(MoveToPosition(position, 0.4f));
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Section"))
        {
            CollidedSection = other.name;
            Debug.Log("Collided with section: " + CollidedSection);
        }
    }
}
