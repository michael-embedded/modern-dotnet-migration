# Modern .NET, Chapter 13, Section 13.2
docker run --rm -p 8080:8080 -e Shop__Name="Container Shop" shopservice:1.0.0
curl http://localhost:8080/
curl http://localhost:8080/health/ready
