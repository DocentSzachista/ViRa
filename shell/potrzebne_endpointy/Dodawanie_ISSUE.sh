curl --request POST \
  --url 'https://vizards.atlassian.net/rest/api/3/issue' \
  --user 'username:token' \
  --header 'Accept: application/json' \
  --header 'Content-Type: application/json' \
  --data '{
    "fields": {
       "project":
       {
          "key": "VIRA"
       },
       "summary": "test_3",
       "issuetype": {
          "id": "10003"
       }
   }
}'

## bare minimum
