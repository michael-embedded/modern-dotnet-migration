# Modern .NET, Chapter 13, Section 13.2
docker build -t shopservice:dockerfile .
docker run --rm -p 8080:8080 shopservice:dockerfile
