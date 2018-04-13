#!/usr/bin/env bash
echo "path is $1";
if [ ! -d "$1" ]; then
    echo "invalid path, exiting!";
    exit;
fi
cd src/
for d in */; do cd ${d} && dotnet pack *.csproj --include-symbols --output "$1" && cd ..; done