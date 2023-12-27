using Assets.Scripts.Models.IssueProperties;
using Assets.Scripts.Models.IssueProperties.DescriptionContents;
using Assets.Scripts.Models.Issues;
using Assets.Scripts.Models.Transitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ApiRequest : MonoBehaviour
{
    // URL do API
    private string apiUrl = "https://vizards.atlassian.net/rest/api/3/";

    // Dane do uwierzytelnienia
    private string username = "";
    private string apiKey = "";

    public Issue issue;
    public List<Issue> issues;

    // Start is called before the first frame update
    public void Start()
    {
        //StartCoroutine(GetAllIssues());
        //StartCoroutine(GetIssueById("10022"));
        //StartCoroutine(GetAllTransitions());
        //StartCoroutine(CreateIssue(MockCreateIssueData()));
        //StartCoroutine(CreateIssueWithDescription(MockCreateIssueDataWithDescription()));
        //StartCoroutine(UpdateIssue("10055", MockUpdateIssueData()));
        //StartCoroutine(UpdateIssueWithDescription("10055", MockUpdateIssueDataWithDescription()));
        //StartCoroutine(DeleteIssue("10029"));
        //StartCoroutine(UpdateTransitions());
    }

    public IEnumerator GetAllIssues(Action callback)
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

                issues = new List<Issue>();

                foreach (ReceivedIssue receivedIssue in receivedIssues.issues)
                {
                    var issue = SetIssueDataFromReceivedIssue(receivedIssue);
                    issues.Add(issue);
                    Debug.Log("Id: " + issue.id + ", key: " + issue.key + ", summary: " + issue.summary + ", transition: " + issue.transitionName + ", desc: " + issue.description);
                }
            }
        }
        callback();
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

    public IEnumerator CreateIssue(CreateIssue createIssue, Action<string> callback)
    {
        string createIssueJson = JsonUtility.ToJson(createIssue);
        var url = apiUrl + $"issue";

        Debug.Log(createIssueJson);

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
                callback(receivedIssue.id);
            }
        }
    }

    IEnumerator CreateIssueWithDescription(CreateIssueWithDescription createIssue)
    {
        string createIssueJson = JsonUtility.ToJson(createIssue);
        var url = apiUrl + $"issue";

        Debug.Log(createIssueJson);

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

    public IEnumerator UpdateIssue(string issueId, UpdateIssue updateIssue)
    {
        string updateIssueJson = JsonUtility.ToJson(updateIssue);
        var url = apiUrl + $"issue/{issueId}";

        Debug.Log(updateIssueJson);

        using (UnityWebRequest webRequest = UnityWebRequest.Put(url, updateIssueJson))
        {
            SetWebRequest(webRequest);
            SetWebRequestForUpload(webRequest, updateIssueJson);

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

    IEnumerator UpdateIssueWithDescription(string issueId, UpdateIssueWithDescription updateIssue)
    {
        string updateIssueJson = JsonUtility.ToJson(updateIssue);
        var url = apiUrl + $"issue/{issueId}";

        Debug.Log(updateIssueJson);

        using (UnityWebRequest webRequest = UnityWebRequest.Put(url, updateIssueJson))
        {
            SetWebRequest(webRequest);
            SetWebRequestForUpload(webRequest, updateIssueJson);

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

    public IEnumerator DeleteIssue(string issueId)
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

    public IEnumerator GetAllTransitions(string issueId = "10022")
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

    public IEnumerator UpdateTransitions(string issueId = "10028", int transitionId = 11)
    {
        var url = apiUrl + $"issue/{issueId}/transitions";

        var updateTransition = new UpdateTransition
        {
            transition = new TransitionId
            {
                id = transitionId,
            }
        };

        string updateTransitionJson = JsonUtility.ToJson(updateTransition);

        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, updateTransitionJson))
        {
            SetWebRequest(webRequest);
            SetWebRequestForUpload(webRequest, updateTransitionJson);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Update transition request successful");
            }
        }
    }

    private void SetWebRequest(UnityWebRequest webRequest)
    {
        LoadEnv();
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
                summary = "Nowo utworzone zadanie"
            }
        };
    }

    private CreateIssueWithDescription MockCreateIssueDataWithDescription()
    {
        var description = new Description();
        var paragraphContent = new ParagraphContent();
        var text = new TextContent();
        text.text = "Pierwsza notatka";
        paragraphContent.content.Add(text);
        description.content.Add(paragraphContent);

        return new CreateIssueWithDescription
        {
            fields = new CreateIssueFieldsWithDescription
            {
                summary = "Nowo utworzone zadanie",
                description = description
            }
        };
    }

    private UpdateIssue MockUpdateIssueData()
    {
        return new UpdateIssue
        {
            fields = new UpdateIssueFields
            {
                summary = "Edytowane zadanie"
            }
        };
    }

    private UpdateIssueWithDescription MockUpdateIssueDataWithDescription()
    {
        var description = new Description();
        var paragraphContent = new ParagraphContent();
        var text = new TextContent();
        text.text = "Edytowana notatka";
        paragraphContent.content.Add(text);
        description.content.Add(paragraphContent);

        return new UpdateIssueWithDescription
        {
            fields = new UpdateIssueFieldsWithDescription
            {
                summary = "Edytowane zadanie",
                description = description
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

    private void LoadEnv()
    {
        string relativePath = "../../.env";
        string envPath = Path.Combine(Application.dataPath, relativePath);

        if (File.Exists(envPath))
        {
            Dictionary<string, string> envVariables = new Dictionary<string, string>();

            // Read lines from the .env file
            string[] lines = File.ReadAllLines(envPath);

            foreach (string line in lines)
            {
                int index = line.IndexOf('=');
                if (index != -1)
                {
                    string key = line.Substring(0, index).Trim();
                    string value = line.Substring(index + 1).Trim();

                    // Add key-value pair to the dictionary
                    envVariables.Add(key, value);
                }
            }
            Debug.Log(envVariables);
            // Access the variables using the dictionary
            username = envVariables["USERNAME"];
            apiKey = envVariables["API_KEY"];
        }
        else
        {
            Debug.LogError(".env file not found at path: " + envPath);
        }
    }
}
