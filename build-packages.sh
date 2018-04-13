#!/usr/bin/env bash
cd src/
for d in */; do cd ${d} && dotnet pack *.csproj --include-symbols --output /home/firebirdy/Shared/Nuget/ && cd ..; done