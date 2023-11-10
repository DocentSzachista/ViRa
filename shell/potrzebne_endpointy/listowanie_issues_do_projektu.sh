curl -D- -u USERNAME:API_KEY \
        -X GET -H "Content-Type: application/json" \
"https://vizards.atlassian.net/rest/api/3/search?jql=project=ViRa%20AND%20status!=Done"
