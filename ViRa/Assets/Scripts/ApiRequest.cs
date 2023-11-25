using Assets.Scripts.Models.IssueProperties;
using Assets.Scripts.Models.Issues;
using Assets.Scripts.Models.Transitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiRequest : MonoBehaviour
{
    // URL do API
    private string apiUrl = "https://vizards.atlassian.net/rest/api/3/";

    // Dane do uwierzytelnienia
    private string username = "damian.raczkowski@vizards.it";
    private string apiKey = "";

    public Issue issue;

    // Start is called before the first frame update
    public void Start()
    {
        //StartCoroutine(GetAllIssues());
        StartCoroutine(GetIssueById("10022"));
        //StartCoroutine(GetAllTransitions());
        //StartCoroutine(CreateIssue(MockCreateIssueData()));
        //StartCoroutine(DeleteIssue("10029"));
    }

    IEnumerator GetAllIssues()
    {
        var url = apiUrl + "search?jql=project=VIRA";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            SetWebRequest(webRequest);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseData = webRequest.downloadHandler.text;
                Debug.Log("Response: " + responseData);


                var receivedIssues = JsonUtility.FromJson<ReceivedIssues>(responseData);

                var issues = new List<Issue>();

                foreach (ReceivedIssue receivedIssue in receivedIssues.issues)
                {
                    var issue = SetIssueDataFromReceivedIssue(receivedIssue);
                    issues.Add(issue);
                    Debug.Log("Id: " + issue.id + ", key: " + issue.key + ", summary: " + issue.summary + ", transition: " + issue.transitionName + ", desc: " + issue.description);
                }
            }
        }
    }

    IEnumerator GetIssueById(string issueId = "10022")
    {
        var url = apiUrl + $"issue/{issueId}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            SetWebRequest(webRequest);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseData = webRequest.downloadHandler.text;
                Debug.Log("Response: " + responseData);

                var receivedIssue = JsonUtility.FromJson<ReceivedIssue>(responseData);

                this.issue = SetIssueDataFromReceivedIssue(receivedIssue);
                Debug.Log("Id: " + this.issue.id + ", key: " + this.issue.key + ", summary: " + this.issue.summary + ", transition: " + this.issue.transitionName + ", desc: " + this.issue.description);
            }
        }
    }

    IEnumerator CreateIssue(CreateIssue createIssue)
    {
        string createIssueJson = JsonUtility.ToJson(createIssue);
        var url = apiUrl + $"issue";

        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, createIssueJson))
        {
            SetWebRequest(webRequest);
            SetWebRequestForUpload(webRequest, createIssueJson);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseData = webRequest.downloadHandler.text;
                Debug.Log("Response: " + responseData);

                var receivedIssue = JsonUtility.FromJson<ReceivedAfterCreateIssue>(responseData);
            }
        }
    }

    IEnumerator DeleteIssue(string issueId)
    {
        var url = apiUrl + $"issue/{issueId}";

        using (UnityWebRequest webRequest = UnityWebRequest.Delete(url))
        {
            SetWebRequest(webRequest);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("DELETE request successful");
            }
        }
    }

    IEnumerator GetAllTransitions(string issueId = "10022")
    {
        var url = apiUrl + $"issue/{issueId}/transitions";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            SetWebRequest(webRequest);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseData = webRequest.downloadHandler.text;
                Debug.Log("Response: " + responseData);

                var receivedTransitions = JsonUtility.FromJson<ReceivedTransitions>(responseData);

                foreach (var transition in receivedTransitions.transitions)
                {
                    Debug.Log("Id: " + transition.id + ", Name: " + transition.name);
                }
            }
        }
    }

    private void SetWebRequest(UnityWebRequest webRequest)
    {
        webRequest.SetRequestHeader("Accept", "application/json");
        webRequest.SetRequestHeader("Content-Type", "application/json");

        string credentials = username + ":" + apiKey;
        webRequest.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(credentials)));
    }

    private void SetWebRequestForUpload(UnityWebRequest webRequest, string jsonData)
    {
        webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
        webRequest.uploadHandler.contentType = "application/json";

        webRequest.downloadHandler = new DownloadHandlerBuffer();
    }

    private CreateIssue MockCreateIssueData()
    {
        return new CreateIssue
        {
            fields = new CreateIssueFields
            {
                summary = "Nowo utworzone zadanie",
            }
        };
    }

    private Issue SetIssueDataFromReceivedIssue(ReceivedIssue receivedIssue)
    {
        var issue = new Issue
        {
            id = receivedIssue.id,
            key = receivedIssue.key,
            summary = receivedIssue.fields.summary,
            description = receivedIssue.fields.description,
            transitionName = receivedIssue.fields.status.name
        };

        return issue;
    }
}
