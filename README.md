# Damn Vulnerable C# Application (API Only)

## Getting Started

**Note:** This is a deliberately vulnerable app, please do not host it on production or Internet/public facing servers. Use with caution.

### Docker

```
docker-compose up
```

### Manual

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

Start application server with watcher for auto-reload on change:

```
dotnet watch run
```

## Build Docker

* To build a docker image run the following command

```bash
docker build -t appsecco/dvcsharp .
```

* To run the docker container

```bash
docker run -d --name dvcsharp -it -p 5000:5000 appsecco/dvcsharp
```

## Solution

The [documentation-dvcsharp-book](./documentation-dvcsharp-book) folder has instructions to use the app and exploit vulnerabilities that have been programmed.
