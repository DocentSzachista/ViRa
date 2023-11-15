## Gdzie znaleźć passy:
- https://github.com/DocentSzachista/ViRa/settings/secrets/codespaces
jest tam zarówno username i klucz api



## Dodawanie nowego issue:
[Szczegóły](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issues/#api-rest-api-3-issue-post) co można dodatkowo wrzucić.
```
curl --request POST \
  --url 'https://vizards.atlassian.net/rest/api/3/issue' \
  --user 'USERNAME:klucz_API' \
  --header 'Accept: application/json' \
  --header 'Content-Type: application/json' \
  --data '{}'
```
Jakie jest minimum danych i co jest czym

```
{
    "fields": {
       "project":
       {
          "key": "VIRA" <- Taki jest klucz w jirze,
       },
       "summary": "tester_3", <- podsumowanie (czyli to co się wyświetla jako nagłówek)
       "issuetype": {
          "id": "10003" <- jest to defaultowo zadanie (radziłbym zostawić na sztywno)
       }
   }
}
```
Potencjalnie dodałbym ewentualnie jeszcze to dla kompletności jako wartość dla "fields":
```
"description": {
      "content": [
        {
          "content": [
            {
              "text": "Order entry fails when selecting supplier.",
              "type": "text"
            }
          ],
          "type": "paragraph"
        }
      ],
      "type": "doc",
      "version": 1
    },
```



Jako odpowiedź zostanie zwrócone coś takiego na przykład
```
{   "id":"10019",
    "key":"VIRA-3",
    "self":"https://vizards.atlassian.net/rest/api/3/issue/10019"
}
```
id i klucz można używać wzajemnie w pozostałych endpointach, choć ja testowałem jedynie klucz

## Wyświetlanie szczegółów issue
[link](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issues/#api-rest-api-3-issue-issueidorkey-get)

```
curl --request GET \
  --url 'https://vizards.atlassian.net/rest/api/3/issue/{klucz-do-issue}' \
  --user 'username:api_key' \
  --header 'Accept: application/json'
```

Bare minimum pola które będziemy chcieli zapisać i wyświetlać (IMO)
- summary
- description
- id/klucz do issue
- status issue (to jest jedyna rzecz która nie jest konsekwetna)

## Usuwanie issue
Usuwanie issue
[link](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issues/#api-rest-api-3-issue-issueidorkey-delete)
```
curl --request DELETE \
  --url 'https://vizards.atlassian.net/rest/api/3/issue/{issueIdOrKey}' \
  --user 'username:api_key'
```

## Wyciąganie możliwych tranzycji w projekcie (czyli np TODO, in progress itd.)

```
curl --request GET \
  --url 'https://vizards.atlassian.net/rest/api/3/issue/{issueIdOrKey}/transitions' \
  --user 'username:api_key' \
  --header 'Accept: application/json'
```
Potrzebne:
- name
- id


## Update issue
Można niby próbować tutaj też wrzucić tranzycje ale mi nie chciało działać
[link](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issues/#api-rest-api-3-issue-issueidorkey-put)
```
curl --request PUT \
  --url 'https://vizards.atlassian.net/rest/api/3/issue/{issueIdOrKey}' \
  --user 'username:api_key' \
  --header 'Accept: application/json' \
  --header 'Content-Type: application/json' \
  --data-raw '{ "fields": {
        "summary": "Update Summary"
    }
}'
```
Takie minimum


## Przesuwanie issue po tablicach
[link](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issues/#api-rest-api-3-issue-issueidorkey-transitions-post)
```
curl --request POST \
  --url 'https://vizards.atlassian.net/rest/api/3/issue/{issueIdOrKey}/transitions' \
  --user 'username:api_key' \
  --header 'Accept: application/json' \
  --header 'Content-Type: application/json' \
  --data '{"transition":{"id":<Jakies id tranzcyji>}}'
```


## listowanie wszystkich issues.
```
curl --request GET \
  --url 'https://vizards.atlassian.net/rest/api/3/search?jql=project=VIRA' \
  --user 'username:api_key' \
  --header 'Accept: application/json'
```
potrzebne pola:
- summary
- description
- id/klucz do issue
- status issue (to jest jedyna rzecz która nie jest - konsekwetna)
