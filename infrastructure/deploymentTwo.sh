#!/bin/bash

cd ..

if [ -d "microservices" ]; then
    rm -rf microservices
    echo "There is already a microservices directory, removing it..."
fi

mkdir -p microservices
cd microservices

# Create all microservie projects of microservices
projectsList=("GetAdult" "GetChild" "GetAdultById" "GetChildById" "AddMember" "PickAge" "AddChild" "AddAdult")
for project in "${projectsList[@]}"; do
    dotnet new webapi -n "$project"
    cd $project
    echo "FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
WORKDIR /src
COPY $project.csproj .
RUN dotnet restore
COPY . .

RUN dotnet build \"$project.csproj\" -c Release -o /app/build

RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [\"dotnet\", \"$project.dll\"]
" > Dockerfile
cd ..
done

