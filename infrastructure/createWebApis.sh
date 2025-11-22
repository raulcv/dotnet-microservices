#!/bin/bash

# Create the directory to store the microservices
cd ..

mkdir -p microservices
cd microservices

# Create all microservie projects of microservices
dotnet new webapi -n GetAdult
dotnet new webapi -n GetChild
dotnet new webapi -n GetAdultById
dotnet new webapi -n GetChildById
dotnet new webapi -n AddMember
dotnet new webapi -n PickAge
dotnet new webapi -n AddChild
dotnet new webapi -n AddAdult