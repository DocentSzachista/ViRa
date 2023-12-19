using System;
using System.Collections;
using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PostItNote : MonoBehaviour
{
    public bool IsTemplate;

    public string Description;
    public string CurrentSection;
    public string TaskId;

    private string CollidedSection;

    public Transform ToDoSection;
    public Transform DoingSection;
    public Transform DoneSection;

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
        if (!IsTemplate)
        {
            if (CurrentSection.IsNullOrEmpty())
            {
                CurrentSection = "To Do";
            }
            MoveToSection(CurrentSection);
        }
    }

    private void OnEnable()
    {
        m_GrabInteractable.selectExited.AddListener(OnSelectExit);
        //m_GrabInteractable.focusEntered.AddListener(OnFocusEntered);
        //m_GrabInteractable.focusExited.AddListener(OnFocusExited);
    }

    private void OnDisable()
    {
        m_GrabInteractable.selectExited.RemoveListener(OnSelectExit);
        //m_GrabInteractable.focusEntered.RemoveListener(OnFocusEntered);
        //m_GrabInteractable.focusExited.RemoveListener(OnFocusExited);
    }
    //private void OnFocusEntered(FocusEnterEventArgs e)
    //{
    //    isFocused = true;
    //}

    //private void OnFocusExited(FocusExitEventArgs e)
    //{
    //    isFocused = false;
    //}

    private void OnSelectExit(SelectExitEventArgs arg0) => Invoke(nameof(Dropped), 0.1f);

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Dropped()
    {
        if (!CollidedSection.IsNullOrEmpty())
        {
            CurrentSection = CollidedSection;
        }
        MoveToSection(CurrentSection);
    }

    public void MoveToSection()
    {
        MoveToSection(CurrentSection);
    }

    void MoveToSection(string section)
    {
        Vector3 targetPosition = Vector3.zero;
        Debug.Log($"Moving to section {section}");
        switch (section)
        {
            case "To Do":
                targetPosition = ToDoSection.position;
                targetPosition += GetRandomPositionOffset(ToDoSection.GetComponent<BoxCollider>().size);
                transform.rotation = ToDoSection.rotation;
                break;
            case "In Progress":
                targetPosition = DoingSection.position;
                targetPosition += GetRandomPositionOffset(DoingSection.GetComponent<BoxCollider>().size);
                transform.rotation = DoingSection.rotation;
                break;
            case "Done":
                targetPosition = DoneSection.position;
                targetPosition += GetRandomPositionOffset(DoneSection.GetComponent<BoxCollider>().size);
                transform.rotation = DoneSection.rotation;
                break;
        }

        StartCoroutine(MoveToPosition(targetPosition, 0.4f)); // Adjust the duration as needed
    }

    Vector3 GetRandomPositionOffset(Vector3 sectionSize)
    {
        return new Vector3(
            UnityEngine.Random.Range(-sectionSize.x / 2, sectionSize.x / 2),
            UnityEngine.Random.Range(-sectionSize.y / 2, sectionSize.y / 2),
            0);
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
