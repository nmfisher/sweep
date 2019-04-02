#!/bin/sh
dotnet restore src/Sweep.fsproj
dotnet build --project src/Sweep.fsproj

