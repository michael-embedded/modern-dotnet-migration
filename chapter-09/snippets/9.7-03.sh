# Modern .NET, Chapter 9, Section 9.7
TOKEN=$(curl -s -X POST "http://localhost:5000/dev/token?user=ada&scope=products.write" | jq -r .access_token)
curl -i -X POST http://localhost:5000/api/products \
     -H "Authorization: Bearer $TOKEN" -H "Content-Type: application/json" \
     -d '{"name":"Monitor","price":199}'
