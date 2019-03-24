#!/usr/bin/env bash
for directory in src/*; do 
    if [[ -d ${directory} ]]; then
        cd ${directory}
        dotnet pack *.csproj --include-symbols --output "../../publish"
        cd .. && cd ..;
        cd publish
        dotnet nuget push "${directory}.${GIT_TAG}.symbols.nupkg"
    fi
done