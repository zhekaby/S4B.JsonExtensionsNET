#!/usr/bin/env bash
set -e 

API_KEY=$1
VERSION=$2

dotnet pack S4B.JsonExtensionsNET/S4b.JsonExtensionsNET.csproj -c Release -o out -p:PackageVersion=$VERSION
dotnet nuget push out/S4B.JsonExtensionsNET.$VERSION.nupkg --api-key $API_KEY --source https://api.nuget.org/v3/index.json