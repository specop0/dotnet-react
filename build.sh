#!/bin/bash

DIR=$(dirname "$(readlink -f "$0")")

echo dotnet publish $DIR/dotnet-react/dotnet-react.csproj --configuration Release --output publish/dotnet-react
dotnet publish $DIR/dotnet-react/dotnet-react.csproj --configuration Release --output publish/dotnet-react
