curl --request POST \
  --url 'https://vizards.atlassian.net/rest/api/3/issue/{VIRA-2}/transitions' \
  --user 'username:apitoken' \
  --header 'Accept: application/json' \
  --header 'Content-Type: application/json' \
  --data '{"transition":{"id":"21"}}'


# Potrzebne rzeczy
# - ID tranzycji (czyli podboarda, nie będziemy tworzyć nowych XD)
# - ID issue, które chcemy przenieść
