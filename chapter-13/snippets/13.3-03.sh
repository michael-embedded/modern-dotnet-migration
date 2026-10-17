#!/usr/bin/env bash
# Modern .NET, Chapter 13, Section 13.3
set -euo pipefail

docker build -t shop:sdk-default -f Dockerfile .
dotnet publish -c Release /t:PublishContainer -p:ContainerRepository=shop -p:ContainerImageTags=chiseled -p:ContainerFamily=noble-chiseled

echo
docker images --format "table {{.Repository}}:{{.Tag}}\t{{.Size}}" | grep -E "^shop|REPOSITORY"
