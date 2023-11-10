curl --request GET \
  --url 'https://vizards.atlassian.net/rest/api/3/issue/{VIRA-1}/transitions' \
  --user 'username:apitoken' \
  --header 'Accept: application/json'

# Potrzebne info
# -nic, to wykorzystujemy jedynie do pobrania ID tranzycji które będą wykorzystywane później
# VIRA-1 to jest klucz do issue
