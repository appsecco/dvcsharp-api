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

## Vulnerabilities



## Issues (For Students to find and fix)

* Authentication is custom. It should be replaced with Identity Framework
* Hardcoded JWT secret and other validation info