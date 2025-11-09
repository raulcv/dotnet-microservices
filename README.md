## Azure Service Bus + dotnet 8 [C#] microservice

Example services based in events with Azure service Bus + in dotnet 8 + PostgreSQL

#### Services apps
Design of services communication

![something](/utils/design.png)
---

[![dotnet](https://img.shields.io/badge/dotnet-V8-purple?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/en-us/) [![csharp](https://img.shields.io/badge/CSharp-v12-darkgreen?style=for-the-badge&logo=C&logoColor=green)](https://dotnet.microsoft.com/es-es/languages/csharp) [![Azure-Service-Bus](https://img.shields.io/badge/azureServiceBus-v5.13.4-blue?style=for-the-badge)](https://azure.microsoft.com/en-us/products/service-bus) [![docker](https://img.shields.io/badge/DOCKER-v4-lightblue?style=for-the-badge&logo=docker&logoColor=lightblue)](https://www.docker.com/)
[![potsgresql](https://img.shields.io/badge/PostgreSql-v5.13.4-lightyellow?style=for-the-badge&logo=postgresql&logoColor=blue)](https://www.postgresql.com/)

------------
#### Run this bundle of projects based in microservices
You need `dotnet CLI` check dotnet version `dotnet --version`


* Create required services in `Azure` cloud
```
bash clouddeployment.sh
```

* Located in the project file, Install dotnet project dependencies with `dotnet CLI`
```
dotnet restore
```

### Finally Start Services

Service AddMember
 ```
 dotnet run
 ```

 Service PickAge
 ```
 dotnet run
 ```

 Service AddAdult
 ```
 dotnet run
 ```

Service AddChild
 ```
 dotnet run
 ```

#### ...Etc

------------------------------------------------------------------------
<p align="center">
	With :heart: by <a href="https://www.raulcv.com" target="_blank">raulcv</a>
</p>

#
<h3 align="center">🤗 If you found helpful this repo, your welcome! 🐣</h3>
<p align="center">
<a href="https://www.buymeacoffee.com/iraulcv" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>
</p>