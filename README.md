# Damn Vulnerable CSharp: Core API

## Getting Started

Install .NET Core 2.x SDK
[Microsoft .NET Core SDK](https://www.microsoft.com/net/download/macos)

Install dependencies and migrate database:

```
dotnet restore
dotnet ef database update
```

Start application server:

```
dotnet run
```

Start application server with watcher:

```
dotnet watch run
```

## Vulnerabilities

Documented in `dvcsharp-book`

## Fixes

Document in `dvcsharp-book`


## Build Docker

* To build a docker image run the following command

```bash
docker build -t appsecco/dvcsharp .
```

* To run the docker container

```bash
docker run -d --name dvcsharp -it -p 5000:5000 appsecco/dvcsharp
```
