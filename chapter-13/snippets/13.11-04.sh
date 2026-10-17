# Modern .NET, Chapter 13, Section 13.11
docker compose -f compose.release.yaml up -d
for i in 1 2 3 4; do curl -s localhost:8080/; echo; done   # responses alternate between replicas
